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
    public class ApiReporteListadoService: IApiReporteListadoService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiReporteListadoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsReporteListadoResponse> obtenerReporteListado(int usuId, int sgrId, int likId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ReporteListado";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@sgrId", System.Data.SqlDbType.Int).Value = Convert.ToInt32(sgrId);
            cmd.Parameters.Add("@likId", System.Data.SqlDbType.Int).Value = Convert.ToInt32(likId);

            var reporteListado = new List<clsReporteListadoResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                reporteListado.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (reporteListado == null)
            {
                return null;
            }

            return reporteListado;

        }
        private clsReporteListadoResponse MapToValue(SqlDataReader reader)
        {

            return new clsReporteListadoResponse()
            {

                repId = Convert.ToInt32(reader["repId"]),
                repNombre = reader["repNombre"].ToString().Trim(),

                repDireccion = reader["repDirecion"].ToString().Trim(),
                repDescripcion = reader["repDescripcion"].ToString().Trim(),

            };

        }
    }
}
