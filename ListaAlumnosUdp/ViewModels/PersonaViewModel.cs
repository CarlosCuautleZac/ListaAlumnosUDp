using GalaSoft.MvvmLight.Command;
using ListaAlumnosUdp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace ListaAlumnosUdp.ViewModels
{
    internal class PersonaViewModel : INotifyPropertyChanged
    {
        //Propiedades

        private Persona persona;

        public Persona Persona
        {
            get { return persona; }
            set { persona = value; PropertyChanged?.Invoke(PropertyChanged, new PropertyChangedEventArgs(nameof(Persona))); }
        }


        private string ip = "";

        public string IP
        {
            get { return ip; }
            set { ip = value; PropertyChanged?.Invoke(PropertyChanged, new PropertyChangedEventArgs(nameof(IP))); }
        }



        private string error = "";

        public string Error
        {
            get { return error; }
            set { error = value; 
                PropertyChanged?.Invoke(PropertyChanged, new PropertyChangedEventArgs(nameof(Error))); }
        }



        UdpClient client;


        //Si hay un solo comando se puede dejar instanciado directo
        public ICommand EnviarCommand { get; set; }

        public PersonaViewModel()
        {
            Persona = new Persona();
            EnviarCommand = new RelayCommand(Eviar);
            client = new(10002);//Bind - Separar un puerto
        }

        //Metodos

        private void Eviar()
        {
            Error = "";

            //Validar informacion
            if (!IPAddress.TryParse(IP, out IPAddress? direccion))
            {
                Error = "Escriba correctamente la direccion IP del servidor al cual enviar la informacion";
            }

            if (string.IsNullOrWhiteSpace(Persona.Nombre))
            {
                Error = "Escriba el nombre de la persona";

            }

            if (string.IsNullOrWhiteSpace(Persona.Lista))
            {
                Error = "Seleccione la lista";
            }

            

            //Todo bien
            if (Error == "" && direccion != null)
            {

                IPEndPoint ip = new IPEndPoint(direccion, 10001);

                string json = JsonConvert.SerializeObject(Persona);
                byte[] buffer = Encoding.UTF8.GetBytes(json);

                
                client.Send(buffer,buffer.Length,ip);

                Persona.Nombre = "";
                persona.Lista = "";

            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
