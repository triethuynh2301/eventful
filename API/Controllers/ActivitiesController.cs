using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using MediatR;
using Application.Activities;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ActivitiesController : ControllerBase
  {
    private readonly IMediator _mediator;
    public ActivitiesController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivitiesAsync()
    {
      return await _mediator.Send(new ListActivitiesUseCase.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivityAsync(Guid id)
    {
      return await _mediator.Send(new GetActivityDetailsUseCase.Query { Id = id });
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivityAsync(Activity activity)
    {
      return Ok(await _mediator.Send(new CreateNewActivityUseCase.Command { Activity = activity }));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateActivityAsync(Guid id, [FromBody] Activity activity)
    {
      return Ok(await _mediator.Send(new UpdateActivityUseCase.Command { Id = id, Activity = activity }));
    }
  }
}
