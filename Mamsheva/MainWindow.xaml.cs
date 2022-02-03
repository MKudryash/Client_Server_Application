using Mamsheva.Pages;
using System.Windows;

namespace Mamsheva
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frmMain.Navigate(new DiagramPage());
            LoadPages.MainFrame = frmMain;
            BaseConnect.BaseModel = new Entities();
        }
    }
}
