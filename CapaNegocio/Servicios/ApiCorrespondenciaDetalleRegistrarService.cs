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
    public class ApiCorrespondenciaDetalleRegistrarService:IApiCorrespondenciaDetalleRegistrarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaDetalleRegistrarService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCorrespondenciaDetalleRegistrarResponse registrarCorrespondenciaDetalle(string usuCodigo, int corId, string cdeCodigo, int usuFinalId, string cdeDetalle, string cdeUrgente)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pa_CorrespondenciaDetalleRegistrar";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.VarChar, 20).Value = usuCodigo;
            cmd.Parameters.Add("@CorId", System.Data.SqlDbType.Int).Value = corId;
            cmd.Parameters.Add("@CdeCodigo", System.Data.SqlDbType.VarChar, 20).Value = cdeCodigo;
            cmd.Parameters.Add("@UsuFinalId", System.Data.SqlDbType.Int).Value = usuFinalId;
            cmd.Parameters.Add("@CdeDetalle", System.Data.SqlDbType.VarChar, 150).Value = cdeDetalle;
            cmd.Parameters.Add("@CdeUrgente", System.Data.SqlDbType.Char, 1).Value = cdeUrgente;
          
            ;


            var correspondencia = new clsCorrespondenciaDetalleRegistrarResponse();
            var reader = cmd.ExecuteReader();

            correspondencia.divisionRegistrada = true;

            conn.Close();

            return correspondencia;

        }


    }
}
