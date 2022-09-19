using GalaSoft.MvvmLight.Command;
using ListaAlumnosUdpServidor.Models;
using ListaAlumnosUdpServidor.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace ListaAlumnosUdpServidor.ViewModels
{
    public class PersonasViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<string> ListaNombres1 { get; set; } = new();

        public ObservableCollection<string> ListaNombres2 { get; set; } = new();

        PersonaService? server;



        public ICommand IniciarCommand { get; set; }

        public ICommand DetenerCommand { get; set; }

        //Cross Thread Calls
        Dispatcher dispatcher;

        public PersonasViewModel()
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            server = new();
            server.PersonaRecibida += Server_PersonaRecibida;
            IniciarCommand = new RelayCommand(Iniciar);
            DetenerCommand = new RelayCommand(Detener);
            

        }

        private void Detener()
        {
            if (server != null)
                server.Detener();
        }

        private void Iniciar()
        {
            if (server != null)
                server.Iniciar();
        }

        private void Server_PersonaRecibida(Persona obj)
        {

            dispatcher.Invoke(()=>{
                if (obj.Lista == "Lista 1")
                {
                    if (!ListaNombres1.Contains(obj.Nombre))
                        ListaNombres1.Add(obj.Nombre);

                    ListaNombres2.Remove(obj.Nombre);
                }
                else
                {
                    if (!ListaNombres2.Contains(obj.Nombre))
                        ListaNombres2.Add(obj.Nombre);

                    ListaNombres1.Remove(obj.Nombre);
                }

            });

            



        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
