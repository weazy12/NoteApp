using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using NotesApp.DAL.Repositories.Interfaces.Note;

namespace NotesApp.DAL.Repositories.Interfaces.Base
{
    public interface IRepositoryWrapper
    {
        INoteRepository NoteRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
