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
    public class ApiCorrespondenciaRegistrarService:IApiCorrespondenciaRegistrarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaRegistrarService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCorrespondenciaRegistrarResponse registrarCorrespondencia(string UsuCodigo, string CorCodigo, string CorNumGuia, int CodId, int AreId, string CorRemitente, string CorUrgente)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pa_CorrespondenciaRegistrar";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.VarChar, 20).Value = UsuCodigo;
            cmd.Parameters.Add("@CorCodigo", System.Data.SqlDbType.VarChar, 20).Value = CorCodigo;
            cmd.Parameters.Add("@CorNumGuia", System.Data.SqlDbType.VarChar, 45).Value = CorNumGuia;
            cmd.Parameters.Add("@CodId", System.Data.SqlDbType.Int).Value = CodId;
            cmd.Parameters.Add("@AreId", System.Data.SqlDbType.Int).Value = AreId;
            cmd.Parameters.Add("@CorRemitente", System.Data.SqlDbType.VarChar, 150).Value = CorRemitente;
            cmd.Parameters.Add("@CorUrgente", System.Data.SqlDbType.Char, 1).Value = CorUrgente;
            ;


            var correspondencia = new clsCorrespondenciaRegistrarResponse();
            var reader = cmd.ExecuteReader();

            correspondencia.registrado = true;

            conn.Close();

            return correspondencia;

        }

    }
}
