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

namespace NotesApp.BLL.Mediatr.Note.GetAll
{
    public class GetAllNotesHandler : IRequestHandler<GetAllNotesQuery, Result<IEnumerable<NoteDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _loggerService;

        public GetAllNotesHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService loggerService)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _loggerService = loggerService;
        }
        public async Task<Result<IEnumerable<NoteDto>>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repositoryWrapper.NoteRepository.GetAllAsync();
            _loggerService.LogInformation("Success! Return all Notes from db.");
            var dtos = _mapper.Map<IEnumerable<NoteDto>>(entities);
            return Result.Ok(dtos);
        }
    }
}
