using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB.Models;

namespace WEB.Data
{
    public class WEBContext : DbContext
    {
        public WEBContext (DbContextOptions<WEBContext> options)
            : base(options)
        {
        }

        public DbSet<WEB.Models.Order> Order { get; set; } = default!;
    }
}
