using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.Exams
{
    public class Create : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public Command Data { get; set; }

        public Create(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> OnPostAsync()
        {
            await _mediator.Send(Data);

            return this.RedirectToPageJson("Index");
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(m => m.Title).NotNull().Length(3, 50);
                RuleFor(m => m.Date).NotNull();
            }
        }

        public class MappingProfiler : Profile
        {
            public MappingProfiler() => CreateMap<Command, Exam>(MemberList.Source);
        }

        public class Command : IRequest<int>
        {
            //public int Id { get; set; }
            [StringLength(50, MinimumLength = 3)]
            public string Title { get; set; }
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            [Display(Name = "Exam Date")]
            public DateTime? Date { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly SchoolContext _context;
            private readonly IMapper _mapper;

            public CommandHandler(SchoolContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<int> Handle(Command message, CancellationToken token)
            {
                var exam = _mapper.Map<Command, Exam>(message);

                _context.Exams.Add(exam);

                await _context.SaveChangesAsync(token);

                return exam.Id;
            }
        }
    }
}