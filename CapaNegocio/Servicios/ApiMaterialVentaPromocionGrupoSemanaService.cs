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
using System.Linq;

namespace CapaNegocio.Servicios
{
    public class ApiMaterialVentaPromocionGrupoSemanaService:IApiMaterialVentaPromocionGrupoSemanaService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiMaterialVentaPromocionGrupoSemanaService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialVentaPromocionGrupoSemanaResponce> obtenerVentaPromocionGrupoSemana(string matCodigo, string ageOficina)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialVentaPromocionGrupoSemana";

            cmd.Parameters.Add("@matCodigo", System.Data.SqlDbType.NVarChar, 10).Value = matCodigo;
            cmd.Parameters.Add("@ageOficina", System.Data.SqlDbType.Char, 4).Value = ageOficina;

            var ventaPromocion = new List<clsMaterialVentaPromocionGrupoSemanaResponce>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                ventaPromocion.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (ventaPromocion == null)
            {
                return null;
            }

            return ventaPromocion;

        }
        private clsMaterialVentaPromocionGrupoSemanaResponce MapToValue(SqlDataReader reader)
        {

            return new clsMaterialVentaPromocionGrupoSemanaResponce()
            {
                facMes = Convert.ToInt32(reader["facMes"]),
                facSemana = Convert.ToInt32(reader["facSemana"]),
                facCantidad = Convert.ToInt32(reader["facCantidad"])
            };

        }
    }
}
