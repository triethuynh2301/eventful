using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using AutoMapper;
using Persistence;

namespace Application.Activities
{
  public class UpdateActivityUseCase
  {
    public class Command : IRequest
    {
      public Guid Id { get; set; }
      public Activity Activity { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly DataContext _context;
      private readonly IMapper _mapper;
      public Handler(DataContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }
      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        // get the activity to update
        var activity = await _context.Activities.FindAsync(request.Id);

        // Map(source, destination) -> map properties from request body to activity obj
        _mapper.Map(request.Activity, activity);

        // persist changes in db
        await _context.SaveChangesAsync();

        return Unit.Value;
      }
    }
  }
}