using AutoMapper;
using CatchMeUp.API.Dto;
using CatchMeUp.Core.Entities;
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
    //[Authorize]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        var persistedUser = await _unitOfWork.UserRepository.Get(x => x.UserId == userDto.UserId);
        if (!persistedUser.Any())
        {
            var newUser = new User()
            {
                UserId = userDto.UserId,
                Name = userDto.FullName,
                UserName = userDto.UserName,
                TeamId = userDto.TeamId
            };
            await _unitOfWork.UserRepository.Insert(newUser);
            await _unitOfWork.Save();
        }

        return Ok();
    }
}