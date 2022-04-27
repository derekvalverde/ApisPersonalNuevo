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
    public class ApiCursoUsuarioEliminarService: IApiCursoUsuarioEliminarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoUsuarioEliminarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCursoUsuarioEliminarResponse obtenerCursoUsuarioEliminar(int usuId, int curId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoUsuarioEliminar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;

            var curso = new clsCursoUsuarioEliminarResponse();
            var reader = cmd.ExecuteReader();
            curso.eliminado = true;
            conn.Close();
            return curso;
        }
    }
}
