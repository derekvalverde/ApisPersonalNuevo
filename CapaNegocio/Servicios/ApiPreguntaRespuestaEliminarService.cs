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
    public class ApiPreguntaRespuestaEliminarService: IApiPreguntaRespuestaEliminarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiPreguntaRespuestaEliminarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsPreguntaRespuestaEliminarResponse obtenerPreguntaRespuestaEliminar(int preId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_PreguntaRespuestaEliminar";

            cmd.Parameters.Add("@preId", System.Data.SqlDbType.Int).Value = preId;

            var pregunta = new clsPreguntaRespuestaEliminarResponse();
            var reader = cmd.ExecuteReader();
            pregunta.eliminado = true;
            conn.Close();
            return pregunta;
        }
    }
}
