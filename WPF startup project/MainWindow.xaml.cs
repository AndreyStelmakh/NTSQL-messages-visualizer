using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPF_startup_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public XDocument Ntsqlmessage
        {
            get
            {
                return _ntsqlmessage;
            }

            set
            {
                _ntsqlmessage = value;

                if(PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Ntsqlmessage"));
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private XDocument _ntsqlmessage;

        public event PropertyChangedEventHandler PropertyChanged;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ntsqlmessage = XDocument.Parse("<err text='Сохранение документа'><err text='Повторяющиеся получатели'><err text='34563'/><err text='234'/><err text='65564'/></err></err>");
        }
    }
}
