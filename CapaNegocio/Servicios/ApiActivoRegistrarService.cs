using System;
using CapaDatos.Data;
using CapaDatos.Request;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.Servicios
{
    public class ApiActivoRegistrarService : IApiActivoRegistrarService
    {
        private readonly ApplicationDbContextInventario context;
        
        public ApiActivoRegistrarService(ApplicationDbContextInventario context)
        {
            this.context = context;
        }
        public ClsActivoRegistrarResponse Registrar(string actCodigo, string actDenominacion, int empId, string usuCodigo)
        {
            SqlConnection conn = (SqlConnection)context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ActivoRegistrar";
            cmd.Parameters.Add("@actCodigo", System.Data.SqlDbType.VarChar,15).Value = actCodigo;
            cmd.Parameters.Add("@actDenominacion", System.Data.SqlDbType.VarChar,250).Value = actDenominacion;
            cmd.Parameters.Add("@empId", System.Data.SqlDbType.Int).Value = empId;
            cmd.Parameters.Add("@usucodigo", System.Data.SqlDbType.VarChar,15).Value = usuCodigo;

            var respuesta = new ClsActivoRegistrarResponse()
            {
                registrado = false
            };

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (Convert.ToInt32(reader["resultado"]) == 1) {
                    respuesta.registrado = true;
                }
            }
            conn.Close();
            return respuesta;
        }
    }
}