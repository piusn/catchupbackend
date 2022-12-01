using AutoMapper;
using CatchMeUp.API.Dto;
using CatchMeUp.API.Extensions;
using CatchMeUp.API.Responses;
using CatchMeUp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CatchMeUp.API.Controllers;

[Route("api/manage")]
[ApiController]
//[Authorize]
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
    //[Authorize]
    public async Task<IActionResult> SetAvailability(AvailabilityDto availabilityDto)
    {
        var user = (await _unitOfWork.UserRepository.Get(x => x.UserId == availabilityDto.UserId)).First();

        //user.Available = availabilityDto.AvailabilityStatus == "true";
        await _unitOfWork.AvailabilityRepository.Insert(new UserAvailability()
        {
            StartTime = availabilityDto.StartTime,
            EndTime = availabilityDto.EndTime,
            UserId = user.Id,
            UserInterests = new List<UserInterest>()
            {
                new UserInterest() {
                    InterestId = availabilityDto.InterestId,
                    MemberId = user.Id
                }
            }
        });

        await _unitOfWork.Save();

        return Ok();
    }
    // get interests
    [HttpGet("users/{userId}/interests")]
    //[Route("users/{userId}/interests")]
    [SwaggerOperation("GetInterests")]
    //[Authorize]
    public async Task<IActionResult> GetInterests(string userId)
    {
        var user = (await _unitOfWork.UserRepository.Get(x => x.UserId == userId)).FirstOrDefault();
        var interests = await _unitOfWork.UserInterestRepository.Get(x => x.MemberId == user.Id, includeProperties: "Interest");
        return Ok(interests);
    }

    // get availabilities (my)
   // [HttpGet("GetAvailabilities")]
    [HttpGet("users/{userId}/availability")]
    [SwaggerOperation("GetAvailabilities")]
    //[Authorize]
    public async Task<IActionResult> GetAvailabilities(string userId)
    {
        var user = (await _unitOfWork.UserRepository.Get(x => x.UserId == userId)).FirstOrDefault();
        var availabilities = await _unitOfWork.AvailabilityRepository.Get(x => x.UserId == user.Id, includeProperties: "UserInterests.Interest");
        return Ok(availabilities.Select(x=>new UserAvailabilityResponse
        {
            UserId = x.UserId,Name = x.User.Name,
            UserInterestResponses = x.UserInterests.Select(y=>new UserInterestResponse(){InterestId = y.InterestId, Name = y.Interest.Name}).ToList()
        }));
    }

    // get available availabilities(person, available, interest)

    [HttpPost]
    [Route("unfavourite")]
    public async Task<IActionResult> UnFavourite([FromBody] FavouriteDto favouriteDto)
    {
        var favourite = await _unitOfWork.FavouriteRepository.GetFavouriteByUserIds(favouriteDto.UserId, favouriteDto.MemberId);
        if (favourite != null)
        {
            await _unitOfWork.FavouriteRepository.Delete(favourite);
            await _unitOfWork.Save();
        }
        return Ok();
    }


    [HttpPost]
    [Route("favourite")]
    public async Task<IActionResult> Favourite(FavouriteDto favouriteDto)
    {
        var favourite = _mapper.Map<Favourite>(favouriteDto);
        await _unitOfWork.FavouriteRepository.Insert(favourite);
        await _unitOfWork.Save();
        return Ok();
    }

    [HttpGet]
    [Route("favourites/{userId?}")]
    //[Authorize]
    public async Task<List<UserDto>> GetFavourites(int userId)
    {
        var favourites = await _unitOfWork.FavouriteRepository.Get(x => x.UserId == userId);
        var ids = favourites.Select(x => x.MemberId).ToList();
        var members = await _unitOfWork.UserRepository.Get(x => ids.Contains(x.Id));
        return _mapper.Map<List<UserDto>>(members);
    }

    [HttpGet]
    [Route("interests/{userId:int}")]
    public async Task<IActionResult> GetInterests(int userId)
    {
        var user = await _unitOfWork.UserInterestRepository
            .Get(x => x.MemberId == userId, includeProperties: "Interest,Member");

        var userInterests = user.Select(x => new
        {
            Name = x.Interest.Name,
            Id = x.Interest.Id,
            UserId = x.MemberId
        });
        return Ok(userInterests);
    }

}