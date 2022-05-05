using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
  public class CreateNewActivityUseCase
  {
    public class Command : IRequest
    {
      public Activity Activity { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }
      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        // persist in memory only 
        _context.Activities.Add(request.Activity);

        // persist changes in db
        await _context.SaveChangesAsync();

        return Unit.Value;
      }
    }
  }
}