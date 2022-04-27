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
    public class ApiPreguntaAdicionarService: IApiPreguntaAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiPreguntaAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsPreguntaAdicionarResponse obtenerPreguntaAdicionar(int evaId, string prePregunta, string preEstado,  int mtiId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_PreguntaAdicionar";
            cmd.Parameters.Add("@evaId", System.Data.SqlDbType.Int).Value = evaId;
            cmd.Parameters.Add("@prePregunta", System.Data.SqlDbType.NVarChar, 100).Value = prePregunta;
            cmd.Parameters.Add("@preEstado", System.Data.SqlDbType.NVarChar, 350).Value = preEstado;
           
            cmd.Parameters.Add("@mtiId", System.Data.SqlDbType.Int).Value = mtiId;

            var pregunta = new clsPreguntaAdicionarResponse();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                pregunta = MapToValue(reader);
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (pregunta == null)
            {
                return null;
            }
            return pregunta;
        }
        private clsPreguntaAdicionarResponse MapToValue(SqlDataReader reader)
        {
            return new clsPreguntaAdicionarResponse()
            {
                preId = Convert.ToInt32(reader["preId"]),
            };

        }
    }

 }

