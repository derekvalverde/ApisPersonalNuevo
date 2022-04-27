using System;
using System.Collections.Generic;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.Extensions.Options;
using WebIntiApi.Models;
using CapaDatos.Request;
using CapaDatos.Models;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.Servicios
{
    public class ApiUsuarioDatosService: IApiUsuarioDatosService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContext1 _context;
        public ApiUsuarioDatosService(IOptions<AppSettings> appSettings, AplicacionDbContext1 context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsUsuarioDatosResponse obtenerDatosEmpleados(string empCodigo)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            
                cmd.CommandText = "pa_UsuarioDatos";
                cmd.Parameters.Add("@EmpCodigo", System.Data.SqlDbType.NVarChar, 15).Value = empCodigo;

            var datosEmp = new clsUsuarioDatosResponse();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                datosEmp= MapToValue(reader);
               

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
        private clsUsuarioDatosResponse MapToValue(SqlDataReader reader)
        {

            return new clsUsuarioDatosResponse()
            {
                empCodigo = reader["EmpCodigo"].ToString().Trim(),
                empNombre = reader["EmpNombre"].ToString().Trim(),
                empNacimientoFecha = Convert.ToDateTime(reader["EmpNacimientoFecha"]),
                empNacimientoLugar = reader["EmpNacimientoLugar"].ToString().Trim(),
                empSexo = reader["EmpSexo"].ToString().Trim(),
                empCarnet = Convert.ToInt32(reader["EmpCarnet"]),
                empCarnetExp = reader["EmpCarnetExp"].ToString().Trim(),
                empCarnetCaducado = Convert.ToDateTime(reader["EmpCarnetCad"]),
                empLicencia = Convert.ToInt32(reader["EmpLicencia"]),
                empLicenciaCad = Convert.ToDateTime(reader["EmpLicenciaCad"]),
                empAfp = reader["EmpAfp"].ToString().Trim(),
                empFechaIngreso = Convert.ToDateTime(reader["EmpFechaIngreso"]),
                empNua = reader["EmpNua"].ToString().Trim(),
                empCaja = reader["EmpCaja"].ToString().Trim(),
                empNumSalud = reader["EmpNumSalud"].ToString().Trim(),

                empTelPriv = Convert.ToInt32(reader["EmpTelPriv"]),
                empCelPriv = Convert.ToInt32(reader["EmpCelPriv"]),
                empCelTrab = Convert.ToInt32(reader["EmpCelTrab"]),
                empInterno = Convert.ToInt32(reader["EmpInterno"]),
                empLinkEdin = reader["EmpLinkedin"].ToString().Trim(),
                empEmailTrab = reader["EmpeMailTrab"].ToString().Trim(),
                empEmailPriv = reader["EmpeMailPriv"].ToString().Trim(),
                empEstadoCiv = reader["EmpEstadoCiv"].ToString().Trim(),
                empCarNombre = reader["CarNombre"].ToString().Trim(),
                areNombre = reader["AreNombre"].ToString().Trim(),
                cecNombre = reader["CecNombre"].ToString().Trim(),
                ofiNombre = reader["OfiNombre"].ToString().Trim(),

                /*empFechaIni = Convert.ToDateTime(reader["EmpFechaIni"]),
                empFechaMod = Convert.ToDateTime(reader["EmpFechaMod"]),
                empFechaFin = Convert.ToDateTime(reader["EmpFechaFin"]),
                empUsuIniId = Convert.ToInt32(reader["EmpUsuIniId"]),
                empUsuModId = Convert.ToInt32(reader["EmpUsuModId"]),
                empUsuFinId = Convert.ToInt32(reader["EmpUsuFinId"]),*/



            };

        }
    }
}
