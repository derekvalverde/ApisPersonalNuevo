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
    public class ApiCursoUsuarioNoInscritoBusquedaListarService: IApiCursoUsuarioNoInscritoBusquedaListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoUsuarioNoInscritoBusquedaListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCursoUsuarioNoInscritoBusquedaListarResponse> obtenerCursoUsuarioNoInscritoBusquedaListar(int ageId, int cargoId, string like, int curId)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoUsuarioNoInscritoBusquedaListar";

            cmd.Parameters.Add("@ageId", System.Data.SqlDbType.Int).Value = ageId;
            cmd.Parameters.Add("@cargoId", System.Data.SqlDbType.Int).Value = cargoId;
            cmd.Parameters.Add("@like", System.Data.SqlDbType.NVarChar, 50).Value = like;
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;


            var listar = new List<clsCursoUsuarioNoInscritoBusquedaListarResponse>();
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
        private clsCursoUsuarioNoInscritoBusquedaListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsCursoUsuarioNoInscritoBusquedaListarResponse()
            {
                usuId = Convert.ToInt32(reader["usuId"]),
                usuNombre = reader["usuNombre"].ToString().Trim(),
                
            };

        }
    }
}
