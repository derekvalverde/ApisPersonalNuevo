using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Data
{
    public class ApplicationDbContextInventario : IdentityDbContext
    {
        public ApplicationDbContextInventario(DbContextOptions<ApplicationDbContextInventario> options)
            : base(options)
        {
        }
    }
}
