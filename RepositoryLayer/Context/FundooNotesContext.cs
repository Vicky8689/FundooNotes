using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooNotesContext:DbContext
    {
        public FundooNotesContext(DbContextOptions<FundooNotesContext> options):base(options) { }
       
        public DbSet<UserEntity> Users { get; set; }  

    }
}
