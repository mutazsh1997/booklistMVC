using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booklistMVS.Models
{
    public class DataContenxt : DbContext
    {
        public DataContenxt(DbContextOptions<DataContenxt> options) : base(options)
        {
        }

        public DbSet<Book> books { get; set; }
    }
}
