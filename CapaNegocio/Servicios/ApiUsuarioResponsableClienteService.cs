using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CapaDatos.Data;
using CapaDatos.Response;
using CapaNegocio.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CapaNegocio.Servicios
{
    public class ApiUsuarioResponsableClienteService: IApiUsuarioResponsableClienteService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiUsuarioResponsableClienteService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsUsuarioResponsableClienteResponse> obtenerUsuarioResponsableCliente(string cliCodigo)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_UsuarioResponzableCliente";
            cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.Text).Value = cliCodigo;


            var usuario = new List<clsUsuarioResponsableClienteResponse>();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                usuario.Add(MapToValue(reader));
            }
            conn.Close();
            //
            //Si el cheque no tiene elementos
            if (usuario == null)
            {
                return null;
            }

            return usuario;
        }
        private clsUsuarioResponsableClienteResponse MapToValue(SqlDataReader reader)
        {

            return new clsUsuarioResponsableClienteResponse()
            {
                usuId = Convert.ToInt32(reader["usuId"]),
                usuNombre = reader["usuNombre"].ToString().Trim(),
                uscNombre = reader["uscNombre"].ToString().Trim(),
                gepDetalle = reader["gepDetalle"].ToString().Trim(),
                usuCorreo = reader["usuCorreo"].ToString().Trim(),              
                usuCelular = reader["usuCelular"].ToString().Trim(),
              
            };

        }
    }
}
