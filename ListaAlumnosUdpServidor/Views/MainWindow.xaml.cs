using ListaAlumnosUdpServidor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ListaAlumnosUdpServidor.Views
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((PersonasViewModel)this.DataContext).IniciarCommand.Execute(null);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((PersonasViewModel)this.DataContext).DetenerCommand.Execute(null);
        }
    }
}
