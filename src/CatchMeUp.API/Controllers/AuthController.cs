using AutoMapper;
using CatchMeUp.API.Dto;
using CatchMeUp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatchMeUp.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    [Route("register")]
    [Authorize]
    public async Task<IActionResult> Register(MemberDto memberDto)
    {
        var userFullName = HttpContext.User.Identity?.Name;
        var preferredUserName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value;
        var userIdFromToken = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sid")?.Value;

        var persistedUser = await _unitOfWork.MemberRepository.Get(x => x.UserId == userIdFromToken);
        if (!persistedUser.Any())
        {
            var newUser = new User()
            {
                UserId = userIdFromToken,
                Name = userFullName,
                UserName = preferredUserName,
                TeamId = memberDto.TeamId
            };
            await _unitOfWork.MemberRepository.Insert(newUser);
            await _unitOfWork.Save();
        }

        return Ok();
    }
}