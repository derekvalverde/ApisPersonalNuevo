using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Data
{
    public class AplicacionDbContext2:IdentityDbContext
    {
        public AplicacionDbContext2(DbContextOptions<AplicacionDbContext2> options)
            : base(options)
        {
        }
    }
}
