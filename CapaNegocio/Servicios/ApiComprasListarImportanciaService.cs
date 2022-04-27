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
    public class ApiComprasListarImportanciaService:IApiComprasListarImportanciaService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiComprasListarImportanciaService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialVentaStockResponse> obtenerPreferencias(string ageOficina, int uclId, int comImportancia)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ComprasListarImportancia";
            cmd.Parameters.Add("@ageOficina", System.Data.SqlDbType.VarChar, 4).Value = ageOficina;
            cmd.Parameters.Add("@uclId", System.Data.SqlDbType.Int).Value = uclId;
            cmd.Parameters.Add("@comImportancia", System.Data.SqlDbType.Int).Value = comImportancia;

            var preferencia = new List< clsMaterialVentaStockResponse > ();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                preferencia.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (preferencia == null)
            {
                return null;
            }
            //Si existe Material


            return preferencia;

        }
        private clsMaterialVentaStockResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialVentaStockResponse()
            {

                matCodigo = reader["matCodigo"].ToString().Trim(),
                matNombre = reader["matNombre"].ToString().Trim(),
                secCodigo = reader["secCodigo"].ToString().Trim(),
                mavPrecio = Convert.ToDecimal(reader["mavPrecio"]),
                mavLinea = reader["mavLinea"].ToString().Trim(),
                mavFamilia = reader["mavFamilia"].ToString().Trim(),
                mavOrigen = reader["mavOrigen"].ToString().Trim(),
                mavExistencia = Convert.ToInt32(reader["mavExistencia"]),
                madDescuento = Convert.ToDecimal(reader["madDescuento"]),
                matImagen = reader["matImagen"].ToString().Trim(),
            };

        }
        
    }
}

