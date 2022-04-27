using CapaDatos.Data;
using CapaDatos.Models.Response;
using CapaDatos.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapaNegocio.Servicios
{
    public class ApiCategoriaListarService: IApiCategoriaListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCategoriaListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsCategoriaListarResponse> obtenerCategoria()
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CategoriaListar";

            var categoria = new List<clsCategoriaListarResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                categoria.Add(MapToValue(reader));
            }
            conn.Close();

            if (categoria == null)
            {
                return null;
            }

            return categoria;

        }
        private clsCategoriaListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsCategoriaListarResponse()
            {

                catId = Convert.ToInt32(reader["catId"]),
                catNombre = reader["catNombre"].ToString().Trim(),
                catColor = reader["catColor"].ToString().Trim(),
                catImagenDireccion = reader["catImagenDireccion"].ToString().Trim()
                
            };

        }
    }
}
