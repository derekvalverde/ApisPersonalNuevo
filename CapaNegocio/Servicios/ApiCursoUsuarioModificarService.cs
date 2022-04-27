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
    public class ApiCursoUsuarioModificarService: IApiCursoUsuarioModificarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoUsuarioModificarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCursoUsuarioModificarResponse obtenerCursoUsuarioModificar(int cuuId, int usuId, int curId, decimal cuuScore,decimal cuuNota, string cuuResena)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoUsuarioModificar";
            cmd.Parameters.Add("@cuuId", System.Data.SqlDbType.Int).Value = cuuId;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;
            cmd.Parameters.Add("@cuuScore", System.Data.SqlDbType.Decimal).Value = cuuScore;
            cmd.Parameters.Add("@cuuNota", System.Data.SqlDbType.Decimal).Value = cuuNota;
            cmd.Parameters.Add("@cuuResena", System.Data.SqlDbType.VarChar, 500).Value = cuuResena;

            var curso = new clsCursoUsuarioModificarResponse();
            var reader = cmd.ExecuteReader();
            curso.CursoUsuarioModificado = true;
            conn.Close();
            return curso;
        }

    }
}
