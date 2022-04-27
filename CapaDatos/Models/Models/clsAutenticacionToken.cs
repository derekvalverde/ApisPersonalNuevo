using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Models
{
    public class clsAutenticacionToken
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string aplicacion { get; set; }
        //Futuro
        public string token { get; set; }
        public DateTime fechaExpiracion { get; set; }
    }
}
