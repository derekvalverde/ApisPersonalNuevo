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
    public class ApiCuotaListarUsuIdService:IApiCuotaListarUsuIdService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiCuotaListarUsuIdService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCuotaListarUsuIdResponse> obtenerCuotaListarUsuId(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CuotaListarUsuId";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var correlativo = new List<clsCuotaListarUsuIdResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                correlativo.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (correlativo == null)
            {
                return null;
            }

            return correlativo;

        }
        private clsCuotaListarUsuIdResponse MapToValue(SqlDataReader reader)
        {

            return new clsCuotaListarUsuIdResponse()
            {
                cuoId= Convert.ToInt32(reader["cuoId"]),
                cuoTitulo = reader["cuoTitulo"].ToString().Trim(),
                cliCodigo= reader["cliCodigo"].ToString().Trim(),
                cuoMeta = reader["cuoMeta"].ToString().Trim(),
                cuoAvance= reader["cuoAvance"].ToString().Trim(),
             };

        }
    }
}
