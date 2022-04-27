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
using CapaDatos.Models.Response;

namespace CapaNegocio.Servicios
{
    public class ApiModuloLeccionListarIdService: IApiModuloLeccionListarIdService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiModuloLeccionListarIdService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsModuloListarIdResponse> obtenerModuloListarId(int curId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ModuloListarId";
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;
            var listar = new List<clsModuloListarIdResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listar.Add(MapToValue(reader));
            }
            conn.Close();
            // 
            //Si el buscador no tiene elementos
            if (listar == null)
            {
                return null;
            }
            //Si existe Material


            return listar;

        }
        private clsModuloListarIdResponse MapToValue(SqlDataReader reader)
        {
            clsModuloListarIdResponse objModulo = new clsModuloListarIdResponse()
            {
                modId = Convert.ToInt32(reader["modId"]),
                modTitulo = reader["modTitulo"].ToString().Trim(),
                modDescripcion = reader["modDescripcion"].ToString().Trim(),
                modImagenDireccion = reader["modImagenDireccion"].ToString().Trim(),
                
            };

            objModulo.leccionListarId = obtenerLeccionListarId(objModulo.modId);
            return objModulo;

        }
        public List<clsLeccionListarIdResponse> obtenerLeccionListarId(int modId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_LeccionListarId";
            cmd.Parameters.Add("@modId", System.Data.SqlDbType.Int).Value = modId;
            var leccion = new List<clsLeccionListarIdResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                leccion.Add(MapToValueDetalle(reader));
            }

            //
            //Si  no tiene 
            if (leccion == null)
            {
                return null;
            }
            //Si existe Material


            return leccion;

        }

        private clsLeccionListarIdResponse MapToValueDetalle(SqlDataReader reader)
        {
            return new clsLeccionListarIdResponse()
            {

                lecId = Convert.ToInt32(reader["lecId"]),               
                lecTitulo = reader["lecTitulo"].ToString().Trim(),
            };

        }
    }
}
