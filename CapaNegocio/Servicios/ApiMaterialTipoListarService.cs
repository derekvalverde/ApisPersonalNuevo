﻿using CapaDatos.Data;
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
    public class ApiMaterialTipoListarService: IApiMaterialTipoListarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiMaterialTipoListarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialTipoListarResponse> obtenerMaterialTipoListar()
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialTipoListar";

            var categoria = new List<clsMaterialTipoListarResponse>();
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
        private clsMaterialTipoListarResponse MapToValue(SqlDataReader reader)
        {

            return new clsMaterialTipoListarResponse()
            {
                mtiTipo = reader["mtiTipo"].ToString().Trim(),
                mtiDetalle = reader["mtiDetalle"].ToString().Trim(),

            };

        }

    }
}
