using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesApp.DAL.Entities;

namespace NotesApp.DAL.Data
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions<NotesAppDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Note> Notes { get; set; }
    }
}
