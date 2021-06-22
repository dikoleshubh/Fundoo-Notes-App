using Common_Layer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Services
{
 
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<NotesModel> FundooNotes { get; set; }
       // public DbSet<LableModel> Lables { get; set; }

    }
}
