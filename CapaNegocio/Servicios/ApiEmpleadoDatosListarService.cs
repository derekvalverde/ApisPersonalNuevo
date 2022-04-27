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
    public class ApiEmpleadoDatosListarService:IApiEmpleadoDatosListarService
    {

        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiEmpleadoDatosListarService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsEmpleadoDatosListarResponse obtenerDatosEmpleados(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_EmpleadoDatosListar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var datosEmp = new clsEmpleadoDatosListarResponse();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                datosEmp = MapToValue(reader);


            }
            conn.Close();
            //
            //Si no exixte cliente
            if (datosEmp == null)
            {
                return null;
            }
            //Si existe cliente


            return datosEmp;

        }
        private clsEmpleadoDatosListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsEmpleadoDatosListarResponse()
            {
                empId = Convert.ToInt32(reader["empId"]),
                empNombre = reader["empNombre"].ToString().Trim(),
                empNacimientoFecha = Convert.ToDateTime(reader["empNacimientoFecha"]),
                empNacimientoLugar = reader["empNacimientoLugar"].ToString().Trim(),
                empSexo = reader["empSexo"].ToString().Trim(),
                empConyugeNombre = reader["empConyugeNombre"].ToString().Trim(),
                empConyugeCelPriv = reader["empConyugeCelPriv"].ToString().Trim(),
                empCarnet = reader["empCarnet"].ToString().Trim(),
                empCarnetCad = Convert.ToDateTime(reader["empCarnetCad"]),
                empLicencia = Convert.ToInt32(reader["empLicencia"]),
                empLicenciaCad = Convert.ToDateTime(reader["empLicenciaCad"]),
                empFechaIngreso = Convert.ToDateTime(reader["empFechaIngreso"]),
                empAfp = reader["empAfp"].ToString().Trim(),
                empNua = reader["empNua"].ToString().Trim(),
                empCaja = reader["empCaja"].ToString().Trim(),
                empNumSalud = reader["empNumSalud"].ToString().Trim(),
                empTelPriv = Convert.ToInt32(reader["empTelPriv"]),
                empCelPriv = Convert.ToInt32(reader["empCelPriv"]),
                empCelTrab = Convert.ToInt32(reader["empCelTrab"]),
                empInterno = Convert.ToInt32(reader["empInterno"]),
                empLinkEdin = reader["empLinkEdin"].ToString().Trim(),
                empEmailTrab = reader["empEmailTrab"].ToString().Trim(),
                empEmailPriv = reader["empEmailPriv"].ToString().Trim(),
                empFacebook = reader["empFacebook"].ToString().Trim(),
                empHobby = reader["empHobby"].ToString().Trim(),
                empEstado = reader["empEstado"].ToString().Trim(),
                areNombre = reader["areNombre"].ToString().Trim(),
                ofiNombre = reader["ofiNombre"].ToString().Trim(),
                cecNombre = reader["cecNombre"].ToString().Trim(),
                carNombre = reader["carNombre"].ToString().Trim(),
                cexNombre = reader["cexNombre"].ToString().Trim(),
                eecNombre = reader["eecNombre"].ToString().Trim(),
               
            };

        }
    }
}
