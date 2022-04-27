
using Microsoft.Data.SqlClient;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace CapaNegocio.Servicios
{
    public class ApiClienteUbicacionAdicionarService:IApiClienteUbicacionAdicionarService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiClienteUbicacionAdicionarService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsClienteUbicacionAdicionaResponse obtenerUbicacionAdicionar(string cliCodigo, int usuId, float cluLatitud, float cluLongitud)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ClienteUbicacionAdicionar";
            cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.VarChar, 10).Value = cliCodigo;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@cluLatitud", System.Data.SqlDbType.Float).Value = cluLatitud;
            cmd.Parameters.Add("@cluLongitud", System.Data.SqlDbType.Float).Value = cluLongitud;


            var ubicacion = new clsClienteUbicacionAdicionaResponse();
            var reader = cmd.ExecuteReader();
            ubicacion.ubicacionAdicionada = true;

            conn.Close();

            return ubicacion;

        }
    }
}
