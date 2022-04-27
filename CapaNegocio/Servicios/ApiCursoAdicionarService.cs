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
   public class ApiCursoAdicionarService: IApiCursoAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCursoAdicionarResponse obtenerCursoAdicionar(int ticId, string curTitulo, string curImagenDireccion, string curDescripcion, int curDuracionHoras, string curEstado, int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoAdicionar";
            cmd.Parameters.Add("@ticId", System.Data.SqlDbType.Int).Value = ticId;
            cmd.Parameters.Add("@curTitulo", System.Data.SqlDbType.NVarChar, 100).Value = curTitulo;
            cmd.Parameters.Add("@curImagenDireccion", System.Data.SqlDbType.NVarChar, 200).Value = curImagenDireccion;
            cmd.Parameters.Add("@curDescripcion", System.Data.SqlDbType.NVarChar, 300).Value = curDescripcion;
            cmd.Parameters.Add("@curDuracionHoras", System.Data.SqlDbType.Int).Value = curDuracionHoras;
            cmd.Parameters.Add("@curEstado", System.Data.SqlDbType.Char, 2).Value = curEstado;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            var curso = new clsCursoAdicionarResponse();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                curso = MapToValue(reader);
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (curso == null)
            {
                return null;
            }

            return curso;

        }
        private clsCursoAdicionarResponse MapToValue(SqlDataReader reader)
        {

            return new clsCursoAdicionarResponse()
            {

                curId = Convert.ToInt32(reader["curId"]),
              

            };

        }


    }

    
}
