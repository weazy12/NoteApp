using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using NotesApp.BLL.DTOs.Note;
using NotesApp.BLL.Interfaces.Logging;
using NotesApp.DAL.Repositories.Interfaces.Base;

namespace NotesApp.BLL.Mediatr.Note.Update
{
    public class UpdateNoteHandler : IRequestHandler<UpdateNoteCommand, Result<NoteDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;
        public UpdateNoteHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }
        public async Task<Result<NoteDto>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repositoryWrapper.NoteRepository.GetFirstOrDefaultAsync(n => n.Id == request.Id);

            if (entity == null)
            {
                string errorMsg = $"Note with Id {request.Id} not found.";
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            _mapper.Map(request.UpdateNoteDto, entity);

            _repositoryWrapper.NoteRepository.Update(entity);

            if (await _repositoryWrapper.SaveChangesAsync() > 0)
            { 
                var noteDto = _mapper.Map<NoteDto>(entity);
                return Result.Ok(noteDto);
            }

            string errorM = "Failed to update the note.";
            _logger.LogError(request, errorM);
            return Result.Fail(errorM);

        }
    }
}
