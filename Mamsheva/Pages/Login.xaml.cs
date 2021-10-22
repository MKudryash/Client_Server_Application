using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mamsheva.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                auth CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.login == txtLogin.Text && x.password == txtPassword.Password);
                if (CurrentUser != null)
                {
                    switch (CurrentUser.role)
                    {
                        case 1:
                            MessageBox.Show("Вы вошли как администратор");
                            LoadPages.MainFrame.Navigate(new AdminMenu());
                            break;
                        case 2:
                        default:
                            MessageBox.Show("Вы вошли как пользователь");
                            LoadPages.MainFrame.Navigate(new Info(CurrentUser));
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Пользователя не существует");
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new Registration());
        }
    }
}
