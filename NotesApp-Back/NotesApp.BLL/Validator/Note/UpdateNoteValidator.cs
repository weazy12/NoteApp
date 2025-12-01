using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using NotesApp.BLL.Mediatr.Note.Update;

namespace NotesApp.BLL.Validator.Note
{
    public class UpdateNoteValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteValidator(BaseNoteValidator baseNoteValidator)
        {
            RuleFor(x => x.UpdateNoteDto).SetValidator(baseNoteValidator);
        }
    }
}
