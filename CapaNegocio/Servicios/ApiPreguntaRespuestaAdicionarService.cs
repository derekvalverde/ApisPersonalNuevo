using CapaDatos.Data;
using CapaDatos.Models.Request;
using CapaDatos.Models.Response;
using CapaDatos.Request;
using CapaDatos.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public class ApiPreguntaRespuestaAdicionarService: IApiPreguntaRespuestaAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiPreguntaRespuestaAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsPreguntaRespuestaAdicionarResponse registrarPreguntaRespuesta(clsPreguntaAdicionarRequest pregunta)
        {
            //puedes hac
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd1 = conn.CreateCommand(); 
            conn.Open();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.CommandText = "Api_PreguntaAdicionar";
            cmd1.Parameters.Add("@evaId", System.Data.SqlDbType.Int).Value = pregunta.evaId;
            cmd1.Parameters.Add("@prePregunta", System.Data.SqlDbType.NVarChar,300).Value = pregunta.prePregunta;
            cmd1.Parameters.Add("@preEstado", System.Data.SqlDbType.Char, 2).Value = pregunta.preEstado;
            cmd1.Parameters.Add("@mtiId", System.Data.SqlDbType.Int).Value = pregunta.mtiId;

            var reader = cmd1.ExecuteReader();


            int preId = 0;

            while (reader.Read())
            {
                preId = Convert.ToInt32(reader["preId"]);
            }

            //detalle


            foreach (clsRespuestaAdicionarRequest respuestaPre in pregunta.respuestaAdicionar)
                {
                    SqlCommand cmd4 = conn.CreateCommand();
                    cmd4.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd4.CommandText = "Api_RespuestaAdicionar";

                    cmd4.Parameters.Add("@preId", System.Data.SqlDbType.Int).Value = preId;
                    cmd4.Parameters.Add("@resRespuesta", System.Data.SqlDbType.NVarChar,300).Value = respuestaPre.resRespuesta;
                    cmd4.Parameters.Add("@resEsCorrecta", System.Data.SqlDbType.Bit).Value = respuestaPre.resEsCorrecta;
                    cmd4.Parameters.Add("@resEstado", System.Data.SqlDbType.NVarChar, 80).Value = respuestaPre.resEstado;
                    cmd4.Parameters.Add("@mtiId", System.Data.SqlDbType.Int).Value = respuestaPre.mtiId;

                    reader = cmd4.ExecuteReader();
                }         
          
            clsPreguntaRespuestaAdicionarResponse preguntasRes = new clsPreguntaRespuestaAdicionarResponse();
            preguntasRes.preguntaRespuestaAdicionada = true;
            conn.Close();
            return preguntasRes;
          
        }
    }
}
