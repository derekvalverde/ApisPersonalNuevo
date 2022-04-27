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
    public class ApiAgregarEstudioUsuarioService:IApiAgregarEstudioUsuarioService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiAgregarEstudioUsuarioService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsAgregarEstudioUsuarioResponse actualizarEstudiosUsuario(string EmpCodigo, string EstInstitucion, DateTime EstFecha, DateTime EstFechaFin, string EstExplicacion, string EstNombre, string EstNivel, int EttId, string EmpCodigoIni)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pa_AgregarEstudio";

            cmd.Parameters.Add("@EmpCodigo", System.Data.SqlDbType.VarChar, 15).Value = EmpCodigo;
            cmd.Parameters.Add("@EstInstitucion", System.Data.SqlDbType.VarChar, 150).Value = EstInstitucion;
            cmd.Parameters.Add("@EstFecha", System.Data.SqlDbType.DateTime).Value = EstFecha;
            cmd.Parameters.Add("@EstFechaFin ", System.Data.SqlDbType.DateTime).Value = EstFechaFin;
            cmd.Parameters.Add("@EstExplicacion ", System.Data.SqlDbType.VarChar, 150).Value = EstExplicacion;
            cmd.Parameters.Add("@EstNombre", System.Data.SqlDbType.VarChar, 150).Value = EstNombre;
            cmd.Parameters.Add("@EstNivel", System.Data.SqlDbType.VarChar, 50).Value = EstNivel;
            cmd.Parameters.Add("@EttId", System.Data.SqlDbType.Int).Value = EttId;
            cmd.Parameters.Add("@EmpCodigoIni", System.Data.SqlDbType.VarChar, 15).Value = EmpCodigoIni;


            var estudioUsuario = new clsAgregarEstudioUsuarioResponse();
            var reader = cmd.ExecuteReader();

            estudioUsuario.actualizado = true;

            conn.Close();

            return estudioUsuario;

        }

    }
}
