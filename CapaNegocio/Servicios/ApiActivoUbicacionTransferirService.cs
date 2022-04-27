using System;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CapaDatos.Data;
using CapaDatos.Response;
using CapaNegocio.Servicios;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CapaNegocio.Servicios
{
    public class ApiActivoUbicacionTransferirService:IApiActivoUbicacionTransferirService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContextInventario _context;
        public ApiActivoUbicacionTransferirService(IOptions<AppSettings> appSettings, ApplicationDbContextInventario context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsActivoUbicacionTransferirResponse obtenerUbicacionTransferir(string actCodigo, string usuCodigo)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ActivoUbicacionTransferir";
            cmd.Parameters.Add("@actCodigo", System.Data.SqlDbType.VarChar, 15).Value = actCodigo;
            cmd.Parameters.Add("@usuCodigo", System.Data.SqlDbType.VarChar, 15).Value = usuCodigo;
            
            var ubicacion = new clsActivoUbicacionTransferirResponse();
            var reader = cmd.ExecuteReader();
            ubicacion.ubicacionTransferir = true;

            conn.Close();

            return ubicacion;

        }

    }
}
