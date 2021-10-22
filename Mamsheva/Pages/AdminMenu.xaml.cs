using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mamsheva.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Page
    {
        public AdminMenu()
        {
            InitializeComponent();
            dgUsers.ItemsSource = BaseConnect.BaseModel.auth.ToList();

        }

        private void btnSaveCahanges_Click(object sender, RoutedEventArgs e)
        {
            BaseConnect.BaseModel.SaveChanges();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            auth SelectedUser = (auth)dgUsers.SelectedItem;
            BaseConnect.BaseModel.auth.Remove(SelectedUser);
            BaseConnect.BaseModel.SaveChanges();
            MessageBox.Show("Выбранный пользователь удален");
            dgUsers.ItemsSource = BaseConnect.BaseModel.auth.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
