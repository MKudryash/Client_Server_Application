using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mamsheva.Pages
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Page
    {
        List<users> users;
        public User()
        {
            InitializeComponent();
            users = BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = users;
        }
        private void lbTraits_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = Convert.ToInt32(lb.Uid);
            lb.ItemsSource = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == index).ToList();
            lb.DisplayMemberPath = "traits.trait";
        }
        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            int OT = Convert.ToInt32(txtOT.Text) - 1;
            int DO = Convert.ToInt32(txtDO.Text);
            List<users> lu1 = users.Skip(OT).Take(DO - OT).ToList();            
            lbUsersList.ItemsSource = lu1;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            lbUsersList.ItemsSource = users;
            txtDO.Clear();
            txtOT.Clear();
        }
        private void Changebtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int id = Convert.ToInt32(b.Uid);
            auth tUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.id == id);
            LoadPages.MainFrame.Navigate(new ChangeUser(tUser));
        }

        private void Delbtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int id = Convert.ToInt32(b.Uid);
            auth DellUs = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.id == id);
            BaseConnect.BaseModel.auth.Remove(DellUs);
            BaseConnect.BaseModel.SaveChanges();
            MessageBox.Show("Пользователь удален!");
            TimeSpan.FromSeconds(3);
            lbUsersList.ItemsSource = BaseConnect.BaseModel.users.ToList();
        }
        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new Registration());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }
    }
}
