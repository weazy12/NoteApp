using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using NotesApp.BLL.Mediatr.Note.Create;

namespace NotesApp.BLL.Validator.Note
{
    public class CreateNoteValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteValidator(BaseNoteValidator baseNoteValidator)
        {
            RuleFor(x => x.CreateNoteDto).SetValidator(baseNoteValidator);
        }
    }
}
