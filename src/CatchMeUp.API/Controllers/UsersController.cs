using AutoMapper;
using CatchMeUp.API.Dto;
using CatchMeUp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CatchMeUp.API.Controllers;

[Route("api/manage")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UsersController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    [Route("follow")]
    public async Task<IActionResult> Follow(FollowDto followDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var follow = _mapper.Map<Following>(followDto);
        await _unitOfWork.FollowingsRepository.Insert(follow);
        await _unitOfWork.Save();

        followDto = _mapper.Map<FollowDto>(follow);
        return Ok(followDto);
    }

    [HttpPost]
    [Route("setavailability")]
    public async Task<IActionResult> SetAvailability(AvailabilityDto availabilityDto)
    {
        var user = _unitOfWork.MemberRepository.GetByID(availabilityDto.UserId);
        await _unitOfWork.Save();
        return Ok();
    }

    [HttpPost]
    [Route("unfavourite")]
    public async Task<IActionResult> UnFavourite([FromBody] FavouriteDto favouriteDto)
    {
        return Ok();
    }


}