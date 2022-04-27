
using System.Collections.Generic;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.Extensions.Options;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.Servicios
{
    public class ApiUsuarioListarActivoService : IApiUsuarioListarActivoService
    {
        private readonly ApplicationDbContextInventario context;

        public ApiUsuarioListarActivoService(ApplicationDbContextInventario context) {
            this.context = context;
        }

        public IEnumerable<ClsUsuarioListarActivoResponse> UsuarioListarActivo()
        {
            SqlConnection conn = (SqlConnection)context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_UsuarioListarActivo";

            var usuarios = new List<ClsUsuarioListarActivoResponse>();
            var reader = cmd.ExecuteReader();

            while (reader.Read()) {
                usuarios.Add(MapToValue(reader));
            }

            conn.Close();
            return usuarios;
        }

        private ClsUsuarioListarActivoResponse MapToValue(SqlDataReader reader)
        {

            return new ClsUsuarioListarActivoResponse()
            {
                
                usuCodigo = reader["usuCodigo"].ToString().Trim(),
                usuNombre = reader["usuNombre"].ToString().Trim(),
            };

        }
    }
}