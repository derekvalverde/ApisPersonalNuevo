using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CapaDatos.Data;
using CapaDatos.Response;
using CapaNegocio.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CapaDatos.Models;
using System.Linq;

namespace CapaNegocio.Servicios
{
    public class ApiMaterialLineaVentaStockBuscadorService:IApiMaterialLineaVentaStockBuscadorService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiMaterialLineaVentaStockBuscadorService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsMaterialLineaVentaStockBuscadorResponce> obtenerMaterialesLineaBuscador(string ageOficina, string linCodigo, string like, string cliCodigo, string aplicacion, string matCodigo, int permiso)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            var buscadorLin = new List<clsMaterialLineaVentaStockBuscadorResponce>();
           

            if (aplicacion == "PP")
            {
                cmd.CommandText = "Api_MaterialVentaPersonalLineaBuscador";
                cmd.Parameters.Add("@matCodigo", System.Data.SqlDbType.NVarChar, 20).Value = matCodigo;
                cmd.Parameters.Add("@ageOficina", System.Data.SqlDbType.Char, 4).Value = ageOficina;
                cmd.Parameters.Add("@linCodigo", System.Data.SqlDbType.Char, 4).Value = linCodigo;
                cmd.Parameters.Add("@permiso", System.Data.SqlDbType.Int).Value = permiso;
            }

