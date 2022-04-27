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
    public class ApiMaterialVentaPrecioService:IApiMaterialVentaPrecioService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiMaterialVentaPrecioService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialVentaPrecioResponse> obtenerMaterialVentaPrecio(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialVentaPrecio";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var material = new List<clsMaterialVentaPrecioResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                material.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (material == null)
            {
                return null;
            }

            return material;

        }
        private clsMaterialVentaPrecioResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialVentaPrecioResponse()
            {
                marClaseCondicion= reader["marClaseCondicion"].ToString().Trim(),
                cadCodigo = reader["cadCodigo"].ToString().Trim(),
                secCodigo = reader["secCodigo"].ToString().Trim(),
                ageOficina = reader["ageOficina"].ToString().Trim(),
                cliCodigo = reader["cliCodigo"].ToString().Trim(),
                prgCodigo = reader["prgCodigo"].ToString().Trim(),
                marGrupoMaterial1 = reader["marGrupoMaterial1"].ToString().Trim(), 
                marGrupoMaterial4 = reader["marGrupoMaterial4"].ToString().Trim(), 
                matCodigo = reader["matCodigo"].ToString().Trim(),
                marValor = Convert.ToDecimal(reader["marValor"]),
                marOrden = reader["marOrden"].ToString().Trim(),
                marUnidad = reader["marUnidad"].ToString().Trim(),
                marEstado = reader["marEstado"].ToString().Trim(),

            };

        }
    }
}
