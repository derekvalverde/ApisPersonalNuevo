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
   public class ApiModuloModificarService: IApiModuloModificarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiModuloModificarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsModuloModificarResponse obtenerModuloModificar(int modId, string modTitulo, string modDescripcion, string modImagenDireccion)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ModuloModificar";
            cmd.Parameters.Add("@modId", System.Data.SqlDbType.Int).Value = modId;
            
            cmd.Parameters.Add("@modTitulo", System.Data.SqlDbType.NVarChar, 100).Value = modTitulo;
            cmd.Parameters.Add("@modDescripcion", System.Data.SqlDbType.NVarChar, 350).Value = modDescripcion;
            cmd.Parameters.Add("@modImagenDireccion", System.Data.SqlDbType.NVarChar, 200).Value = modImagenDireccion;
           
            var modulo = new clsModuloModificarResponse();
            var reader = cmd.ExecuteReader();
            modulo.moduloModificado = true;
            conn.Close();
            return modulo;
        }

    }
}
