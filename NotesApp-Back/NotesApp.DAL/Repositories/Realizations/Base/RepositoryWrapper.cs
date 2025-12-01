using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.DAL.Data;
using NotesApp.DAL.Repositories.Interfaces.Base;
using NotesApp.DAL.Repositories.Interfaces.Note;
using NotesApp.DAL.Repositories.Realizations.Note;

namespace NotesApp.DAL.Repositories.Realizations.Base
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly NotesAppDbContext _notesAppDbContext;
        private INoteRepository? _noteRepository;

        public RepositoryWrapper(NotesAppDbContext notesAppDbContext)
        {
            _notesAppDbContext = notesAppDbContext;
        }
        public INoteRepository NoteRepository
        {
            get 
            { 
                if (_noteRepository == null)
                {
                    _noteRepository = new NoteRepository(_notesAppDbContext); 
                }
                return _noteRepository; 
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _notesAppDbContext.SaveChangesAsync();
        }
    }
}
