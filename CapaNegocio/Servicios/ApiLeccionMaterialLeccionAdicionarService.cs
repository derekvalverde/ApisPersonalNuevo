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
    public class ApiLeccionMaterialLeccionAdicionarService: IApiLeccionMaterialLeccionAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiLeccionMaterialLeccionAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsLeccionAdicionarResponse registrarMaterialLeccion(clsLeccionAdicionarRequest leccion)
        {
            //puedes hac
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_LeccionAdicionar";
            cmd.Parameters.Add("@modId", System.Data.SqlDbType.Int).Value = leccion.modId;
            cmd.Parameters.Add("@lecTitulo", System.Data.SqlDbType.NVarChar, 100).Value = leccion.lecTitulo;
            cmd.Parameters.Add("@lecEstado", System.Data.SqlDbType.Char, 2).Value = leccion.lecEstado;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = leccion.usuId;

            var reader = cmd.ExecuteReader();


            int lecId = 0;

            while (reader.Read())
            {
                lecId = Convert.ToInt32(reader["lecId"]);
            }

            //detalle


            foreach (clsMaterialLeccionAdicionarRequest material in leccion.materialAdicionar)
            {
                SqlCommand cmd4 = conn.CreateCommand();
                cmd4.CommandType = System.Data.CommandType.StoredProcedure;

                cmd4.CommandText = "Api_MaterialLeccionAdicionar";

                cmd4.Parameters.Add("@lecId", System.Data.SqlDbType.Int).Value = lecId;
                cmd4.Parameters.Add("@malNombre", System.Data.SqlDbType.NVarChar, 200).Value = material.malNombre;
                cmd4.Parameters.Add("@malDireccion", System.Data.SqlDbType.NVarChar, 200).Value = material.malDireccion;
                cmd4.Parameters.Add("@mtiId", System.Data.SqlDbType.Int).Value = material.mtiId;

                reader = cmd4.ExecuteReader();

            }

            clsLeccionAdicionarResponse cursoCategoria = new clsLeccionAdicionarResponse();
            cursoCategoria.lecId = lecId;
            conn.Close();
            return cursoCategoria;

        }
    }
}
