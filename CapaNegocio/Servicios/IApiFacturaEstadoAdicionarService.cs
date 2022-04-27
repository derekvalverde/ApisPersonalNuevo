namespace CapaNegocio.Servicios
{
    public interface IApiFacturaEstadoAdicionarService
    {
         void Adicionar(string facCodigo, int usuId, float fteLatitud, float fteLongitud);
    }
}