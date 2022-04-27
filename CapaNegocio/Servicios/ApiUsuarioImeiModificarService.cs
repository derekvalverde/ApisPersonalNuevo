using CapaDatos.Data;
using CapaDatos.Models.Response;
using CapaDatos.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public class ApiUsuarioImeiModificarService: IApiUsuarioImeiModificarServices
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiUsuarioImeiModificarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsUsuarioImeiModificarResponse> usuarioImeiModificar(int usuId, string usuImeiCampus)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_UsuarioImeiModificar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@usuImeiCampus", System.Data.SqlDbType.VarChar, 50).Value = usuImeiCampus;
            var imei = new List<clsUsuarioImeiModificarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                var obj = new clsUsuarioImeiModificarResponse();
                int R = MapToValue(reader);
                if (R <= 0)
                {
                    obj.respuesta = 1;
                }
                else
                {
                    obj.respuesta = 0;
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
