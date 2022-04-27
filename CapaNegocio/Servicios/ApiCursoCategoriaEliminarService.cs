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
    public class ApiCursoCategoriaEliminarService: IApiCursoCategoriaEliminarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoCategoriaEliminarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCursoCategoriaEliminarResponse obtenerCursoCategoriaEliminar(int catId, int curId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoCategoriaEliminar";
            cmd.Parameters.Add("@catId", System.Data.SqlDbType.Int).Value = catId;
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;

            var cursoCategoria = new clsCursoCategoriaEliminarResponse();
            var reader = cmd.ExecuteReader();
            cursoCategoria.eliminado = true;
            conn.Close();
            return cursoCategoria;
        }
    }
}
