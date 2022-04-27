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
    public class ApiBannerListarTipoService:IApiBannerListarTipoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiBannerListarTipoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsBannerListarTipoResponse> obtenerBannerTipo(int bnnTipo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_BannerListarTipo";
            cmd.Parameters.Add("@bnnTipo", System.Data.SqlDbType.Int).Value = bnnTipo;
            var banner = new List<clsBannerListarTipoResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                banner.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (banner == null)
            {
                return null;
            }
            //Si existe Material


            return banner;

        }
        private clsBannerListarTipoResponse MapToValue(SqlDataReader reader)
        {

            return new clsBannerListarTipoResponse()
            {

                bnnId = Convert.ToInt32(reader["bnnId"]),
                bnnTipo = Convert.ToInt32(reader["bnnTipo"]),
                bnnEnlace = reader["bnnEnlace"].ToString().Trim(),
                bnnImagen = reader["bnnImagen"].ToString().Trim(),

            };

        }
    }
}
