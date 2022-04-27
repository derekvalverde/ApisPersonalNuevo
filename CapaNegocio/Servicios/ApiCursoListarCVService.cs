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
    public class ApiCursoListarCVService: IApiCursoListarCVService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoListarCVService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCursoListarCVResponse> obtenerCursoListar(int usuId, string curEstado)
        {
            
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoListar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@curEstado", System.Data.SqlDbType.Char, 3).Value = curEstado;
            var curso = new List<clsCursoListarCVResponse>();
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
        private clsCursoListarCVResponse MapToValue(SqlDataReader reader)
        {

            return new clsCursoListarCVResponse()
            {
                curId = Convert.ToInt32(reader["curId"]),
                curTitulo = reader["curTitulo"].ToString().Trim(),
                curDescripcion = reader["curDescripcion"].ToString().Trim(),
                curDuracionHoras = Convert.ToInt32(reader["curDuracionHoras"]),
                curFecha = Convert.ToDateTime(reader["curFecha"]),
                curImagenDireccion = reader["curImagenDireccion"].ToString().Trim(),
                curScore = Convert.ToDecimal(reader["curScore"]),
                
                ticId = Convert.ToInt32(reader["ticId"]),
                nroInscritos = Convert.ToInt32(reader["nroInscritos"]),
               
            };

        }
    }
}
