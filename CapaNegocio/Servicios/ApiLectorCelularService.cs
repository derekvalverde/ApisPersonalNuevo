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
    public class ApiLectorCelularService:IApiLectorCelularService
    {

        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiLectorCelularService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsLectorCelularResponse obtenerLectorCelular(int LectorTipo, string UsuCodigo, string Codigo, string Archivero)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pa_LectorCelular";

            cmd.Parameters.Add("@LectorTipo", System.Data.SqlDbType.Int).Value = LectorTipo;
            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.VarChar, 20).Value = UsuCodigo;
            cmd.Parameters.Add("@Codigo", System.Data.SqlDbType.VarChar, 20).Value = Codigo;
            cmd.Parameters.Add("@Archivero ", System.Data.SqlDbType.VarChar, 20).Value = Archivero;
            

            var lector = new clsLectorCelularResponse();
            var reader = cmd.ExecuteReader();

            lector.escaneado = true;

            conn.Close();

            return lector;

        }

    }
}
