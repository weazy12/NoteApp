using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using NotesApp.BLL.DTOs.Note;

namespace NotesApp.BLL.Validator.Note
{
    public class BaseNoteValidator : AbstractValidator<CreateNoteDto>
    {
        public BaseNoteValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(5).WithMessage("Title must be at least 5 characters long.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required")
                .MaximumLength(200).WithMessage("Content must not exceed 200 characters.");
        }
    }
}
