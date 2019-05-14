using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Exams
{
    public class Delete : PageModel
    {
        private readonly IMediator _mediator;

        public Delete(IMediator mediator) => _mediator = mediator;

        [BindProperty]
        public Command Data { get; set; }

        public async Task OnGetAsync(Query query) => Data = await _mediator.Send(query);

        public async Task<IActionResult> OnPostAsync()
        {
            await _mediator.Send(Data);

            return this.RedirectToPageJson(nameof(Index));
        }

        public class Query : IRequest<Command>
        {
            public int? Id { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(m => m.Id).NotNull();
            }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Course, Command>();
        }

        public class QueryHandler : IRequestHandler<Query, Command>
        {
            private readonly SchoolContext _db;
            private readonly IConfigurationProvider _configuration;

            public QueryHandler(SchoolContext db, IConfigurationProvider configuration)
            {
                _db = db;
                _configuration = configuration;
            }

            public Task<Command> Handle(Query message, CancellationToken token) =>
                _db.Exams
                    .Where(c => c.Id == message.Id)
                    .ProjectTo<Command>(_configuration)
                    .SingleOrDefaultAsync(token);
        }

        public class Command : IRequest
        {
            public int Id { get; set; }
            public string Title { get; set; }
            [Display(Name = "Exam Date")]
            public DateTime Date { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly SchoolContext _db;

            public CommandHandler(SchoolContext db) => _db = db;

            public async Task<Unit> Handle(Command message, CancellationToken token)
            {
                var exam = await _db.Exams.FindAsync(message.Id);

                _db.Exams.Remove(exam);

                return default;
            }
        }
    }
}