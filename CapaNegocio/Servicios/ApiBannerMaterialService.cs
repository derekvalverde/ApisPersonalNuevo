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
    public class ApiBannerMaterialService:IApiBannerMaterialService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiBannerMaterialService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsBannerListarTipoCabeceraResponse> obtenerBanner(int bnnTipo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_BannerListarTipo";
            
            cmd.Parameters.Add("@bnnTipo", System.Data.SqlDbType.Int).Value = bnnTipo;

            var banner = new List<clsBannerListarTipoCabeceraResponse>();
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
        private clsBannerListarTipoCabeceraResponse MapToValue(SqlDataReader reader)
        {
            clsBannerListarTipoCabeceraResponse objBanner = new clsBannerListarTipoCabeceraResponse()
            {
                bnnId = Convert.ToInt32(reader["bnnId"]),
                bnnTipo = Convert.ToInt32(reader["bnnTipo"]),
                bnnEnlace = reader["bnnEnlace"].ToString().Trim(),
                bnnImagen = reader["bnnImagen"].ToString().Trim(),
                
            };

            objBanner.detalleBanner = obtenerBannerMaterial(objBanner.bnnId);
            return objBanner;

        }
        public List<clsBannerMaterialListarDetalleResponse> obtenerBannerMaterial(int bnnId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "Api_BannerMaterialListar";
            cmd.Parameters.Add("@bnnId", System.Data.SqlDbType.Int).Value = bnnId;

            var detalleBanner = new List<clsBannerMaterialListarDetalleResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                detalleBanner.Add(MapToValueDetalle(reader));
            }

            //
            //Si  no tiene 
            if (detalleBanner == null)
            {
                return null;
            }
            //Si existe Material


            return detalleBanner;

        }
        private clsBannerMaterialListarDetalleResponse MapToValueDetalle(SqlDataReader reader)
        {


            return new clsBannerMaterialListarDetalleResponse()
            {
               
                matCodigo = reader["matCodigo"].ToString().Trim(),

            };

        }
    }
}
