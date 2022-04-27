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
    public class ApiCursoIdCategoriaAdicionarService: IApiCursoIdCategoriaAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoIdCategoriaAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCursoAdicionarResponse registrarCursoIdCategoria(clsCursoAdicionarRequest curso)
        {
            //puedes hac
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoAdicionar";
            cmd.Parameters.Add("@ticId", System.Data.SqlDbType.Int).Value = curso.ticId;
            cmd.Parameters.Add("@curTitulo", System.Data.SqlDbType.NVarChar, 100).Value = curso.curTitulo;
            cmd.Parameters.Add("@curImagenDireccion", System.Data.SqlDbType.NVarChar, 200).Value = curso.curImagenDireccion;
            cmd.Parameters.Add("@curDescripcion", System.Data.SqlDbType.NVarChar, 300).Value = curso.curDescripcion;
            cmd.Parameters.Add("@curDuracionHoras", System.Data.SqlDbType.Int).Value = curso.curDuracionHoras;
            cmd.Parameters.Add("@curEstado", System.Data.SqlDbType.Char, 2).Value = curso.curEstado;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = curso.usuId;

            var reader = cmd.ExecuteReader();


            int curId = 0;

            while (reader.Read())
            {
                curId = Convert.ToInt32(reader["curId"]);
            }

            //detalle


            foreach (clsCursoCategoriaAdicionarRequest categoria in curso.categoriaAdicionar)
            {
                SqlCommand cmd4 = conn.CreateCommand();
                cmd4.CommandType = System.Data.CommandType.StoredProcedure;

                cmd4.CommandText = "Api_CursoCategoriaAdicionar";

                cmd4.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;
                cmd4.Parameters.Add("@catId", System.Data.SqlDbType.Int).Value = categoria.catId;
                reader = cmd4.ExecuteReader();

            }

            clsCursoAdicionarResponse cursoCategoria = new clsCursoAdicionarResponse();
            cursoCategoria.curId = curId;
            conn.Close();
            return cursoCategoria;

        }
    }
}
