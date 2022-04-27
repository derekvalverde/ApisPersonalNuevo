
using System.Collections.Generic;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.Extensions.Options;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.Servicios
{
    public class ApiUsuarioInternoRegistrarImeiService:IApiUsuarioInternoRegistrarImeiService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiUsuarioInternoRegistrarImeiService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsUsuarioInternoRegistrarImeiResponse> obtenerUsuarioInternoRegistrarImei(int usuId, string usuImei)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_UsuarioInternoRegistrarImei";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@usuImei", System.Data.SqlDbType.VarChar,30).Value = usuImei;
            var imei = new List<clsUsuarioInternoRegistrarImeiResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                var obj = new clsUsuarioInternoRegistrarImeiResponse();
                int R = MapToValue(reader);
                if (R == 0)
                {
                    obj.respuesta = "NOK";
                }
                if (R == 1)
                {
                    obj.respuesta = "OK";
                }
                imei.Add(obj);
                
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (imei == null)
            {
                return null;
            }
            //Si existe Material


            return imei;

        }
        private int MapToValue(SqlDataReader reader)
        {

            return reader.GetInt32(0);


           

        }
    }
}
