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
    public class ApiRespuestaAdionarService: IApiRespuestaAdionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiRespuestaAdionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsRespuestaAdicionarResponse obtenerRespuestaAdicionar(int preId, string resRespuesta, bool resEsCorrecta, string resEstado,DateTime resFecha, int mtiId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_RespuestaAdicionar";
            cmd.Parameters.Add("@preId", System.Data.SqlDbType.Int).Value = preId;
            cmd.Parameters.Add("@resRespuesta", System.Data.SqlDbType.NVarChar, 300).Value = resRespuesta;
            cmd.Parameters.Add("@resEsCorrecta", System.Data.SqlDbType.Bit).Value = resEsCorrecta;
            cmd.Parameters.Add("@resEstado", System.Data.SqlDbType.Char, 2).Value = resRespuesta;
            cmd.Parameters.Add("@resFecha", System.Data.SqlDbType.DateTime).Value = resFecha;
            cmd.Parameters.Add("@mtiId", System.Data.SqlDbType.Int).Value = mtiId;

            var respuesta = new clsRespuestaAdicionarResponse();
            var reader = cmd.ExecuteReader();
            respuesta.respuestaAdicionada = true;
            conn.Close();
            return respuesta;
        }
    }
}
