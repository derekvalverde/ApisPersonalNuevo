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
    public class ApiCursoUsuarioResponsableAdicionarService: IApiCursoUsuarioResponsableAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly AplicacionDbContextCampusVirtual _context;
        public ApiCursoUsuarioResponsableAdicionarService(IOptions<AppSettings> appSettings, AplicacionDbContextCampusVirtual context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsCursoUsuarioResponsableAdicionarResponse obtenerCursoUsuarioResponsableAdicionar(int usuId, int curId, string curEstado)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CursoUsuarioResponsableAdicionar";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@curId", System.Data.SqlDbType.Int).Value = curId;
            cmd.Parameters.Add("@curEstado", System.Data.SqlDbType.Char, 2).Value = curEstado;
           

            var curso = new clsCursoUsuarioResponsableAdicionarResponse();
            var reader = cmd.ExecuteReader();
            curso.usuarioResponsableAdicionado = true;
            conn.Close();
            return curso;
        }

    }
}
