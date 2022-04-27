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
    public class ApiReferenciaListarService:IApiReferenciaListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiReferenciaListarService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsReferenciaListarResponse> obtenerUsuarioReferencia(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_ReferenciaListar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.NVarChar, 15).Value = usuId;

            var datosEmp = new List<clsReferenciaListarResponse>();
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
        private clsReferenciaListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsReferenciaListarResponse()
            {


                refId = Convert.ToInt32(reader["refId"]),
                refNombre = reader["refNombre"].ToString().Trim(),
                refParentesco = reader["refParentesco"].ToString().Trim(),
                refCelular = Convert.ToInt32(reader["refCelular"]),
                refExtras = reader["refExtras"].ToString().Trim(),

                
            };

        }
    }
}
