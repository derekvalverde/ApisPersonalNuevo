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
    public class ApiEmpleadoListarService:IApiEmpleadoListarService
    {


        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiEmpleadoListarService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsEmpleadoListarResponse> obtenerListaEmpleados()
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_EmpleadoListar";
           

            var empleados = new List<clsEmpleadoListarResponse>();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                empleados.Add(MapToValue(reader));

            }
            conn.Close();
            //
            //Si no exixte cliente
            if (empleados == null)
            {
                return null;
            }
            //Si existe cliente

            return empleados;

        }
        private clsEmpleadoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsEmpleadoListarResponse()
            {
                usuId = Convert.ToInt32(reader["usuId"]),
                empNombre = reader["empNombre"].ToString().Trim(),
              

            };

        }

    }
}
