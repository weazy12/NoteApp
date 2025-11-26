using AutoMapper;
using NotesApp.BLL.DTOs.Note;

namespace NotesApp.BLL.Mapping.Note
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<CreateNoteDto, DAL.Entities.Note>().ReverseMap();
            CreateMap<DAL.Entities.Note, NoteDto>().ReverseMap();
            CreateMap<UpdateNoteDto, DAL.Entities.Note>().ReverseMap();
        }
    }
}
