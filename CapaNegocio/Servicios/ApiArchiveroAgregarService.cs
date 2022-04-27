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

namespace CapaNegocio.Servicios
{
    public class ApiArchiveroAgregarService:IApiArchiveroAgregarService
    {

        private readonly AppSettings _appSettings;
        private readonly ApicacionDbContextCorrespondencia _context;
        public ApiArchiveroAgregarService(IOptions<AppSettings> appSettings, ApicacionDbContextCorrespondencia context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsArchiveroAgregarResponse obteneArchiveroAgregar(string UsuCodigo, string Codigo, string Nombre, string Descripcion, int AreId)
        {
            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.CommandText = "pa_ArchiveroAgregar";

            cmd.Parameters.Add("@UsuCodigo", System.Data.SqlDbType.VarChar, 20).Value = UsuCodigo;
            cmd.Parameters.Add("@Codigo", System.Data.SqlDbType.VarChar, 20).Value = Codigo;
            cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 100).Value = Nombre;
            cmd.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar, 100).Value = Descripcion;
            cmd.Parameters.Add("@AreId", System.Data.SqlDbType.Int).Value = AreId;
            //var informacion = new List<clsArchiveroAgregarResponse>();
            var reader = cmd.ExecuteReader();

            clsArchiveroAgregarResponse informacion = new clsArchiveroAgregarResponse();
            informacion.agregado = "Agregado";
            while (reader.Read()){
                informacion.agregado = "No tiene permisos para agregar a esta área";
            }
            conn.Close();

            return informacion;

        }
       

    }
}
