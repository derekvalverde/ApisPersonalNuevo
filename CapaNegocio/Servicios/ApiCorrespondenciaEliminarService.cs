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
    public class ApiCorrespondenciaEliminarService:IApiCorrespondenciaEliminarService
    {

        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiCorrespondenciaEliminarService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCorrespondenciaEliminarResponse eliminarCorrespondencia(string usuCodigo, int idCorrespondencia, int opcion)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pa_CorrespondenciaEliminar";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.VarChar, 20).Value = usuCodigo;
            cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = idCorrespondencia;
            
                     
            cmd.Parameters.Add("@Opcion", System.Data.SqlDbType.Int).Value = opcion;
            
            ;


            var correspondencia = new clsCorrespondenciaEliminarResponse();
            var reader = cmd.ExecuteReader();

            correspondencia.correspondenciaElimimnada = true;

            conn.Close();

            return correspondencia;

        }
    }
}
