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
    public class ApiAgregarUbicacionUsuarioService: IApiAgregarUbicacionUsuarioService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiAgregarUbicacionUsuarioService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsAgregarUbicacionUsuarioResponse actualizarAgregarUbicacion(string EmpCodigo, string EmuZona, string EmuDireccion, string EmuLat, string EmuLong, string EmpCodigoIni)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "pa_AgregarUbicación";

            cmd.Parameters.Add("@EmpCodigo", System.Data.SqlDbType.VarChar, 15).Value = EmpCodigo;
            cmd.Parameters.Add("@EmuZona", System.Data.SqlDbType.VarChar, 150).Value = EmuZona;
            cmd.Parameters.Add("@EmuDireccion", System.Data.SqlDbType.VarChar, 150).Value = EmuDireccion;
            cmd.Parameters.Add("@EmuLat", System.Data.SqlDbType.VarChar, 50).Value = EmuLat;
            cmd.Parameters.Add("@EmuLong", System.Data.SqlDbType.VarChar, 50).Value = EmuLong;
            cmd.Parameters.Add("@EmpCodigoIni", System.Data.SqlDbType.VarChar, 15).Value = EmpCodigoIni;


            var ubicacionUsuario = new clsAgregarUbicacionUsuarioResponse();
            var reader = cmd.ExecuteReader();

            ubicacionUsuario.actualizado = true;

            conn.Close();

            return ubicacionUsuario;

        }
    }
}
