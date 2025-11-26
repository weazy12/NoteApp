using FluentResults;
using MediatR;
using NotesApp.BLL.DTOs.Note;

namespace NotesApp.BLL.Mediatr.Note.Create
{
    public record CreateNoteCommand(CreateNoteDto CreateNoteDto) : IRequest<Result<NoteDto>>;
}
