using System;
using System.Collections.Generic;
using System.Text;

namespace CapaDatos.Models.Response
{
    public class clsLeccionListarIdResponse
    {
        public int lecId { get; set; }
        public string lecTitulo { get; set; }
        public List<clsMaterialLeccionListarResponse> materialLeccionListar { get; set; }
    }
}
