using FluentResults;
using MediatR;
using NotesApp.BLL.DTOs.Note;

namespace NotesApp.BLL.Mediatr.Note.GetAll
{
    public record GetAllNotesQuery : IRequest<Result<IEnumerable<NoteDto>>>;
}
