using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Mamsheva.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangeUser.xaml
    /// </summary>
    public partial class ChangeUser : Page
    {
        auth CurrentUser;
        int ind;
        public ChangeUser(auth CurrentUser)
        {
            InitializeComponent();
           this.CurrentUser =  CurrentUser;
            ind = CurrentUser.id;
            try
            {
                txtLogin.Text = CurrentUser.login;
                txtPass.Password = CurrentUser.password;
                txtName.Text = CurrentUser.users.name;
                dtDr.SelectedDate = CurrentUser.users.dr;
                int i = 0;
                string[] traits = new string[3];
                List<users_to_traits> LUTT = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == CurrentUser.id).ToList();
                List<traits> traits1 = BaseConnect.BaseModel.traits.ToList();
                foreach (traits tr in traits1)
                {
                    traits[i] = tr.trait;
                    i++;
                }            
                d1.Content = traits[0];
                d2.Content = traits[1];
                d3.Content = traits[2];

                foreach (users_to_traits tr in LUTT)
                {
                    if (d1.Content == tr.traits.trait)
                    {
                        d1.IsChecked = true;
                    }
                    if (d2.Content == tr.traits.trait)
                    {
                        d2.IsChecked = true;
                    }
                    if (d3.Content == tr.traits.trait)
                    {
                        d3.IsChecked = true;
                    }
                }
                listGenders.ItemsSource = BaseConnect.BaseModel.genders.ToList();
                listGenders.SelectedValuePath = "id";
                listGenders.DisplayMemberPath = "gender";
                listGenders.SelectedIndex = CurrentUser.users.genders.id - 1;
            }
            catch
            {
                MessageBox.Show("Информации о вас нет в системе");
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }
        public static void Create(auth User, int i)
        {
            users_to_traits UTT = new users_to_traits();
            UTT.id_user = User.id;
            UTT.id_trait = i;
            BaseConnect.BaseModel.users_to_traits.Add(UTT);
        }
        private void UserImage_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Image IMG = sender as System.Windows.Controls.Image;
            users U = BaseConnect.BaseModel.users.FirstOrDefault(x => x.id == ind);
            usersimage UI = BaseConnect.BaseModel.usersimage.FirstOrDefault(x => x.id_user == ind&&x.avatar==true);
            BitmapImage BI = new BitmapImage();
            if (UI != null&& UI.avatar==true)
            {
                if (UI.path != null)
                {
                    BI = new BitmapImage(new Uri(UI.path, UriKind.Relative));
                }
                else
                {
                    BI.BeginInit();
                    BI.StreamSource = new MemoryStream(UI.image);
                    BI.EndInit();
                }
            }
            else
            {
                switch (U.gender)
                {
                    case 1:
                        BI = new BitmapImage(new Uri(@"/Image/man.png", UriKind.Relative));
                        break;
                    case 2:
                        BI = new BitmapImage(new Uri(@"/Image/woman.png", UriKind.Relative));
                        break;
                    default:
                        BI = new BitmapImage(new Uri(@"/Image/other.png", UriKind.Relative));
                        break;
                }
            }
            IMG.Source = BI;
        }
        private void BtmAddImage_Click(object sender, RoutedEventArgs e)
        {
            Button BTN = (Button)sender;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".jpg";
            openFileDialog.Filter = "Изображения |*.jpg;*.png";
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                System.Drawing.Image UserImage = System.Drawing.Image.FromFile(openFileDialog.FileName);
                ImageConverter IC = new ImageConverter();
                byte[] ByteArr = (byte[])IC.ConvertTo(UserImage, typeof(byte[]));
                usersimage UI = new usersimage() { id_user = ind, image = ByteArr };
                BaseConnect.BaseModel.usersimage.Add(UI);
                BaseConnect.BaseModel.SaveChanges();
                MessageBox.Show("Картинка пользователя добавлена в базу");
            }
            else
            {
                MessageBox.Show("Операция выбора изображения отменена");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            auth User = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.login == CurrentUser.login && x.password == CurrentUser.password);
            users mbUser = BaseConnect.BaseModel.users.FirstOrDefault(x => x.id == User.id);
            if (mbUser == null)
            {
                users newUser = new users { id = User.id, name = txtName.Text, dr = (DateTime)dtDr.SelectedDate, gender = (int)listGenders.SelectedValue };
                BaseConnect.BaseModel.users.Add(newUser);
                if (d1.IsChecked == true)
                {
                    users_to_traits UTT = new users_to_traits();
                    UTT.id_user = newUser.id;
                    UTT.id_trait = 1;
                    BaseConnect.BaseModel.users_to_traits.Add(UTT);
                }
                if (d2.IsChecked == true)
                {
                    users_to_traits UTT = new users_to_traits();
                    UTT.id_user = newUser.id;
                    UTT.id_trait = 2;
                    BaseConnect.BaseModel.users_to_traits.Add(UTT);
                }
                if (d3.IsChecked == true)
                {
                    users_to_traits UTT = new users_to_traits();
                    UTT.id_user = newUser.id;
                    UTT.id_trait = 3;
                    BaseConnect.BaseModel.users_to_traits.Add(UTT);
                }
                BaseConnect.BaseModel.SaveChanges();
            }
            else
            {
                mbUser.dr = (DateTime)dtDr.SelectedDate;
                mbUser.name = txtName.Text;
                User.login = txtLogin.Text;
                User.password = txtPass.Password;
                users_to_traits trait1 = BaseConnect.BaseModel.users_to_traits.FirstOrDefault(x => x.id_user == User.id && x.id_trait == 1);
                users_to_traits trait2 = BaseConnect.BaseModel.users_to_traits.FirstOrDefault(x => x.id_user == User.id && x.id_trait == 2);
                users_to_traits trait3 = BaseConnect.BaseModel.users_to_traits.FirstOrDefault(x => x.id_user == User.id && x.id_trait == 3);
                try
                {
                    if (listGenders.SelectedItem != null)
                    {
                        mbUser.gender = (int)listGenders.SelectedValue;
                    }
                    if (d1.IsChecked == false && trait1 != null)
                    {
                        BaseConnect.BaseModel.users_to_traits.Remove(trait1);
                    }
                    else if (d1.IsChecked == true && trait1 == null)
                    {
                        Create(User, 1);
                    }
                    if (d2.IsChecked == false && trait2 != null)
                    {
                        BaseConnect.BaseModel.users_to_traits.Remove(trait2);
                    }
                    else if (d2.IsChecked == true && trait2 == null)
                    {
                        Create(User, 2);
                    }
                    if (d3.IsChecked == false && trait3 != null)
                    {
                        BaseConnect.BaseModel.users_to_traits.Remove(trait3);
                    }
                    else if (d3.IsChecked == true && trait3 == null)
                    {
                        Create(User, 3);
                    }
                    BaseConnect.BaseModel.SaveChanges();
                    MessageBox.Show("Изменения успешно внесены");
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                }
            }
        }

        private void BtnGalery_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new GalleryUser(CurrentUser));
        }
    }
}
