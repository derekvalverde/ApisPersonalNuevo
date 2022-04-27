using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Data
{
    public class AplicacionDbContext1: IdentityDbContext
    {
        public AplicacionDbContext1(DbContextOptions<AplicacionDbContext1> options)
           : base(options)
        {
        }
    }
}
