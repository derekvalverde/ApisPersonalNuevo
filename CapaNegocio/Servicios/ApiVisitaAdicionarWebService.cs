using System;
using System.Collections.Generic;
using CapaDatos.Data;
using CapaDatos.Response;
using Microsoft.Extensions.Options;
using WebIntiApi.Models;
using CapaDatos.Request;
using CapaDatos.Models;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CapaNegocio.Servicios
{
    public class ApiVisitaAdicionarWebService:IApiVisitaAdicionarWebService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiVisitaAdicionarWebService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public clsVisitaAdicionarWebResponse obtenerVisita(int usuId)
        {

            //
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_VisitaAdicionarWeb";
            cmd.Parameters.Add("@usuId", System.Data.SqlDbType.Int).Value = usuId;


            var visita = new clsVisitaAdicionarWebResponse();
            var reader = cmd.ExecuteReader();

            
                visita.visitaAdicionado = true;
            
            conn.Close();

            return visita;

        }
    }
}
