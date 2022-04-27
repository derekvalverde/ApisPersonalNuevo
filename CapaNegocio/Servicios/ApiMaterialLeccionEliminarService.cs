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
    public class ApiMaterialLeccionEliminarService: IApiMaterialLeccionEliminarService
    {

        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiMaterialLeccionEliminarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsMaterialLeccionEliminarResponse obtenerMaterialLeccionEliminar(int malId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialLeccionEliminar";

            cmd.Parameters.Add("@malId", System.Data.SqlDbType.Int).Value = malId;

            var leccion = new clsMaterialLeccionEliminarResponse();
            var reader = cmd.ExecuteReader();
            leccion.eliminado = true;
            conn.Close();
            return leccion;
        }
    }
}
