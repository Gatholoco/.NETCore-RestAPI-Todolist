using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todoAPI.ViewModel;

namespace todoAPI.Models
{
    public class todoAPIContext : DbContext
    {
        public todoAPIContext (DbContextOptions<todoAPIContext> options)
            : base(options)
        {
        }

        public DbSet<todoAPI.ViewModel.ToDoModel> ToDoModel { get; set; }
    }
}
