using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace CapaNegocio.Servicios
{
    public class ApiCorrespondenciaListarService:IApiCorrespondenciaListarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaListarService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCorrespondenciaListarResponse> obteneCorrespondenciaListar(string UsuCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_CorrespondenciaListar";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.Int).Value = UsuCodigo;
          

            var informacion = new List<clsCorrespondenciaListarResponse>();
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
        private clsCorrespondenciaListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsCorrespondenciaListarResponse()
            {
                Corid = Convert.ToInt32(reader["Corid"]),
                CorCodigo = reader["CorCodigo"].ToString().Trim(),
                CorNumGuia = reader["CorNumGuia"].ToString().Trim(),
                CorRemitente = reader["CorRemitente"].ToString().Trim(),
                CodNombre = reader["CodNombre"].ToString().Trim(),
                AreCodigo = reader["AreCodigo"].ToString().Trim(),
                CorFechaIni = Convert.ToDateTime(reader["CorFechaIni"]),
                CorEstado = reader["CorEstado"].ToString().Trim(),
                Urgente= reader["CdeUrgente"].ToString().Trim(),
                NDivisiones = Convert.ToInt32(reader["NDivisiones"]),
            };

        }

    }
}
