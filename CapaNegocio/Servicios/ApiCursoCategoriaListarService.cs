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
    public class ApiCursoCategoriaListarService: IApiCursoCategoriaListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoCategoriaListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCursoCategoriaListarResponse> obtenerCursoCategoriaListar(int curId)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoCategoriaListar";
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;

            var evaluacion = new List<clsCursoCategoriaListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                evaluacion.Add(MapToValue(reader));
            }
            conn.Close();

            if (evaluacion == null)
            {
                return null;
            }

            return evaluacion;

        }
        private clsCursoCategoriaListarResponse MapToValue(SqlDataReader reader)
        {
            return new clsCursoCategoriaListarResponse()
            {
                catId = Convert.ToInt32(reader["catId"]),
                catNombre = reader["catNombre"].ToString().Trim(),
                catImagenDireccion = reader["catImagenDireccion"].ToString().Trim(),
                catColor = reader["catColor"].ToString().Trim(),
                curId = Convert.ToInt32(reader["curId"]),                
            };

        }
    }
}
