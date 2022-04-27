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
    public class ApiModuloEvaluacionListarIdService: IApiModuloEvaluacionListarIdService
    {

        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiModuloEvaluacionListarIdService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
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

            objModulo.evaluacionListarId = obtenerEvaluacionListarId(objModulo.modId);
            return objModulo;

        }
        public List<clsEvaluacionListarIdResponse> obtenerEvaluacionListarId(int modId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionListarId";
            cmd.Parameters.Add("@modId", System.Data.SqlDbType.Int).Value = modId;
            var evaluacion = new List<clsEvaluacionListarIdResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                evaluacion.Add(MapToValueDetalle(reader));
            }

            //
            //Si  no tiene 
            if (evaluacion == null)
            {
                return null;
            }
            //Si existe Material


            return evaluacion;

        }

        private clsEvaluacionListarIdResponse MapToValueDetalle(SqlDataReader reader)
        {
            return new clsEvaluacionListarIdResponse()
            {

                evaId = Convert.ToInt32(reader["evaId"]),
                evaTitulo = reader["evaTitulo"].ToString().Trim(),
                tieId = Convert.ToInt32(reader["tieId"]),
            };

        }
    }
}
