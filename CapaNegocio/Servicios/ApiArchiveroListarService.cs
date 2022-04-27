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
    public class ApiArchiveroListarService:IApiArchiveroListarService
    {

        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiArchiveroListarService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsArchiveroListarResponse> obteneArchiveroListar(string UsuCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_ArchiveroListar";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.Int).Value = UsuCodigo;

            var archiveros = new List<clsArchiveroListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                archiveros.Add(MapToValue(reader));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (archiveros == null)
            {
                return null;
            }
            //Si existe Material


            return archiveros;

        }
        private clsArchiveroListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsArchiveroListarResponse()
            {
                ArcCodigo = reader["ArcCodigo"].ToString().Trim(),
                ArcNombre = reader["ArcNombre"].ToString().Trim(),
                ArcDescripcion = reader["ArcDescripcion"].ToString().Trim(),
                AreCodigo = reader["AreCodigo"].ToString().Trim(),

                ArcFechaIni = Convert.ToDateTime(reader["ArcFechaIni"]),
                
            };

        }

    }
}
