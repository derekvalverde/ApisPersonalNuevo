using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CapaNegocio.Servicios
{
   public class ApiArchiveroListarPaginadoService: IApiArchiveroListarPaginadoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiArchiveroListarPaginadoService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsArchiveroListarPaginadoResponse> obtenerArchiveroListarPaginado(string UsuCodigo, int arcNumPag, int arcCantReg)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_ArchiveroListarPaginado";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.VarChar,20).Value = UsuCodigo;
            cmd.Parameters.Add("@arcNumPag", System.Data.SqlDbType.Int).Value = arcNumPag;
            cmd.Parameters.Add("@arcCantReg", System.Data.SqlDbType.Int).Value = arcCantReg;

            var informacion = new List<clsArchiveroListarPaginadoResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                informacion.Add(MapToValue(reader));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (informacion == null)
            {
                return null;
            }
            //Si existe Material


            return informacion;

        }
        private clsArchiveroListarPaginadoResponse MapToValue(SqlDataReader reader)
        {

            return new clsArchiveroListarPaginadoResponse()
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
