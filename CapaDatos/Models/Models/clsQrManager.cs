using CapaDatos.Response;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Models
{
    public class clsQrManager
    {
        private readonly ILogger<clsQrManager> _logger;
        public clsQrManager(ILogger<clsQrManager> logger)
        {
            _logger = logger;
        }
        /**
    * string url: Es la ruta a que desea apuntar para la conexion rest. 
    * ejemplo: "https://www.bancred.com.bo/api/v1/APIS"
    * SortedDictionary<string, string> header: Es lo que tiene que enviar por 
    *                     la cabecera excepto lo que 
    *                     Content-Type porque ya está integrado
    *                     en el ejemplo.
    * string method: Es el metodo a utilizar en el servicio HTTP
    * ejemplo: "POST"
    * string filePfx: Es la ubicación exacta donde se encuentra
    *                     el documento.
    * ejemplo: "RutaServidorEmpresa/CertificateEmpresa.pfx"
    * string passPfx: Es la contraseña de seguridad emitida
    *                     por el BCP junto con el certificado no
    *                     confundirse con la contraseña de usuario
    * ejemplo: "ContrasenaDelCertificado0023458"
    * string body: Es lo que se debe enviar en el cuerpo del
    *                     API REST. 
    * ejemplo: "{\"Clave\": \"Valor\"}"
    * string usuario: Es el usuario de la empresa emitida por el
    *                     BCP.
    * ejemplo: "EmpresaExample00"
    * string password: Es la contraseña de seguridad emitida
    *                     por el BCP para la empresa o usuario.
    * ejemplo: "x125da3asd4s56d13asd85"
    */
        /**Funcion de conexión */
        public clsGenerarResponse Conexion(string ruta, SortedDictionary<string, string> header, string method, string filePfx, string passPfx, clsQrModels body, string usuario, string password)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string result = "";
            /**Concatenación del usuario y password */
            string user = usuario + ":" + password;
            /**Codificando de formato de Bytes */
            var enviar = Encoding.Default.GetBytes(user);
            /**Convirtiendo el certificado PFX en formato X509 */
            _logger.LogInformation("Antes de cargar el certificado");
            X509Certificate2 certificate = new X509Certificate2(filePfx, passPfx); _logger.LogInformation("Despues de cargar el certificado");
            /**Empezamos a realizar la conexión con nuestra API */
            _logger.LogInformation("Antes de cargar al webRequest");
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(ruta);
            _logger.LogInformation("Despues de cargar al webRequest");
            /**Adiciona el certificado al servicio HTTP */
            _logger.LogInformation("Antes de adicionar el certificado al servicio HTTP");
            httpWebRequest.ClientCertificates.Add(certificate);
            _logger.LogInformation("Despues de adicionar el certificado al servicio HTTP");
            /**Es el tipo de informacion que se manda y que recive */
            httpWebRequest.ContentType = "application/json";
            /**Se convierte el usuario en Base64 y se adiciona como token al servicio */
            httpWebRequest.Headers.Add(
                "Authorization", "Basic " + Convert.ToBase64String(enviar)
            );
            /**Adicionas requirimientos adicionales a la cabecera */
            foreach (var item in header)
            {
                httpWebRequest.Headers.Add(item.Key, item.Value);
            }
            /**Indicamos el metodo de envio en nuestro servio HTTP */
            httpWebRequest.Method = method;
            /**Adjuntamos el cuerpo a nuestro servicio HTTP */
            using (StreamWriter streamWriter = new StreamWriter(
                   httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(body));
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                /**Conexion y respuesta de nuestro servicio */
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                /**Transformando la respuesta en formato Stream para su lectura */
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                /**Obteniendo la respuesta en formato String */
                result = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (Exception e)
            {
                /**Verifica si existio un error en el servicio HTTP */
                if (e is WebException)
                {
                    /**Obtenine la exception que levanto */
                    WebException webException = e as WebException;
                    /**Buscamos la respuesta que envio el servidor */
                    WebResponse response = webException.Response;
                    /**Transformando la respuesta en formato Stream para su lectura */
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    /**Obteniendo la respuesta en formato String */
                    result = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }
            return JsonConvert.DeserializeObject<clsGenerarResponse>(result);
        }

        public static clsQrConsultResponse ConsultarQR(string ruta, SortedDictionary<string, string> header, string method, string filePfx, string passPfx, clsQrConsult body, string usuario, string password)
        {
            string result = "";
            /**Concatenación del usuario y password */
            string user = usuario + ":" + password;
            /**Codificando de formato de Bytes */
            var enviar = Encoding.Default.GetBytes(user);
            /**Convirtiendo el certificado PFX en formato X509 */
            X509Certificate2 certificate = new X509Certificate2(@filePfx, passPfx);
            /**Empezamos a realizar la conexión con nuestra API */
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(ruta);
            /**Adiciona el certificado al servicio HTTP */
            httpWebRequest.ClientCertificates.Add(certificate);
            /**Es el tipo de informacion que se manda y que recive */
            httpWebRequest.ContentType = "application/json";
            /**Se convierte el usuario en Base64 y se adiciona como token al servicio */
            httpWebRequest.Headers.Add(
                "Authorization", "Basic " + Convert.ToBase64String(enviar)
            );
            /**Adicionas requirimientos adicionales a la cabecera */
            foreach (var item in header)
            {
                httpWebRequest.Headers.Add(item.Key, item.Value);
            }
            /**Indicamos el metodo de envio en nuestro servio HTTP */
            httpWebRequest.Method = method;
            /**Adjuntamos el cuerpo a nuestro servicio HTTP */
            using (StreamWriter streamWriter = new StreamWriter(
                   httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonConvert.SerializeObject(body));
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                /**Conexion y respuesta de nuestro servicio */
                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                /**Transformando la respuesta en formato Stream para su lectura */
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                /**Obteniendo la respuesta en formato String */
                result = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (Exception e)
            {
                /**Verifica si existio un error en el servicio HTTP */
                if (e is WebException)
                {
                    /**Obtenine la exception que levanto */
                    WebException webException = e as WebException;
                    /**Buscamos la respuesta que envio el servidor */
                    WebResponse response = webException.Response;
                    /**Transformando la respuesta en formato Stream para su lectura */
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    /**Obteniendo la respuesta en formato String */
                    result = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }
            return JsonConvert.DeserializeObject<clsQrConsultResponse>(result);
        }
    }
}
