using AutoMapper;
using CatchMeUp.API.Dto;
using CatchMeUp.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CatchMeUp.API.Controllers;

[Route("api/teams")]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TeamsController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    [Route("createevent")]
    public async Task<IActionResult> CreateTeamEvent([FromBody] TeamEventDto teamEventDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var teamEvent = _mapper.Map<TeamEvent>(teamEventDto);
        await _unitOfWork.TeamEventRepository.Insert(teamEvent);
        await _unitOfWork.Save();
        return Ok();
    }

    [HttpGet]
    [Route("teamevents/{teamId:int}")]
    public async Task<IActionResult> GetTeamEvents(int teamId)
    {
        var teamEvents = await _unitOfWork.TeamEventRepository.Get(x => x.TeamId == teamId);
        var teamEventDtos = _mapper.Map<List<TeamEventDto>>(teamEvents);
        return Ok(teamEventDtos);
    }

    [HttpPost]
    [Route("createteam")]
    public async Task<IActionResult> CreateTeam(TeamDto teamDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var team = _mapper.Map<Team>(teamDto);
        await _unitOfWork.TeamRepository.Insert(team);
        await _unitOfWork.Save();
        return Ok(team);
    }

    [HttpGet]
    [Route("team/{teamId:int}")]
    public async Task<IActionResult> GetTeam(int teamId)
    {
        var team = await _unitOfWork.TeamRepository.GetByID(teamId);
        var teamDto = _mapper.Map<TeamDto>(team);
        return Ok(teamDto);
    }
}