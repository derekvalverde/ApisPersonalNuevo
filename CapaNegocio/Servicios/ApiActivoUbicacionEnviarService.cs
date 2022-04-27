using System;
using CapaDatos.Data;
using CapaDatos.Request;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CapaNegocio.Servicios
{
    public class ApiActivoUbicacionEnviarService:IApiActivoUbicacionEnviarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContextInventario _context;
        public ApiActivoUbicacionEnviarService(IOptions<AppSettings> appSettings, ApplicationDbContextInventario context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsActivoUbicacionEnviarResponse obtenerUbicacion(string actCodigo, string usucodigoDestino)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ActivoUbicacionEnviar";
            cmd.Parameters.Add("@actCodigo", System.Data.SqlDbType.VarChar, 15).Value = actCodigo;
            cmd.Parameters.Add("@usucodigoDestino", System.Data.SqlDbType.Char, 15).Value = usucodigoDestino;

            var ubicacion = new clsActivoUbicacionEnviarResponse();
            var reader = cmd.ExecuteReader();


            ubicacion.enviado = true;

            conn.Close();

            return ubicacion;

        }

    }
}
