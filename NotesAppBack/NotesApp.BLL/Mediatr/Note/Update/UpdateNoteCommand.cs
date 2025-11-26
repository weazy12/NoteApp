using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using NotesApp.BLL.DTOs.Note;

namespace NotesApp.BLL.Mediatr.Note.Update
{
    public record UpdateNoteCommand(int Id, UpdateNoteDto UpdateNoteDto): IRequest<Result<NoteDto>>;
}
