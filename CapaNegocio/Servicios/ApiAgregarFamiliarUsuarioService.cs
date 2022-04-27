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
    public class ApiAgregarFamiliarUsuarioService:IApiAgregarFamiliarUsuarioService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiAgregarFamiliarUsuarioService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsAgregarFamiliarUsuarioResponse actualizarFamiliaresUsuario(string EmpCodigo, string FamNombre,int FamCarnet, string FamCarnetExt, int FamCelular, string FamTipo,  DateTime FamNacimiento, string FamSexo, string FamGradoActual, int FarmCursoActual, string EmpCodigoIni)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pa_AgregarFamiliar";

            cmd.Parameters.Add("@EmpCodigo", System.Data.SqlDbType.VarChar, 15).Value = EmpCodigo;
            cmd.Parameters.Add("@FamNombre", System.Data.SqlDbType.VarChar, 100).Value = FamNombre;
            cmd.Parameters.Add("@FamCarnet", System.Data.SqlDbType.Int).Value = FamCarnet;
            cmd.Parameters.Add("@FamCarnetExt", System.Data.SqlDbType.VarChar, 5).Value = FamCarnetExt;
            cmd.Parameters.Add("@FamCelular", System.Data.SqlDbType.Int).Value = FamCelular;
            cmd.Parameters.Add("@FamTipo", System.Data.SqlDbType.VarChar, 15).Value = FamTipo;
            cmd.Parameters.Add("@FamNacimiento", System.Data.SqlDbType.DateTime).Value = FamNacimiento;
            cmd.Parameters.Add("@FamSexo", System.Data.SqlDbType.Char, 1).Value = FamSexo;
            cmd.Parameters.Add("@FamGradoActual", System.Data.SqlDbType.VarChar, 15).Value = FamGradoActual;
            cmd.Parameters.Add("@FarmCursoActual", System.Data.SqlDbType.Int).Value = FarmCursoActual;
            cmd.Parameters.Add("@EmpCodigoIni", System.Data.SqlDbType.VarChar, 15).Value = EmpCodigoIni;


            var datosFamiliar = new clsAgregarFamiliarUsuarioResponse();
            var reader = cmd.ExecuteReader();

            datosFamiliar.actualizado = true;

            conn.Close();

            return datosFamiliar;

        }

    }
}
