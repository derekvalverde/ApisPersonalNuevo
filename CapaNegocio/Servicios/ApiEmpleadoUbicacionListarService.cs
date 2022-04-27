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
    public class ApiEmpleadoUbicacionListarService:IApiEmpleadoUbicacionListarService
    {

        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiEmpleadoUbicacionListarService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsEmpleadoUbicacionListarResponse obtenerUsuarioUbicacion(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_EmpleadoUbicacionListar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var ubicacion = new clsEmpleadoUbicacionListarResponse();
            ubicacion.emuId= 0;
            ubicacion.emuZona = "";
            ubicacion.emuDireccion = "";
            ubicacion.emuLatitud = 0;
            ubicacion.emuLongitud = 0;
        
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ubicacion = MapToValue(reader);

            }
            conn.Close();
            //
            //Si no exixte cliente
            if (ubicacion == null)
            {
                return null;
            }
            //Si existe cliente

            return ubicacion;

        }
        private clsEmpleadoUbicacionListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsEmpleadoUbicacionListarResponse()
            {
                emuId = Convert.ToInt32(reader["emuId"]),
                emuZona = reader["emuZona"].ToString().Trim(),
                emuDireccion = reader["emuDireccion"].ToString().Trim(),
                emuLatitud = Convert.ToDecimal(reader["emuLatitud"]),
                emuLongitud = Convert.ToDecimal(reader["emuLongitud"]),
                
            };

        }

    }
}
