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
        var follow = _mapper.Map<Following>(followDto);
        await _unitOfWork.FollowingsRepository.Insert(follow);
        await _unitOfWork.Save();
        return Ok();
    }

    [HttpPost]
    [Route("setavailability")]
    public async Task<IActionResult> SetAvailability(AvailabilityDto availabilityDto)
    {
        var user = _unitOfWork.MemberRepository.GetByID(availabilityDto.UserId);

        user.Available = true;
        await _unitOfWork.Save();
        return Ok();
    }

    [HttpPost]
    [Route("unfavourite")]
    public async Task<IActionResult> UnFavourite(FavouriteDto favouriteDto)
    {
        var favourite = _mapper.Map<Favourite>(favouriteDto);
        await _unitOfWork.FavouriteRepository.Delete(favourite);
        await _unitOfWork.Save();
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
        var members = await _unitOfWork.MemberRepository.Get(x => favourites.Select(y => y.MemberId).Equals(x.Id));
        return _mapper.Map<List<MemberDto>>(members);
    }

    //getfavourites
    //create event
    //list events
}