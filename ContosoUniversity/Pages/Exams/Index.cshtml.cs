using AutoMapper;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Exams
{
    public class Index : PageModel
    {
        private readonly IMediator _mediator;

        public Index(IMediator mediator) => _mediator = mediator;

        public Result Data { get; private set; }

        public async Task OnGetAsync() => Data = await _mediator.Send(new Query());

        public class Query : IRequest<Result>
        {
        }

        public class Result
        {
            public List<Exam> Exams { get; set; }

            public class Exam
            {
                public int Id { get; set; }
                public string Title { get; set; }
                public DateTime Date { get; set; }
            }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile() => CreateMap<Exam, Result.Exam>();
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            private readonly SchoolContext _db;
            private readonly IConfigurationProvider _configuration;

            public Handler(SchoolContext db, IConfigurationProvider configuration)
            {
                _db = db;
                _configuration = configuration;
            }

            public async Task<Result> Handle(Query message, CancellationToken token)
            {
                var exams = await _db.Exams
                    .OrderBy(d => d.Id)
                    .ProjectToListAsync<Result.Exam>(_configuration);

                return new Result
                {
                    Exams = exams
                };
            }
        }
    }
}