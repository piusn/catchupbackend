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
        var user = await _unitOfWork.MemberRepository.GetByID(availabilityDto.UserId);

        if (user == null)
            return NotFound($"The user with id {availabilityDto.UserId} is not found");

        user.Available = availabilityDto.AvailabilityStatus == "true";
        await _unitOfWork.AvailabilityRepository.Insert(new Availability()
        {
            StartTime = availabilityDto.StartTime,
            EndTime = availabilityDto.EndTime,
            MemberInterests = new List<MemberInterest>()
            {
                new MemberInterest() {
                    InterestId = availabilityDto.ActivityId,
                    MemberId = availabilityDto.UserId
                }
            }
        });

        await _unitOfWork.Save();

        return Ok();
    }

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
    public async Task<List<MemberDto>> GetFavourites(int userId)
    {
        var favourites = await _unitOfWork.FavouriteRepository.Get(x => x.UserId == userId);
        var ids = favourites.Select(x => x.MemberId).ToList();
        var members = await _unitOfWork.MemberRepository.Get(x => ids.Contains(x.Id));
        return _mapper.Map<List<MemberDto>>(members);
    }

    //getfavourites
    //create event
    //list events

    [HttpGet]
    [Route("interests/{userId:int}")]
    public async Task<IActionResult> GetInterests(int userId)
    {
        var user = await _unitOfWork.MemberInterestRepository
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