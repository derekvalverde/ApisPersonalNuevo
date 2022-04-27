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
using CapaDatos.Request;

namespace CapaNegocio.Servicios
{
    public class ApiDeudaCompensacionEstadoAdicionarService : IApiDeudaCompensacionAdicionarEstadoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiDeudaCompensacionEstadoAdicionarService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsDeudaCompensacionAdicionarResponse registrarDeudaCompensacionEstado(clsDeudaCompensacionAdicionarEstadoCabeceraRequest deuda)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd1 = conn.CreateCommand();
            conn.Open();
            cmd1.CommandType = System.Data.CommandType.StoredProcedure;
            cmd1.CommandText = "Api_DeudaCompensacionAdicionarEstado";
            cmd1.Parameters.Add("@recId", System.Data.SqlDbType.Int).Value = deuda.recId;
            cmd1.Parameters.Add("@recManual", System.Data.SqlDbType.Int).Value = deuda.recManual;
            cmd1.Parameters.Add("@cliCodigo", System.Data.SqlDbType.NVarChar, 80).Value = deuda.cliCodigo;
            cmd1.Parameters.Add(new SqlParameter("@decMonto", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = deuda.decMonto;
            cmd1.Parameters.Add("@decFecha", System.Data.SqlDbType.DateTime).Value = deuda.decFecha;
            cmd1.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = deuda.usuId;
            cmd1.Parameters.Add("@decEstado", System.Data.SqlDbType.Char,2).Value = deuda.decEstado;
            
            var reader = cmd1.ExecuteReader();
            

            int decId = 0;

            while (reader.Read())
            {
                decId = Convert.ToInt32(reader["decId"]);
            }
            //detalle
            if (decId > 0)
            {
                
                foreach (clsDeudaCompensacionadicionarDetalleRequest deudaDetalle in deuda.detalleDeudaCompensacion)
                {
                    SqlCommand cmd4 = conn.CreateCommand();
                    cmd4.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd4.CommandText = "Api_DeudaCompensacionDetalleAdicionar";


                    cmd4.Parameters.Add("@decId", System.Data.SqlDbType.Int).Value = decId;
                    cmd4.Parameters.Add("@recId", System.Data.SqlDbType.Int).Value = deudaDetalle.recId;
                    cmd4.Parameters.Add("@clcId", System.Data.SqlDbType.Int).Value = deudaDetalle.clcId;
                    cmd4.Parameters.Add("@facCodigo", System.Data.SqlDbType.NVarChar, 80).Value =deudaDetalle.facCodigo;
                    cmd4.Parameters.Add("@facOrigen", System.Data.SqlDbType.Char, 1).Value = deudaDetalle.facOrigen;
                    
                   
                    cmd4.Parameters.Add(new SqlParameter("@dedMonto", System.Data.SqlDbType.Decimal) { Precision = 18, Scale = 2 }).Value = deudaDetalle.dedMonto;
                    cmd4.Parameters.Add("@dedNumero", System.Data.SqlDbType.NVarChar, 80).Value = deudaDetalle.dedNumero;
                    cmd4.Parameters.Add("@banCodigo", System.Data.SqlDbType.Char, 3).Value = deudaDetalle.banCodigo;
                   
                    reader = cmd4.ExecuteReader();

                   
                }
            }
            conn.Close();
            clsDeudaCompensacionAdicionarResponse respuesta = new clsDeudaCompensacionAdicionarResponse();
            respuesta.decId = decId;
            return respuesta;
            
        }
    }
}
