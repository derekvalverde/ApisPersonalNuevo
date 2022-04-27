using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaDatos.Response
{
    public class clsBannerListarTipoCabeceraResponse
    {
        public int bnnId { get; set; }
        public int bnnTipo { get; set; }
        public string bnnEnlace { get; set; }
        public string bnnImagen { get; set; }
        public List<clsBannerMaterialListarDetalleResponse> detalleBanner { get; set; }
    }
}
