using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mamsheva.Pages
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Page
    {
        List<users> users;
        List<users> lu1;
        public User()
        {
            InitializeComponent();
            users = BaseConnect.BaseModel.users.ToList();
            lbUsersList.ItemsSource = users;
            lbGenderFilter.ItemsSource = BaseConnect.BaseModel.genders.ToList();
            lbGenderFilter.SelectedValuePath = "id";
            lbGenderFilter.DisplayMemberPath = "gender";
            lu1 = users;
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
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            lbUsersList.ItemsSource = users;
            lbGenderFilter.SelectedIndex = -1; //сбрасываем выбранный элемент списка
            txtNameFilter.Clear();
            txtDO.Clear();
            txtOT.Clear();
        }
        int currentpage = 1;
        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;//определяем, какой текстовый блок был нажат
            int countlist = users.Count;//определяем общее колисество пользователей
            int countpage = Convert.ToInt32(txtPageCount.Text);//количество записей на странице
            int countpages = countlist / countpage;//количество страниц
            //изменение номера страници при нажатии на кнопку
            switch (tb.Uid)// в зависимости от нажатой кнопки
            {
                case "prev":
                    currentpage--;
                    break;
                case "1":
                    if (currentpage < 3) currentpage = 1;
                    else if (currentpage > countpages - 2) currentpage = countpages - 4;
                    else currentpage -= 2;
                    break;
                case "2":
                    if (currentpage < 3) currentpage = 2;
                    else if (currentpage > countpages - 2) currentpage = countpages - 3;
                    else currentpage--;
                    break;
                case "3":
                    if (currentpage < 3) currentpage = 3;
                    else if (currentpage > countpages - 2) currentpage = countpages - 2;
                    break;
                case "4":
                    if (currentpage < 3) currentpage = 4;
                    else if (currentpage > countpages - 2) currentpage = countpages - 1;
                    else currentpage++;
                    break;
                case "5":
                    if (currentpage < 3) currentpage = 5;
                    else if (currentpage > countpages - 2) currentpage = countpages;
                    else currentpage += 2;
                    break;
                case "next":
                    currentpage++;
                    break;
                default:
                    currentpage = 1;
                    break;
            }
            //ограничение на выход за пределы страницы
            if (currentpage < 1) currentpage = 1;
            if (currentpage >= countpages) currentpage = countpages;

            //изменение номера страницы в значении текстового поля (отрисовка)
            if (currentpage < 3)
            {
                txt1.Text = " 1 ";
                txt2.Text = " 2 ";
                txt3.Text = " 3 ";
                txt4.Text = " 4 ";
                txt5.Text = " 5 ";
            }
            else if (currentpage > countpages - 2)
            {
                txt1.Text = " " + (countpages - 4).ToString() + " ";
                txt2.Text = " " + (countpages - 3).ToString() + " ";
                txt3.Text = " " + (countpages - 2).ToString() + " ";
                txt4.Text = " " + (countpages - 1).ToString() + " ";
                txt5.Text = " " + (countpages).ToString() + " ";
            }
            else
            {
                txt1.Text = " " + (currentpage - 2).ToString() + " ";
                txt2.Text = " " + (currentpage - 1).ToString() + " ";
                txt3.Text = " " + (currentpage).ToString() + " ";
                txt4.Text = " " + (currentpage + 1).ToString() + " ";
                txt5.Text = " " + (currentpage + 2).ToString() + " ";
            }
            txtCurrentPage.Text = "Текущая страница: " + (currentpage).ToString();

            //изменение списка пользователей
            lu1 = users.Skip(currentpage * countpage - countpage).Take(countpage).ToList();
            lbUsersList.ItemsSource = lu1;
        }

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                lu1 = users.Take(Convert.ToInt32(txtPageCount.Text)).ToList();
                lbUsersList.ItemsSource = lu1;
            }
            catch
            {

            }

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
