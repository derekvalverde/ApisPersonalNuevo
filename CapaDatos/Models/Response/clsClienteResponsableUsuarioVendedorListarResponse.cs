﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsClienteResponsableUsuarioVendedorListarResponse
    {
        public int usuIdR { get; set; }
        public string usuNombre { get; set; }
        public List<clsClienteResponsableUbicacionUsuarioResponse> detalleClientes { get; set; }
    }
}
