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
   public class ApiCursoModificarService:IApiCursoModificarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoModificarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCursoModificarResponse obtenerCursoModificar(int curId, int ticId, string curTitulo, string curImagenDireccion, string curDescripcion, int curDuracionHoras, string curEstado)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoModificar";  
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;
            cmd.Parameters.Add("@ticId", System.Data.SqlDbType.Int).Value = ticId;
            cmd.Parameters.Add("@curTitulo", System.Data.SqlDbType.NVarChar, 100).Value = curTitulo;
            cmd.Parameters.Add("@curImagenDireccion", System.Data.SqlDbType.NVarChar, 200).Value = curImagenDireccion;
            cmd.Parameters.Add("@curDescripcion", System.Data.SqlDbType.NVarChar, 300).Value = curDescripcion;
            cmd.Parameters.Add("@curDuracionHoras", System.Data.SqlDbType.Int).Value = curDuracionHoras;
            cmd.Parameters.Add("@curEstado", System.Data.SqlDbType.Char, 2).Value = curEstado;
            
            var curso = new clsCursoModificarResponse();
            var reader = cmd.ExecuteReader();
            curso.cursoModificado = true;
            conn.Close();
            return curso;
        }
    }
}
