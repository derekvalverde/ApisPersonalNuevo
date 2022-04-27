using System;
using System.Collections.Generic;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.Extensions.Options;
using WebIntiApi.Models;
using CapaDatos.Request;
using CapaDatos.Models;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.Servicios
{
    public class ApiUtilitarioListarEmpleadoService:IApiUtilitarioListarEmpleadoService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiUtilitarioListarEmpleadoService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsUtilitarioListarEmpleadoResponse> obtenerUtilitarioListar(string aux)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_UtilitarioListarEmpleado";
            cmd.Parameters.Add("@aux", System.Data.SqlDbType.VarChar,5).Value = aux;

            var datosEmp = new List<clsUtilitarioListarEmpleadoResponse> ();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                datosEmp.Add(MapToValue(reader));


            }
            conn.Close();
            //
            //Si no exixte cliente
            if (datosEmp == null)
            {
                return null;
            }
            //Si existe cliente


            return datosEmp;

        }
        private clsUtilitarioListarEmpleadoResponse MapToValue(SqlDataReader reader)
        {

            return new clsUtilitarioListarEmpleadoResponse()
            {

               auxId = Convert.ToInt32(reader["auxId"]),               
                auxDescripcion = reader["auxDescripcion"].ToString().Trim(),
           
            };

        }

    }
}
