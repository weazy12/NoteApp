using FluentResults;
using MediatR;
using NotesApp.BLL.DTOs.Note;

namespace NotesApp.BLL.Mediatr.Note.GetById
{
    public record GetNoteByIdQuery(int Id): IRequest<Result<NoteDto>>;

}
