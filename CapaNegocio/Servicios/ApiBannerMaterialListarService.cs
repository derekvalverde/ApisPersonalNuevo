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
    public class ApiBannerMaterialListarService:IApiBannerMaterialListarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
      
        public ApiBannerMaterialListarService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
           
        }
        public List<clsBannerMaterialListarResponse> obtenerBannerMaterial(int bnnId, string ageOficina)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_BannerMaterialDetalleListar";
            cmd.Parameters.Add("@bnnId", System.Data.SqlDbType.Int).Value = bnnId;
            cmd.Parameters.Add("@ageOficina", System.Data.SqlDbType.Int).Value = ageOficina;
            var banner = new List<clsBannerMaterialListarResponse>();
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
        private clsBannerMaterialListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsBannerMaterialListarResponse()
            {
                matCodigo = reader["matCodigo"].ToString().Trim(),
                matNombre = reader["matNombre"].ToString().Trim(),
                secCodigo = reader["secCodigo"].ToString().Trim(),
                mavPrecio = Convert.ToDecimal(reader["mavPrecio"]),
                mavLinea = reader["mavLinea"].ToString().Trim(),
                mavFamilia = reader["mavFamilia"].ToString().Trim(),
                mavOrigen = reader["mavOrigen"].ToString().Trim(),
                mavExistencia = Convert.ToInt32(reader["mavExistencia"]),
                madDescuento = Convert.ToDecimal(reader["madDescuento"]),
                matImagen = reader["matImagen"].ToString().Trim(),

            };

        }

        
    }
}
