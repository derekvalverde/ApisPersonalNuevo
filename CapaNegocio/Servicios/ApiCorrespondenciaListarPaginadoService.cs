using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CapaNegocio.Servicios
{
    public class ApiCorrespondenciaListarPaginadoService: IApiCorrespondenciaListarPaginadoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaListarPaginadoService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCorrespondenciaListarPaginadoResponse> obteneCorrespondenciaListarPaginado(string UsuCodigo, int corNumPag, int corCantReg)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_CorrespondenciaListarPaginado";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.VarChar,20).Value = UsuCodigo;
            cmd.Parameters.Add("@corNumPag", System.Data.SqlDbType.Int).Value = corNumPag;
            cmd.Parameters.Add("@corCantReg", System.Data.SqlDbType.Int).Value = corCantReg;

            var informacion = new List<clsCorrespondenciaListarPaginadoResponse>();
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
        private clsCorrespondenciaListarPaginadoResponse MapToValue(SqlDataReader reader)
        {

            return new clsCorrespondenciaListarPaginadoResponse()
            {
                Corid = Convert.ToInt32(reader["Corid"]),
                CorCodigo = reader["CorCodigo"].ToString().Trim(),
                CorNumGuia = reader["CorNumGuia"].ToString().Trim(),
                CorRemitente = reader["CorRemitente"].ToString().Trim(),
                CodNombre = reader["CodNombre"].ToString().Trim(),
                AreCodigo = reader["AreCodigo"].ToString().Trim(),
                CorFechaIni = Convert.ToDateTime(reader["CorFechaIni"]),
                CorEstado = reader["CorEstado"].ToString().Trim(),
                Urgente = reader["CdeUrgente"].ToString().Trim(),
                NDivisiones = Convert.ToInt32(reader["NDivisiones"]),
            };

        }

    }
}
