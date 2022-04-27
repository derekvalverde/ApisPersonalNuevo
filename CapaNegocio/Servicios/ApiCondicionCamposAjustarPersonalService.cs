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
using CapaDatos.Models;

namespace CapaNegocio.Servicios
{
    public class ApiCondicionCamposAjustarPersonalService: IApiCondicionCamposAjustarPersonalService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiCondicionCamposAjustarPersonalService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCondicionCamposAjustarPersonalResponse> obtenerCamposAjustarPersonal()
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CondicionCamposAjustarPersonal";

            var camposAjustar = new List<clsCondicionCamposAjustarPersonalResponse>();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                camposAjustar.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si no tiene elementos
            if (camposAjustar == null)
            {
                return null;
            }
            //Si existe 


            return camposAjustar;

        }
        private clsCondicionCamposAjustarPersonalResponse MapToValue(SqlDataReader reader)
        {
            return new clsCondicionCamposAjustarPersonalResponse()
            {
                cosId = Convert.ToInt32(reader["cosId"]),
                cooCodigo = reader["cooCodigo"].ToString().Trim(),
                conCodigo = reader["conCodigo"].ToString().Trim(),
                cosOrden = reader["cosOrden"].ToString().Trim(),
                cooDetalle = reader["cooDetalle"].ToString().Trim(),
                cosActivo = reader["cosActivo"].ToString().Trim(),
            };

        }
        public List<clsMaterialPrecio> verDescuento(string acu)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = acu;

            var camposAjustar = new List<clsMaterialPrecio>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {

                camposAjustar.Add(MapToValue2(reader));
            }
            conn.Close();
            //
            //Si no tiene elementos
            if (camposAjustar == null)
            {
                return null;
            }
            //Si existe Material


            return camposAjustar;

        }
        private clsMaterialPrecio MapToValue2(SqlDataReader reader)
        {

            return new clsMaterialPrecio()
            {
                marValor = Convert.ToDecimal(reader["marValor"])
            };

        }
    }
}
