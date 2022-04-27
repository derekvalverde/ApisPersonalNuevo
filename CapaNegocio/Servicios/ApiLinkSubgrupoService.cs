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
using System.Linq;

namespace CapaNegocio.Servicios
{
    public class ApiLinkSubgrupoService : IApiLinkSubgrupoService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationDbContext _context;
        public ApiLinkSubgrupoService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<clsLinkSubgrupoCabeceraResponse> obtenerLinkSubgrupo(int sgrId)
        {

            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "Api_LinkSubgrupo";

            cmd.Parameters.Add("@sgrId", System.Data.SqlDbType.Int).Value = sgrId;


            var TodosLoslinks = new List<clsLinkSubgrupoResponse>();
            var reader = cmd.ExecuteReader();



            while (reader.Read())
            {
                TodosLoslinks.Add(MapToValue(reader));
            }
            conn.Close();


            //En link ya tengo el listado de links ahora debo partir en cabecera y detalle
            var linkCabecera = new List<clsLinkSubgrupoCabeceraResponse>();


            foreach (clsLinkSubgrupoResponse obj in TodosLoslinks)
            {
                List<clsLinkSubgruposubMenuResponse> linkSubmenu = new List<clsLinkSubgruposubMenuResponse>();
                foreach (clsLinkSubgrupoResponse objSubmenu in TodosLoslinks)
                {
                    if (obj.likCabecera == objSubmenu.likCabecera && obj.likCabecera != objSubmenu.likNombre)
                    {
                        //Este es un submenu
                        clsLinkSubgruposubMenuResponse submenu = new clsLinkSubgruposubMenuResponse();
                        submenu.likId = objSubmenu.likId;
                        submenu.likCabecera = objSubmenu.likCabecera;
                        submenu.likAlias = objSubmenu.likAlias;
                        submenu.likDireccion = objSubmenu.likDireccion;
                        submenu.likNombre = objSubmenu.likNombre;
                        linkSubmenu.Add(submenu);
                    }


                    //Si contador es 1 entonces es un solo link sin submenus}
                    //SI Contador es mayor 2 tiene submenus
                }
                var esta = linkCabecera.FirstOrDefault(x => x.likCabecera == obj.likCabecera);
                if (esta == null)
                {//Inserto a mi lista de menus el link con detalle vacio
                    clsLinkSubgrupoCabeceraResponse NuevoLink = new clsLinkSubgrupoCabeceraResponse();
                    NuevoLink.likId = obj.likId;
                    NuevoLink.likCabecera = obj.likCabecera;
                    NuevoLink.likAlias = obj.likAlias;
                    NuevoLink.likDireccion = obj.likDireccion;
                    NuevoLink.likNombre = obj.likNombre;
                    NuevoLink.subMenu = linkSubmenu;
                    linkCabecera.Add(NuevoLink);
                }



            }


            //

            if (linkCabecera == null)
            {
                return null;
            }

            return linkCabecera;

        }
        private clsLinkSubgrupoResponse MapToValue(SqlDataReader reader)
        {

            return new clsLinkSubgrupoResponse()
            {
                likId = Convert.ToInt32(reader["likId"]),
                likCabecera = reader["likCabecera"].ToString().Trim(),

                likNombre = reader["likNombre"].ToString().Trim(),

                likAlias = reader["likAlias"].ToString().Trim(),

                likDireccion = reader["likDireccion"].ToString().Trim(),

            };

        }
        /*
        List<clsLinkSubgrupoCabeceraResponse> IApiLinkSubgrupoService.obtenerLinkSubgrupo(int sgrId)
        {
            throw new NotImplementedException();
        }
        */
    }
}
