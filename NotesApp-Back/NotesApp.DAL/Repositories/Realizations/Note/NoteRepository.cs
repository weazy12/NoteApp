using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.DAL.Data;
using NotesApp.DAL.Repositories.Realizations.Base;

namespace NotesApp.DAL.Repositories.Realizations.Note
{
    public class NoteRepository : RepositoryBase<Entities.Note>, Interfaces.Note.INoteRepository
    {
        public NoteRepository(NotesAppDbContext notesAppDbContext) : base(notesAppDbContext)
        {
        }
    }
}
