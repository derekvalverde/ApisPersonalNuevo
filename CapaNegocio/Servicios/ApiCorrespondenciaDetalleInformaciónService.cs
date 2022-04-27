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
    public class ApiCorrespondenciaDetalleInformacionService:IApiCorrespondenciaDetalleInformaciónService
    {

        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaDetalleInformacionService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCorrespondenciaDetalleInformaciónResponse> obteneCorrespondenciaDetalleInformacion(int cdeId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_CorrespondenciaDetalleInformacion";

            cmd.Parameters.Add("@CdeId", System.Data.SqlDbType.Int).Value = cdeId;

            var informacion = new List<clsCorrespondenciaDetalleInformaciónResponse>();
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
        private clsCorrespondenciaDetalleInformaciónResponse MapToValue(SqlDataReader reader)
        {

            return new clsCorrespondenciaDetalleInformaciónResponse()
            {
                cdeId = Convert.ToInt32(reader["CdeId"]),
                codPadre = reader["CodPadre"].ToString().Trim(),
                cdeCodigo = reader["CdeCodigo"].ToString().Trim(),
                corRemitente = reader["CorRemitente"].ToString().Trim(),
                cdeDetalles = reader["CdeDetalles"].ToString().Trim(),
                corFechaIni = Convert.ToDateTime(reader["CorFechaIni"]),
                cdeEstado = reader["CdeEstado"].ToString().Trim()
            };

        }

    }
}
