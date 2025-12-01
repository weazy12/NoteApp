using FluentResults;
using MediatR;
using NotesApp.BLL.DTOs.Note;

namespace NotesApp.BLL.Mediatr.Note.Delete
{
    public record DeleteNoteCommand(int Id) : IRequest<Result<NoteDto>>;
}
