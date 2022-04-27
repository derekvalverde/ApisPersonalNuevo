using Microsoft.Data.SqlClient;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CapaNegocio.Servicios
{
    public class ApiClienteAdicionalEstadoService: IApiClienteAdicionalEstadoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteAdicionalEstadoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsClienteAdicionalEstadoRsponse obtenerEstado(string cliCodigo, string caaEstado)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ClienteAdicionalEstado";
            cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.NVarChar, 10).Value = cliCodigo;
            cmd.Parameters.Add("@caaEstado ", System.Data.SqlDbType.Char, 2).Value = caaEstado;


            var estado = new clsClienteAdicionalEstadoRsponse();
            var reader = cmd.ExecuteReader();

            estado.registrado = true;

            conn.Close();
            return estado;

        }
    }
}
