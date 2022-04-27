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
    public class ApiConocimientoListarService:IApiConocimientoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiConocimientoListarService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsConocimientoListarResponse> obtenerUsuarioEstudios(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_ConocimientoListar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var datosEmp = new List<clsConocimientoListarResponse>();
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
        private clsConocimientoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsConocimientoListarResponse()
            {

                
                conId = Convert.ToInt32(reader["conId"]),
               
                cnnNombre = reader["cnnNombre"].ToString().Trim(),
                cniNivel = reader["cniNivel"].ToString().Trim(),
             



            };

        }
    }
}
