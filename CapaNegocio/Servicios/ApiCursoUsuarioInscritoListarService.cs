using System;
using Microsoft.Data.SqlClient;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CapaDatos.Models.Response;
using System.Collections.Generic;

namespace CapaNegocio.Servicios
{
   public class ApiCursoUsuarioInscritoListarService: IApiCursoUsuarioInscritoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoUsuarioInscritoListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCursoUsuarioInscritoListarResponse> obtenerCursoUsuarioListar(int usuId)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoUsuarioInscritoListar";

            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
           

            var listar = new List<clsCursoUsuarioInscritoListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                listar.Add(MapToValue(reader));
            }
            conn.Close();


            //
            //Si el buscador no tiene elementos
            if (listar == null)
            {
                return null;
            }
            //Si existe Material
            return listar;

        }
        private clsCursoUsuarioInscritoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsCursoUsuarioInscritoListarResponse()
            {
               curId=Convert.ToInt32(reader["curId"]),
               curTitulo = reader["curTitulo"].ToString().Trim(),
                curImagenDireccion = reader["curImagenDireccion"].ToString().Trim(),
                curDescripcion = reader["curDescripcion"].ToString().Trim(),
               curEstado = reader["curEstado"].ToString().Trim(),
               curFecha= Convert.ToDateTime(reader["curFecha"]),
                curScore = Convert.ToDecimal(reader["curScore"]),
                ticNombre = reader["ticNombre"].ToString().Trim(),
                cuuId = Convert.ToInt32(reader["cuuId"]),
            };

        }
    }
}