            if (aplicacion == "PC")
            {

                cmd.CommandText = "Api_MaterialLineaVentaStockBuscador";
                cmd.Parameters.Add("@ageOficina", System.Data.SqlDbType.VarChar, 4).Value = ageOficina;
                cmd.Parameters.Add("@linCodigo", System.Data.SqlDbType.VarChar, 4).Value = linCodigo;
                cmd.Parameters.Add("@like", System.Data.SqlDbType.VarChar, 50).Value = like;
            }

            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                buscadorLin.Add(MapToValue(reader, aplicacion));
            }
            conn.Close();

            /*ACTUALIZAMOS DEL DESCUENTO*/

            
            /*FIN ACTUALIZAMOS DEL DESCUENTO*/
           
            //Si el buscador no tiene elementos
            if (buscadorLin == null)
            {
                return null;
            }
            //Si existe Material


            return buscadorLin;

        }
        private clsMaterialLineaVentaStockBuscadorResponce MapToValue(SqlDataReader reader, string aplicacion)
        {
            if (aplicacion == "PP")
            {
                return new clsMaterialLineaVentaStockBuscadorResponce()
                {

                    matCodigo = reader["matCodigo"].ToString().Trim(),
                    matNombre = reader["matNombre"].ToString().Trim(),
                    secCodigo = reader["secCodigo"].ToString().Trim(),
                    mavPrecio = Convert.ToDecimal(reader["mavPrecio"]),
                    mavLinea = reader["mavLinea"].ToString().Trim(),
                    mavFamilia = reader["mavFamilia"].ToString().Trim(),
                    mavOrigen = reader["mavOrigen"].ToString().Trim(),
                    mavExistencia = Convert.ToInt32(reader["mavExistencia"]),
                    madDescuento = Convert.ToDecimal(reader["madDescuento"]),
                    matImagen = reader["matImagen"].ToString().Trim(),
                    mprCantidad = Convert.ToInt32(reader["mprCantidad"]),
                };
            }
            else
            {
                return new clsMaterialLineaVentaStockBuscadorResponce()
                {

                    matCodigo = reader["matCodigo"].ToString().Trim(),
                    matNombre = reader["matNombre"].ToString().Trim(),
                    secCodigo = reader["secCodigo"].ToString().Trim(),
                    mavPrecio = Convert.ToDecimal(reader["mavPrecio"]),
                    mavLinea = reader["mavLinea"].ToString().Trim(),
                    mavFamilia = reader["mavFamilia"].ToString().Trim(),
                    mavOrigen = reader["mavOrigen"].ToString().Trim(),
                    mavExistencia = Convert.ToInt32(reader["mavExistencia"]),
                    madDescuento = Convert.ToDecimal(reader["madDescuento"]),
                    matImagen = reader["matImagen"].ToString().Trim(),
                  
                };
            }

                

        }
        /*OBTIENE DESCUENTO*/
        public Decimal ObtieneDescuento(List<clsCondicionCamposAjustarResponse> tabla, string cliCodigo, string prgCodigo, string marGrupoMaterial1, string marGrupoMaterial4, string matCodigo, string ageOficina)
        {
            List<clsMaterialPrecio> tabla1 = new List<clsMaterialPrecio>();
            String[] secuencias = new String[100];
            int c = 0, sw = 0;
            Decimal acumulado = 0;
            String conCodigo = "", acu = "", id = "", descuento = "";

            var objeto = new Dictionary<string, string>();

            objeto["cliCodigo"] = cliCodigo;
            objeto["prgCodigo"] = prgCodigo;// grupo de precios*
            objeto["marGrupoMaterial1"] = marGrupoMaterial1; //linea*
            objeto["marGrupoMaterial4"] = marGrupoMaterial4;//importado* o propio
            objeto["matCodigo"] = matCodigo;//codigo de material
            objeto["ageOficina"] = ageOficina;//oficina
            objeto["secCodigo"] = "00";

            try
            {

                if (tabla.Count > 0)
                {
                    for (int i = 0; i < tabla.Count; i++)
                    {
                        if (tabla.ElementAt(i).cosId.ToString() != id)
                        {
                            if (i != 0 && sw == 0)
                            {
                                secuencias[c] = conCodigo + "|" + acu;
                                c++;
                                descuento = descuento + conCodigo + "-" + acu + "\n";
                                acu = "select top 1 * from MaterialPrecio where cadCodigo='10' and GETDATE() between marFechaInicio and marFechaFin " + acu + " and marClaseCondicion='" + conCodigo + "' and marBorrado<>'X' order by marOrden desc ";
                                tabla1 = verDescuento(acu);
                                if (tabla1.Count > 0)
                                {
                                    descuento = descuento + tabla1.ElementAt(0).marValor.ToString() + "OKKK\n";
                                    acumulado = acumulado + Convert.ToDecimal(tabla1.ElementAt(0).marValor.ToString());
                                    sw = 1;
                                    if (conCodigo == "ZK03")
                                        break;
                                }
                                acu = "";
                            }
                            id = tabla.ElementAt(i).cosId.ToString();
                        }
                        if (tabla.ElementAt(i).cosActivo.ToString() == "1")
                        {
                            acu = acu + " and " + tabla.ElementAt(i).cooCodigo.ToString() + "='" + objeto[tabla.ElementAt(i).cooCodigo.ToString()] + "' ";
                        }
                        else
                        {
                            acu = acu + " and " + tabla.ElementAt(i).cooCodigo.ToString() + "='' ";
                        }


                        if (tabla.ElementAt(i).conCodigo.ToString() != conCodigo)
                        {
                            conCodigo = tabla.ElementAt(i).conCodigo.ToString();
                            sw = 0;
                            acu = " and " + tabla.ElementAt(i).cooCodigo.ToString() + "='" + objeto[tabla.ElementAt(i).cooCodigo.ToString()] + "' ";
                        }
                    }
                    descuento = descuento + conCodigo + "-" + acu + "\n";
                    acu = "select top 1 * from MaterialPrecio where cadCodigo='10' and GETDATE() between marFechaInicio and marFechaFin " + acu + " and marClaseCondicion='" + conCodigo + "' and marBorrado<>'X' order by marOrden desc ";

                    tabla1 = verDescuento(acu);
                    if (tabla1.Count > 0)
                    {
                        descuento = descuento + tabla1.ElementAt(0).marValor.ToString();
                        acumulado = acumulado + Convert.ToDecimal(tabla1.ElementAt(0).marValor.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("aa" + e);
            }
            return acumulado;
        }
        public List<clsCondicionCamposAjustarResponse> obtenerCamposAjustar()
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_CondicionCamposAjustar";

            var ofertas = new List<clsCondicionCamposAjustarResponse>();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ofertas.Add(MapToValueCamposAjustar(reader));
            }
            conn.Close();
            //
            //Si el buscador no tiene elementos
            if (ofertas == null)
            {
                return null;
            }
            //Si existe Material


            return ofertas;

        }
        private clsCondicionCamposAjustarResponse MapToValueCamposAjustar(SqlDataReader reader)
        {
            return new clsCondicionCamposAjustarResponse()
            {
                cosId = Convert.ToInt32(reader["cosId"]),
                cooCodigo = reader["cooCodigo"].ToString().Trim(),
                conCodigo = reader["conCodigo"].ToString().Trim(),
                cosOrden = reader["cosOrden"].ToString().Trim(),
                cooDetalle = reader["cooDetalle"].ToString().Trim(),
                cosActivo = reader["cosActivo"].ToString().Trim(),
            };

        }
        public clsClienteVentaCliCodigoResponse obtenerDatosCliente(string cliCodigo)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_ClienteVentaCliCodigo";
            cmd.Parameters.Add("@cliCodigo", System.Data.SqlDbType.Int).Value = cliCodigo;


            var datosCli = new clsClienteVentaCliCodigoResponse();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                datosCli = MapToValueDatosCliente(reader);
            }
            conn.Close();
            //
            //Si no exixte cliente
            if (datosCli == null)
            {
                return null;
            }
            //Si existe cliente


            return datosCli;

        }
        private clsClienteVentaCliCodigoResponse MapToValueDatosCliente(SqlDataReader reader)
        {

            return new clsClienteVentaCliCodigoResponse()
            {


                cliId = Convert.ToInt32(reader["cliId"]),
                clvOficina = reader["clvOficina"].ToString().Trim(),
                cliCiudad = reader["cliCiudad"].ToString().Trim(),
                cliCodigo = reader["cliCodigo"].ToString().Trim(),
                cloInterlocutor = reader["cloInterlocutor"].ToString().Trim(),
                cliNombreComercial = reader["cliNombreComercial"].ToString().Trim(),
                cliNombreFiscal = reader["cliNombreFiscal"].ToString().Trim(),
                cliRol = reader["cliRol"].ToString().Trim(),
                cliIdentificacionNumero = reader["cliIdentificacionNumero"].ToString().Trim(),
                cliDireccionComercial = reader["cliDireccionComercial"].ToString().Trim(),
                cliTelefono = reader["cliTelefono"].ToString().Trim(),
                cliEmail = reader["cliEmail"].ToString().Trim(),
                cliFechaModificacion = Convert.ToDateTime(reader["cliFechaModificacion"]),
                cadCodigo = reader["cadCodigo"].ToString().Trim(),
                secCodigo = reader["secCodigo"].ToString().Trim(),
                grpCodigo = reader["grpCodigo"].ToString().Trim(),
                clvFormaPago = reader["clvFormaPago"].ToString().Trim(),
                clvZona = reader["clvZona"].ToString().Trim(),
                clvGrupoVendedores = reader["clvGrupoVendedores"].ToString().Trim(),
                clvEstado = reader["clvEstado"].ToString().Trim(),
                prgCodigo = reader["prgCodigo"].ToString().Trim(),
                cheMonto = Convert.ToDecimal(reader["cheMonto"]),



            };

        }

        public List<clsMaterialPrecio> verDescuento(string acu)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = acu;

            var camposAjustar = new List<clsMaterialPrecio>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {

                camposAjustar.Add(MapToValue2(reader));
            }
            conn.Close();
            //
            //Si no tiene elementos
            if (camposAjustar == null)
            {
                return null;
            }
            //Si existe Material


            return camposAjustar;

        }
        private clsMaterialPrecio MapToValue2(SqlDataReader reader)
        {

            return new clsMaterialPrecio()
            {
                marValor = Convert.ToDecimal(reader["marValor"])
            };

        }
        /*FIN OBTIENE DESCUENTO*/
    }
}
