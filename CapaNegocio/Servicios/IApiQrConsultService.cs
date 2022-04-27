using CapaDatos.Response;


namespace CapaNegocio.Servicios
{
    public interface IApiQrConsultService
    {
        clsQrConsultResponse consultarQR(string idQr);
    }
}
