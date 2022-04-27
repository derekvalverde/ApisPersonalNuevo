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
    public class ApiLineaListarSubGrupoService:IApiLineaListarSubGrupoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiLineaListarSubGrupoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsLineaListarSubGrupoResponse> obtenerLineaSubGrupo(int sgrId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_LineaListarSubGrupo";
            cmd.Parameters.Add("@sgrId", System.Data.SqlDbType.Int).Value = sgrId;

            var lineas = new List<clsLineaListarSubGrupoResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                lineas.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (lineas == null)
            {
                return null;
            }
            //Si existe Material


            return lineas;

        }
        private clsLineaListarSubGrupoResponse MapToValue(SqlDataReader reader)
        {

            return new clsLineaListarSubGrupoResponse()
            {

                linId = Convert.ToInt32(reader["linId"]),
                linCodigo = reader["linCodigo"].ToString().Trim(),
                linDescripcion = reader["linDescripcion"].ToString().Trim()

            };

        }

    }
}
