using CapaDatos.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace CapaNegocio.Servicios
{
    public class ApiFacturaEstadoAdicionarService : IApiFacturaEstadoAdicionarService
    {
        private readonly ApplicationDbContext context;
        public ApiFacturaEstadoAdicionarService(ApplicationDbContext context) {
            this.context = context;
        }

        public void Adicionar(string facCodigo, int usuId, float fteLatitud, float fteLongitud)
        {
            SqlConnection conn = (SqlConnection)context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.CommandText = "Api_FacturaEstadoAdicionar";

            cmd.Parameters.Add("@facCodigo", System.Data.SqlDbType.NVarChar, 10).Value = facCodigo;
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;
            cmd.Parameters.Add("@fteLatitud ", System.Data.SqlDbType.Float).Value = fteLatitud;
            cmd.Parameters.Add("@fteLongitud ", System.Data.SqlDbType.Float).Value = fteLongitud;

            var reader = cmd.ExecuteNonQuery();
            conn.Close();
            
            return;
        }
    }
}