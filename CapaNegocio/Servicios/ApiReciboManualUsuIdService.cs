using System;
using System.Collections.Generic;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.Extensions.Options;
using WebIntiApi.Models;
using CapaDatos.Request;
using CapaDatos.Models;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.Servicios
{
    public class ApiReciboManualUsuIdService: IApiReciboManualUsuIdService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiReciboManualUsuIdService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsReciboManualUsuIdResponse> obtenerReciboManualUsuId(int usuId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ReciboManualUsuId";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            var banner = new List<clsReciboManualUsuIdResponse>();
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
        private clsReciboManualUsuIdResponse MapToValue(SqlDataReader reader)
        {

            return new clsReciboManualUsuIdResponse()
            {

                corReciboManual = Convert.ToInt32(reader["corReciboManual"]),
                corReciboManualFinal = Convert.ToInt32(reader["corReciboManualFinal"]),
                
            };

        }
    }
}
