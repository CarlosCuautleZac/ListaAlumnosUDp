using ListaAlumnosUdpServidor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaAlumnosUdpServidor.ViewModels
{
    public class PersonasViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<string> ListaNombres1 { get; set; } = new();

        public ObservableCollection<string> ListaNombres2 { get; set; } = new();





        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
