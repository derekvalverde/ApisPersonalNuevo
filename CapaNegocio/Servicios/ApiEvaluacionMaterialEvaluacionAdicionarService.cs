using CapaDatos.Data;
using CapaDatos.Models.Request;
using CapaDatos.Models.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public class ApiEvaluacionMaterialEvaluacionAdicionarService: IApiEvaluacionMaterialEvaluacionAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiEvaluacionMaterialEvaluacionAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsEvaluacionAdicionarResponse registrarEvaluacionMaterialEvaluacion(clsEvaluacionAdicionarRequest evaluacion)
        {
            //puedes hac
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_EvaluacionAdicionar";
            cmd.Parameters.Add("@modId", System.Data.SqlDbType.Int).Value = evaluacion.modId;
            cmd.Parameters.Add("@tieId", System.Data.SqlDbType.Int).Value = evaluacion.tieId;
            cmd.Parameters.Add("@evaTitulo", System.Data.SqlDbType.NVarChar, 60).Value = evaluacion.evaTitulo;
            cmd.Parameters.Add("@evaEstado", System.Data.SqlDbType.Char, 2).Value = evaluacion.evaEstado;
            cmd.Parameters.Add("@evaFecha", System.Data.SqlDbType.DateTime).Value = evaluacion.evaFecha;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = evaluacion.usuId;

            var reader = cmd.ExecuteReader();

            int evaId = 0;

            while (reader.Read())
            {
                evaId = Convert.ToInt32(reader["evaId"]);
            }

            //detalle


            foreach (clsMaterialEvaluacionAdicionarRequest material in evaluacion.materialAdicionar)
            {
                SqlCommand cmd4 = conn.CreateCommand();
                cmd4.CommandType = System.Data.CommandType.StoredProcedure;

                cmd4.CommandText = "Api_MaterialEvaluacionAdicionar";

                cmd4.Parameters.Add("@evaId", System.Data.SqlDbType.Int).Value = evaId;
                cmd4.Parameters.Add("@maeNombre", System.Data.SqlDbType.NVarChar, 200).Value = material.maeNombre;
                cmd4.Parameters.Add("@maeDireccion", System.Data.SqlDbType.NVarChar, 200).Value = material.maeDireccion;
                cmd4.Parameters.Add("@maeEstado", System.Data.SqlDbType.Char, 2).Value = material.maeEstado;
                cmd4.Parameters.Add("@maeFecha", System.Data.SqlDbType.DateTime).Value = material.maeFecha;
                cmd4.Parameters.Add("@mtiId", System.Data.SqlDbType.Int).Value = material.mtiId;

                reader = cmd4.ExecuteReader();

            }

            clsEvaluacionAdicionarResponse evaluacionMaterial = new clsEvaluacionAdicionarResponse();
            evaluacionMaterial.evaId = evaId;
            conn.Close();
            return evaluacionMaterial;

        }
    }
}
