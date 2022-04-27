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
   public class ApiCursoEliminarService: IApiCursoEliminarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoEliminarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCursoEliminarResponse obtenerCursoEliminar( int curId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoEliminar";
           
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;

            var curso = new clsCursoEliminarResponse();
            var reader = cmd.ExecuteReader();
            curso.eliminado = true;
            conn.Close();
            return curso;
        }
    }
}
