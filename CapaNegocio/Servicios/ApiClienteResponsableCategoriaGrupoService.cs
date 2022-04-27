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
    public class ApiClienteResponsableCategoriaGrupoService:IApiClienteResponsableCategoriaGrupoService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteResponsableCategoriaGrupoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsClienteResponsableCategoriaGrupoResponse> obtenerResponsableGrupoUbicacion(int usuId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "Api_ClienteResponsableCategoriaGrupo";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var ubicacion = new List<clsClienteResponsableCategoriaGrupoResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                ubicacion.Add(MapToValue(reader));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (ubicacion == null)
            {
                return null;
            }
            //Si existe Material


            return ubicacion;

        }
        private clsClienteResponsableCategoriaGrupoResponse MapToValue(SqlDataReader reader)
        {

            return new clsClienteResponsableCategoriaGrupoResponse()
            {

                usuId = Convert.ToInt32(reader["usuId"]),
               usuNombre = reader["usuNombre"].ToString().Trim(),
                

            };

        }
    }
}
