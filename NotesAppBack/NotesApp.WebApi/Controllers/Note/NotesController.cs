using Microsoft.AspNetCore.Mvc;
using NotesApp.BLL.DTOs.Note;
using NotesApp.BLL.Mediatr.Note.Create;
using NotesApp.BLL.Mediatr.Note.Delete;
using NotesApp.BLL.Mediatr.Note.GetAll;
using NotesApp.BLL.Mediatr.Note.GetById;
using NotesApp.BLL.Mediatr.Note.Update;

namespace NotesApp.WebApi.Controllers.Note
{
    public class NotesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNoteDto createNoteDto)
        {
            return HandleResult(await Mediator.Send(new CreateNoteCommand(createNoteDto)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllNotesQuery()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetNoteByIdQuery(id)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteNoteCommand(id)));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNoteDto updateNoteDto)
        {
            return HandleResult(await Mediator.Send(new UpdateNoteCommand(id, updateNoteDto)));
        }
    }
}
