using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapaNegocio.Hubs
{
    public class clsCallbackHub : Hub
    {
        public async Task EnviarSenalCallback(String State, String Message)
        {
            await Clients.Others.SendAsync("RecibirCallback", State, Message);
        }
    }
}
