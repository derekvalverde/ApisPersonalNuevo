using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Data
{
    public class AplicacionDbContextCampusVirtual : IdentityDbContext
    {
        public AplicacionDbContextCampusVirtual(DbContextOptions<AplicacionDbContextCampusVirtual> options)
            : base(options)
        {
        }
    }
}
