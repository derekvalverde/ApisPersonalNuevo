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
   public class ApiMaterialLeccionAdicionarService: IApiMaterialLeccionAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiMaterialLeccionAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsMaterialLeccionAdicionarResponse registrarMaterialLeccion(int lecId, string malNombre, string malDireccion, int mtiId )
        {
            //puedes hac
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_MaterialLeccionAdicionar";

            cmd.Parameters.Add("@lecId", System.Data.SqlDbType.Int).Value = lecId;
            cmd.Parameters.Add("@malNombre", System.Data.SqlDbType.NVarChar, 200).Value = malNombre;
            cmd.Parameters.Add("@malDireccion", System.Data.SqlDbType.NVarChar, 200).Value = malDireccion;
            cmd.Parameters.Add("@mtiId", System.Data.SqlDbType.Int).Value = mtiId;

        var material = new clsMaterialLeccionAdicionarResponse();
        var reader = cmd.ExecuteReader();
            material.materialLeccionAdicionar = true;
            conn.Close();
            return material;
        }

    }
 }

