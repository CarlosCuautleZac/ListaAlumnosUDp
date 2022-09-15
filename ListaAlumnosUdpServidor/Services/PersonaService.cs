using ListaAlumnosUdpServidor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ListaAlumnosUdpServidor.Services
{
    public class PersonaService
    {
        Thread? hilo;
        UdpClient? client;

        public void Iniciar()
        {
            if (hilo == null)
            {
                hilo = new Thread(new ThreadStart(Recibir));
                hilo.Start();

            }
        }

        public event Action<Persona>? PersonaRecibida;


        void Recibir()//Soy un subproceso
        {
            client = new UdpClient(10001);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, 10001);

            while (true)
            {         
                byte[] buffer = client.Receive(ref ipe);
                string json = Encoding.UTF8.GetString(buffer);
                Persona? persona = JsonConvert.DeserializeObject<Persona>(json);

                if (persona != null)
                    PersonaRecibida?.Invoke(persona);
            }

        }

        public void Detener()
        {
            if (hilo != null && client != null)
                client.Close();
        }
    }
}
