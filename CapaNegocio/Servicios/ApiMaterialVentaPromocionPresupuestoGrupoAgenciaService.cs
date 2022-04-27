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
using WebIntiApi.Models;

namespace CapaNegocio.Servicios
{
    public class ApiMaterialVentaPromocionPresupuestoGrupoAgenciaService:IApiMaterialVentaPromocionPresupuestoGrupoAgenciaService
    {

        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiMaterialVentaPromocionPresupuestoGrupoAgenciaService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialVentaPromocionPresupuestoGrupoAgenciaResponse> obtenerVentaPresupuesto(string matCodigo, string ageOficina)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialVentaPromocionPresupuestoGrupoAgencia";

            cmd.Parameters.Add("@matCodigo", System.Data.SqlDbType.NVarChar, 10).Value = matCodigo;
            cmd.Parameters.Add("@ageOficina", System.Data.SqlDbType.Char, 4).Value = ageOficina;

            var presupuesto = new List<clsMaterialVentaPromocionPresupuestoGrupoAgenciaResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                presupuesto.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (presupuesto == null)
            {
                return null;
            }

            return presupuesto;

        }
        private clsMaterialVentaPromocionPresupuestoGrupoAgenciaResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialVentaPromocionPresupuestoGrupoAgenciaResponse()
            {
                matCodigo = reader["matCodigo"].ToString().Trim(),
                matNombre = reader["matNombre"].ToString().Trim(),
                facCantidad = Convert.ToInt32(reader["facCantidad"]),
                manPresupuesto = Convert.ToDecimal(reader["manPresupuesto"])
            };

        }
    }
}
