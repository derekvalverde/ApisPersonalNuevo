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
    public class ApiMaterialLineaVentaStockService:IApiMaterialLineaVentaStockService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiMaterialLineaVentaStockService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialLineaVentaStockResponse> obtenerMatLinea(string ageOficina, string linCodigo,  int permiso, string aplicacion)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (aplicacion == "PC")
            {
                cmd.CommandText = "Api_MaterialLineaVentaStock";
                cmd.Parameters.Add("@ageOficina", System.Data.SqlDbType.VarChar, 4).Value = ageOficina;
                cmd.Parameters.Add("@linCodigo", System.Data.SqlDbType.VarChar, 4).Value = linCodigo;
            }
            if (aplicacion == "PP")
            {
                cmd.CommandText = "Api_MaterialVentaPersonalLinea";
                cmd.Parameters.Add("@ageOficina", System.Data.SqlDbType.VarChar, 4).Value = ageOficina;
                cmd.Parameters.Add("@linCodigo", System.Data.SqlDbType.VarChar, 4).Value = linCodigo;
                cmd.Parameters.Add("@permiso", System.Data.SqlDbType.Int).Value = permiso;
            }



            var buscadorLin = new List<clsMaterialLineaVentaStockResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                buscadorLin.Add(MapToValue(reader, aplicacion));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (buscadorLin == null)
            {
                return null;
            }
            //Si existe Material


            return buscadorLin;

        }
        private clsMaterialLineaVentaStockResponse MapToValue(SqlDataReader reader, string aplicacion)
        {
            if (aplicacion == "PP")
            {
                return new clsMaterialLineaVentaStockResponse()
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
                    mprCantidad = Convert.ToInt32(reader["mprCantidad"]),

                };
            }
            else
            {
                return new clsMaterialLineaVentaStockResponse()
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
}
