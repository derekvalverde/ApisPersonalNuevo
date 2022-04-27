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
    public class ApiModuloAdicionarService: IApiModuloAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiModuloAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsModuloAdicionarResponse obtenerModuloAdicionar(int curId, string modTitulo, string modDescripcion, string modEstado, string modImagenDireccion,  int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ModuloAdicionar";
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;
            cmd.Parameters.Add("@modTitulo", System.Data.SqlDbType.NVarChar, 100).Value = modTitulo;
            cmd.Parameters.Add("@modDescripcion", System.Data.SqlDbType.NVarChar, 350).Value = modDescripcion;       
            cmd.Parameters.Add("@modEstado", System.Data.SqlDbType.Char, 2).Value = modEstado;
            cmd.Parameters.Add("@modImagenDireccion", System.Data.SqlDbType.NVarChar, 200).Value = modImagenDireccion;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;

            var modulo = new clsModuloAdicionarResponse();
            var reader = cmd.ExecuteReader();
            modulo.moduloAdicionado = true;
            conn.Close();
            return modulo;
        }

    }
}
