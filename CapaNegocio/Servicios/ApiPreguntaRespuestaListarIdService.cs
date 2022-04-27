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
    public class ApiPreguntaRespuestaListarIdService: IApiPreguntaRespuestaListarIdService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiPreguntaRespuestaListarIdService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsPreguntaListarIdResponse> obtenerPreguntaListarId(int evaId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_PreguntaListarId";
            cmd.Parameters.Add("@evaId", System.Data.SqlDbType.Int).Value = evaId;
            var listar = new List<clsPreguntaListarIdResponse>();
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
        private clsPreguntaListarIdResponse MapToValue(SqlDataReader reader)
        {
            clsPreguntaListarIdResponse objPregunta = new clsPreguntaListarIdResponse()
            {
               preId = Convert.ToInt32(reader["preId"]),
                prePregunta = reader["prePregunta"].ToString().Trim(),
                mtiTipo = reader["mtiTipo"].ToString().Trim(),
                mtiDetalle = reader["mtiDetalle"].ToString().Trim(),

            };

            objPregunta.respuestaListarId = obtenerRespuestaListarId(objPregunta.preId);
            return objPregunta;

        }
        public List<clsRespuestaListarIdResponse> obtenerRespuestaListarId(int preId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_RespuestaListarId";
            cmd.Parameters.Add("@preId", System.Data.SqlDbType.Int).Value = preId;
            var respuesta = new List<clsRespuestaListarIdResponse>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                respuesta.Add(MapToValueDetalle(reader));
            }

            //
            //Si  no tiene 
            if (respuesta == null)
            {
                return null;
            }
            //Si existe Material


            return respuesta;

        }

        private clsRespuestaListarIdResponse MapToValueDetalle(SqlDataReader reader)
        {
            return new clsRespuestaListarIdResponse()
            {
               resId = Convert.ToInt32(reader["resId"]),
                resRespuesta = reader["resRespuesta"].ToString().Trim(),
                resEsCorrecta = Convert.ToByte(reader["resEsCorrecta"]),
                mtiTipo = reader["mtiTipo"].ToString().Trim(),
                mtiDetalle = reader["mtiDetalle"].ToString().Trim(),
            };

        }
    }
}
