using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Data
{
    public class ApplicationDbContextQR : IdentityDbContext
    {
        public ApplicationDbContextQR(DbContextOptions<ApplicationDbContextQR> options)
            : base(options)
        {
        }
    }
}
