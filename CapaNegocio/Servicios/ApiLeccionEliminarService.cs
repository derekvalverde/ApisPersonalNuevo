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
    public class ApiLeccionEliminarService: IApiLeccionEliminarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiLeccionEliminarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsLeccionEliminarResponse obtenerLeccionEliminar(int lecId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_LeccionEliminar";

            cmd.Parameters.Add("@lecId", System.Data.SqlDbType.Int).Value = lecId;

            var leccion = new clsLeccionEliminarResponse();
            var reader = cmd.ExecuteReader();
            leccion.eliminado = true;
            conn.Close();
            return leccion;
        }
    }
}
