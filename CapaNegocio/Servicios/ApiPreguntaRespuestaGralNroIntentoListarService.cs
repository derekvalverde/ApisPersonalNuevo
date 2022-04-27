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
    public class ApiPreguntaRespuestaGralNroIntentoListarService: IApiPreguntaRespuestaGralNroIntentoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiPreguntaRespuestaGralNroIntentoListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsPreguntaRespuestaNroIntentoListarResponse> obtenerPreguntaRespuestaGralNroIntentoListar(int evuId, int evuNroIntento)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_PreguntaRespuestaGralNroIntentoListar";
            cmd.Parameters.Add("@evuId", System.Data.SqlDbType.Int).Value = evuId;
            cmd.Parameters.Add("@evuNroIntento", System.Data.SqlDbType.Int).Value = evuNroIntento;
            var curso = new List<clsPreguntaRespuestaNroIntentoListarResponse>();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                curso.Add(MapToValue(reader));
            }
            conn.Close();

            if (curso == null)
            {
                return null;
            }

            return curso;

        }
        private clsPreguntaRespuestaNroIntentoListarResponse MapToValue(SqlDataReader reader)
        {
            return new clsPreguntaRespuestaNroIntentoListarResponse()
            {
                preId = Convert.ToInt32(reader["preId"]),
                prePregunta = reader["prePregunta"].ToString().Trim(),
                resId = Convert.ToInt32(reader["resId"]),
                resRespuesta = reader["resRespuesta"].ToString().Trim(),
                resEsCorrecta = reader["resEsCorrecta"].ToString().Trim(),

            };

        }
    }
}
