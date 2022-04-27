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
    public class ApiUsuarioRutaDiaListarUsuIdService: IApiUsuarioRutaDiaListarUsuIdService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiUsuarioRutaDiaListarUsuIdService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsUsuarioRutaDiaListarUsuIdResponse> obtenerRutaDiariaUsuId(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_UsuarioRutaDiaListarUsuId";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var rutaDiaria = new List<clsUsuarioRutaDiaListarUsuIdResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                rutaDiaria.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (rutaDiaria == null)
            {
                return null;
            }

            return rutaDiaria;

        }
        private clsUsuarioRutaDiaListarUsuIdResponse MapToValue(SqlDataReader reader)
        {

            return new clsUsuarioRutaDiaListarUsuIdResponse()
            {
                cliCodigo = reader["cliCodigo"].ToString().Trim(),

                uruPeriodo = reader["uruPeriodo"].ToString().Trim(),

            };

        }
    }
}
