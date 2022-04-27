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
   public class ApiLeccionModificarService: IApiLeccionModificarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiLeccionModificarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsLeccionModificarResponse obtenerLeccionModificar(int lecId, int modId, string lecTitulo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_LeccionModificar";
            cmd.Parameters.Add("@lecId", System.Data.SqlDbType.Int).Value = lecId;
            cmd.Parameters.Add("@modId", System.Data.SqlDbType.Int).Value = modId;
            cmd.Parameters.Add("@lecTitulo", System.Data.SqlDbType.NVarChar, 100).Value = lecTitulo;
            
            var leccion = new clsLeccionModificarResponse();
            var reader = cmd.ExecuteReader();
            leccion.leccionModificado = true;
            conn.Close();
            return leccion;
        }

    }
}
