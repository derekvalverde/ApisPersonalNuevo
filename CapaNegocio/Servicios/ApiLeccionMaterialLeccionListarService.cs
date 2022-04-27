using CapaDatos.Data;
using CapaDatos.Models.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public class ApiLeccionMaterialLeccionListarService: IApiLeccionMaterialLeccionListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiLeccionMaterialLeccionListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsLeccionListarIdResponse> obtenerLeccionListar(int modId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_LeccionListarId";
            cmd.Parameters.Add("@modId", System.Data.SqlDbType.Int).Value = modId;
            var listar = new List<clsLeccionListarIdResponse>();
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
        private clsLeccionListarIdResponse MapToValue(SqlDataReader reader)
        {
            clsLeccionListarIdResponse objModulo = new clsLeccionListarIdResponse()
            {
                lecId = Convert.ToInt32(reader["lecId"]),
                lecTitulo = reader["lecTitulo"].ToString().Trim(),
               
            };

            objModulo.materialLeccionListar = obtenerMaterialLeccionListar(objModulo.lecId);
            return objModulo;

        }
        public List<clsMaterialLeccionListarResponse> obtenerMaterialLeccionListar(int lecId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialLeccionListar";
            cmd.Parameters.Add("@lecId", System.Data.SqlDbType.Int).Value = lecId;
            var leccion = new List<clsMaterialLeccionListarResponse>();
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

        private clsMaterialLeccionListarResponse MapToValueDetalle(SqlDataReader reader)
        {
            return new clsMaterialLeccionListarResponse()
            {

                malNombre = reader["malNombre"].ToString().Trim(),
                malDireccion = reader["malDireccion"].ToString().Trim(),
                malFecha = Convert.ToDateTime(reader["malFecha"]),
            };

        }

    }
}
