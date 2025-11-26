using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NotesApp.BLL.DTOs.Note;
using NotesApp.BLL.Interfaces.Logging;
using NotesApp.DAL.Repositories.Interfaces.Base;

namespace NotesApp.BLL.Mediatr.Note.GetById
{
    public class GetNoteByIdHandler : IRequestHandler<GetNoteByIdQuery, Result<NoteDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;
        public GetNoteByIdHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }
        public async Task<Result<NoteDto>> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repositoryWrapper.NoteRepository.GetFirstOrDefaultAsync(n => n.Id == request.Id);

            if (entity == null)
            {
                string errorMsg = $"Note with Id: {request.Id} not found.";
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            return Result.Ok(_mapper.Map<NoteDto>(entity));
        }
    }
}
