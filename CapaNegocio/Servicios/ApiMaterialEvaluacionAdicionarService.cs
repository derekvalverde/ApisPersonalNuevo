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
    public class ApiMaterialEvaluacionAdicionarService: IApiMaterialEvaluacionAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiMaterialEvaluacionAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsMaterialEvaluacionAdicionarResponse obtenerMaterialEvaluacionAdicionar(int evaId, string maeNombre, string maeDireccion,  string maeEstado, DateTime maeFecha, int mtiId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialEvaluacionAdicionar";
            cmd.Parameters.Add("@evaId", System.Data.SqlDbType.Int).Value = evaId;
            cmd.Parameters.Add("@maeNombre", System.Data.SqlDbType.NVarChar, 200).Value = maeNombre;
            cmd.Parameters.Add("@maeDireccion", System.Data.SqlDbType.NVarChar, 200).Value = maeDireccion;
            cmd.Parameters.Add("@maeEstado", System.Data.SqlDbType.Char, 2).Value = maeEstado;
            cmd.Parameters.Add("@maeFecha", System.Data.SqlDbType.DateTime).Value = maeFecha;
            cmd.Parameters.Add("@mtiId", System.Data.SqlDbType.Int).Value = mtiId;

            var materialEvaluacion = new clsMaterialEvaluacionAdicionarResponse();
            var reader = cmd.ExecuteReader();
            materialEvaluacion.materialevaluacionAdicionado = true;
            conn.Close();
            return materialEvaluacion;
        }
    }
}
