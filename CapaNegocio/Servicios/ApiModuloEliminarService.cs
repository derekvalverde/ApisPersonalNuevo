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
    public class ApiModuloEliminarService: IApiModuloEliminarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiModuloEliminarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsModuloEliminarResponse obtenerModuloEliminar(int modId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ModuloEliminar";

            cmd.Parameters.Add("@modId", System.Data.SqlDbType.Int).Value = modId;

            var curso = new clsModuloEliminarResponse();
            var reader = cmd.ExecuteReader();
            curso.eliminado = true;
            conn.Close();
            return curso;
        }
    }
}
