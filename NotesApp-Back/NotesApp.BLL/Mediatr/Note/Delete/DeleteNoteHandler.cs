using AutoMapper;
using FluentResults;
using MediatR;
using NotesApp.BLL.DTOs.Note;
using NotesApp.BLL.Interfaces.Logging;
using NotesApp.DAL.Repositories.Interfaces.Base;

namespace NotesApp.BLL.Mediatr.Note.Delete
{
    public class DeleteNoteHandler : IRequestHandler<DeleteNoteCommand, Result<NoteDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;
        public DeleteNoteHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }
        public async Task<Result<NoteDto>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repositoryWrapper.NoteRepository.GetFirstOrDefaultAsync(n => n.Id == request.Id);

            if(entity == null)
            {
                string errorMsg = $"Note with Id {request.Id} not found.";
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.NoteRepository.Delete(entity);
            if (await _repositoryWrapper.SaveChangesAsync() > 0)
            {
                _logger.LogInformation("Success! Task was deleted!");
                var dto = _mapper.Map<NoteDto>(entity);
                return Result.Ok(dto);
            }


            string errorM = $"Error while delete Note";
            _logger.LogError(request, errorM);
            return Result.Fail(errorM);


        }
    }
}
