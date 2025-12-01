using AutoMapper;
using FluentResults;
using MediatR;
using NotesApp.BLL.DTOs.Note;
using NotesApp.BLL.Interfaces.Logging;
using NotesApp.DAL.Repositories.Interfaces.Base;

namespace NotesApp.BLL.Mediatr.Note.Create
{
    public class CreateNoteHandler : IRequestHandler<CreateNoteCommand, Result<NoteDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _loggerService;

        public CreateNoteHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService loggerService)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _loggerService = loggerService;
        }
        public async Task<Result<NoteDto>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var noteEntity = _mapper.Map<DAL.Entities.Note>(request.CreateNoteDto);

            noteEntity.CreatedAt = DateTime.UtcNow;

            await _repositoryWrapper.NoteRepository.CreateAsync(noteEntity);

            if(await _repositoryWrapper.SaveChangesAsync() > 0)
            {
                _loggerService.LogInformation($"Success! Note was created!");
                var dto = _mapper.Map<NoteDto>(noteEntity);
                return Result.Ok(dto);
            }

            var errorMessage = "Failed to create the note.";
            _loggerService.LogError(request, errorMessage);
            return Result.Fail(errorMessage);
        }
    }
}
