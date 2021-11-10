using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Mamsheva.Pages
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Page
    {
        List<users> users;
        List<users> lu1;
        ChangePage changePage = new ChangePage();
        public User()
        {
            InitializeComponent();
            users = BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = users;
            lbGenderFilter.ItemsSource = BaseConnect.BaseModel.genders.ToList();
            lbGenderFilter.SelectedValuePath = "id";
            lbGenderFilter.DisplayMemberPath = "gender";
            lu1 = users;
            DataContext = changePage;
        }
        private void lbTraits_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = Convert.ToInt32(lb.Uid);
            lb.ItemsSource = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == index).ToList();
            lb.DisplayMemberPath = "traits.trait";
        }
        private void Filter(object sender, RoutedEventArgs e)
        {
            try
            {
                int OT = Convert.ToInt32(txtOT.Text) - 1;
                int DO = Convert.ToInt32(txtDO.Text);
                lu1 = users.Skip(OT).Take(DO - OT).ToList();
            }
            catch
            {
              
            }
            if (lbGenderFilter.SelectedValue != null)
            {
                lu1 = lu1.Where(x => x.gender == (int)lbGenderFilter.SelectedValue).ToList();
            }
            if (txtNameFilter.Text != "")
            {
                lu1 = lu1.Where(x => x.name.Contains(txtNameFilter.Text)).ToList();
            }
            lbUsersList.ItemsSource = lu1;
            changePage.Countlist = lu1.Count;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        { 
            lbUsersList.ItemsSource = users;
            lu1 = users;
            lbGenderFilter.SelectedIndex = -1; 
            txtNameFilter.Clear();
            txtDO.Clear();
            txtOT.Clear();
        }
        int currentpage = 1;
        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            switch (tb.Uid)
            {
                case "prev":
                    changePage.CurrentPage--;
                    break;
                case "next":
                    changePage.CurrentPage++;
                    break;
                default:
                    changePage.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }

            lbUsersList.ItemsSource = lu1.Skip(changePage.CurrentPage * changePage.CountPage - changePage.CountPage).Take(changePage.CountPage).ToList();

            txtCurrentPage.Text = "Текущая страница: " + (changePage.CurrentPage).ToString();
        }

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                changePage.CountPage = Convert.ToInt32(txtPageCount.Text);
            }
            catch
            {
                changePage.CountPage = lu1.Count;
            }
            changePage.Countlist = users.Count;
            lbUsersList.ItemsSource = lu1.Skip(0).Take(changePage.CountPage).ToList();

        }
        private void UserImage_Loaded(object sender, RoutedEventArgs e)
        {
            Image IMG = sender as Image;
            int ind = Convert.ToInt32(IMG.Uid);
            users U = BaseConnect.BaseModel.users.FirstOrDefault(x => x.id == ind);
            BitmapImage BI;
            switch (U.gender)
            {
                case 1:
                    BI = new BitmapImage(new Uri(@"/Image/man.png", UriKind.Relative));
                    break;
                case 2:
                    BI = new BitmapImage(new Uri(@"/Image/woman.png", UriKind.Relative));
                    break;
                default:
                    BI = new BitmapImage(new Uri(@"/Image/Other.png", UriKind.Relative));
                    break;
            }

            IMG.Source = BI;//помещаем картинку в image
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
    }
}
