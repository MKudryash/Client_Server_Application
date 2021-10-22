using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mamsheva.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
            listGenders.ItemsSource = BaseConnect.BaseModel.genders.ToList();
            listGenders.SelectedValuePath = "id";
            listGenders.DisplayMemberPath = "gender";

            lbTarits.ItemsSource = BaseConnect.BaseModel.traits.ToList();
            lbTarits.SelectedValuePath = "id";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            auth logPass = new auth() { login = txtLogin.Text, password = txtPass.Password, role = 2 };
            BaseConnect.BaseModel.auth.Add(logPass);
            BaseConnect.BaseModel.SaveChanges();
            users User = new users() { name = txtName.Text, id = logPass.id, gender = (int)listGenders.SelectedValue, dr = (DateTime)dtDr.SelectedDate };
            BaseConnect.BaseModel.users.Add(User);


            foreach (traits t in lbTarits.SelectedItems)
            {
                users_to_traits UTT = new users_to_traits();
                UTT.id_user = User.id;
                UTT.id_trait = t.id;
                BaseConnect.BaseModel.users_to_traits.Add(UTT);
            }
            BaseConnect.BaseModel.SaveChanges();
            MessageBox.Show("Данные записаны успешно");
        }
    }
}
