
using Microsoft.Data.SqlClient;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CapaNegocio.Servicios
{
    public class ApiClienteAdicionalAdicionarService: IApiClienteAdicionalAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteAdicionalAdicionarService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsClienteAdicionalAdicionarResponse obtenerClienteAdicionar(string cliCodigo, string caaEmail)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ClienteAdicionalAdicionar";
            cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.NVarChar, 10).Value = cliCodigo;
            cmd.Parameters.Add("@caaEmail", System.Data.SqlDbType.NVarChar, 50).Value = caaEmail;
            
            var email = new clsClienteAdicionalAdicionarResponse();
            var reader = cmd.ExecuteReader();
            email.Registrado = true;

            conn.Close();

            return email;

        }
    }
}
