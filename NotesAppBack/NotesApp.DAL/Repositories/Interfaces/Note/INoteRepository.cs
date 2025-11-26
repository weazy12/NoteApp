using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.DAL.Repositories.Interfaces.Base;

namespace NotesApp.DAL.Repositories.Interfaces.Note
{
    public interface INoteRepository : IRepositoryBase<Entities.Note>
    {
    }
}
