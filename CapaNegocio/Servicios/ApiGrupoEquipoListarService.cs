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
    public class ApiGrupoEquipoListarService:IApiGrupoEquipoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiGrupoEquipoListarService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsGrupoEquipoListarResponse> obtenerGrupoEquipo()
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_GrupoEquipoListar";

            var agencias = new List<clsGrupoEquipoListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                agencias.Add(MapToValue(reader));
            }
            conn.Close();

            if (agencias == null)
            {
                return null;
            }

            return agencias;

        }
        private clsGrupoEquipoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsGrupoEquipoListarResponse()
            {

                gepCodigo = reader["gepCodigo"].ToString().Trim(),
                gepDetalle = reader["gepDetalle"].ToString().Trim(),                

            };

        }

    }
}
