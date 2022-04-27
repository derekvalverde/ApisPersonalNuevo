using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CapaNegocio.Servicios
{
    public class ApiCorrespondenciaDetalleListarPaginadoService: IApiCorrespondenciaDetalleListarPaginadoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaDetalleListarPaginadoService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCorrespondenciaDetalleListarPaginadoResponse> obteneCorrespondenciaDetalleListarPaginado(string UsuCodigo, int cdeNumPag, int cdeCantReg)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_CorrespondenciaDetalleListarPaginado";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.VarChar,20).Value = UsuCodigo;
            cmd.Parameters.Add("@cdeNumPag", System.Data.SqlDbType.Int).Value = cdeNumPag;
            cmd.Parameters.Add("@cdeCantReg", System.Data.SqlDbType.Int).Value = cdeCantReg;

            var informacion = new List<clsCorrespondenciaDetalleListarPaginadoResponse>();
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
        private clsCorrespondenciaDetalleListarPaginadoResponse MapToValue(SqlDataReader reader)
        {

            return new clsCorrespondenciaDetalleListarPaginadoResponse()
            {
                cdeId = Convert.ToInt32(reader["CdeId"]),
                cdeCodigo = reader["CdeCodigo"].ToString().Trim(),
                corCodigo = reader["CorCodigo"].ToString().Trim(),
                corRemitente = reader["CorRemitente"].ToString().Trim(),
                cdeDetalles = reader["CdeDetalles"].ToString().Trim(),
                cdeFechaIni = Convert.ToDateTime(reader["CdeFechaIni"]),
                cdeEstado = reader["Cdeestado"].ToString().Trim(),

                destinatario = reader["Destinatario"].ToString().Trim(),
                usuActual = reader["UsuActual"].ToString().Trim(),
                couActual = reader["CouActual"].ToString().Trim(),
            };

        }

    }
}
