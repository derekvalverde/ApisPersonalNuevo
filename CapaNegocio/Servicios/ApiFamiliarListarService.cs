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
    public class ApiFamiliarListarService: IApiFamiliarListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiFamiliarListarService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsFamiliarListarResponse> obtenerUsuarioFamiliares(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_FamiliarListar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var datosEmp = new List<clsFamiliarListarResponse>();
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
        private clsFamiliarListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsFamiliarListarResponse()
            {
                famId = Convert.ToInt32(reader["famId"]),
                famNombre = reader["famNombre"].ToString().Trim(),
                famCarnet= reader["famCarnet"].ToString().Trim(),
                famNacimiento = Convert.ToDateTime(reader["famNacimiento"]),
                famSexo = reader["famSexo"].ToString().Trim(),
                famGradoActual = reader["famGradoActual"].ToString().Trim(),
                famCursoActual = Convert.ToInt32(reader["famCursoActual"]),
                fatNombre = reader["fatNombre"].ToString().Trim(),
                cexNombre = reader["cexNombre"].ToString().Trim(),
                // famEstado = reader["FamEstado"].ToString().Trim(),


            };

        }

    }
}
