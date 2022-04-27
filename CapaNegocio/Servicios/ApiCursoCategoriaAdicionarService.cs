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
    public class ApiCursoCategoriaAdicionarService: IApiCursoCategoriaAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoCategoriaAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCursoCategoriaAdicionarResponse obtenerCursocategoriaAdicionar(int catId, int curId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoCategoriaAdicionar";
            cmd.Parameters.Add("@catId", System.Data.SqlDbType.Int).Value = catId;            
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;
      
            var curso = new clsCursoCategoriaAdicionarResponse();
            var reader = cmd.ExecuteReader();

            curso.categoriaAdicionado = true;

            conn.Close();

            return curso;
        }

    }
}
