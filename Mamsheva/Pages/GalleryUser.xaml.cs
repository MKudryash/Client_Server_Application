using System;
using System.Collections.Generic;
using System.IO;
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

namespace Mamsheva.Pages
{
    /// <summary>
    /// Логика взаимодействия для GalleryUser.xaml
    /// </summary>
    public partial class GalleryUser : Page
    {
        public List<usersimage> UI { get; set; }
        public users U { get; set; }

        public int ind;
        int userImg;
        public GalleryUser(auth CurrentUser)
        {
            InitializeComponent();
            ind = CurrentUser.id;
            userImg = 0;

        }
        private void UserImage_Loaded(object sender, RoutedEventArgs e)
        {

            U = BaseConnect.BaseModel.users.FirstOrDefault(x => x.id == ind);
            UI = BaseConnect.BaseModel.usersimage.Where(x => x.id_user == ind).ToList();
            UI.Reverse();
            BitmapImage BI = new BitmapImage();
            try
            {
                if (UI != null)
                {
                    if (UI[userImg].path != null)
                    {
                        BI = new BitmapImage(new Uri(UI[userImg].path, UriKind.Relative));
                    }
                    else
                    {
                        BI.BeginInit();
                        BI.StreamSource = new MemoryStream(UI[userImg].image);
                        BI.EndInit();
                    }
                    UserImage.Source = BI;
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
            }
            catch { }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            Button btnSender = (Button)sender;

            if (btnSender.Uid == "BtnNext") 
                userImg++;
            else 
                userImg--;


            if (userImg >= UI.Count - 1) 
                userImg = UI.Count - 1;
            if (userImg <= 0) 
                userImg = 0;

            BitmapImage BI = new BitmapImage();
            if (U != null)
            {
                try
                {
                    if (UI.Count != 0)
                    {
                        if (UI[userImg].path != null)
                        {
                            BI = new BitmapImage(new Uri(UI[userImg].path, UriKind.Relative));
                        }
                        else
                        {
                            BI.BeginInit();
                            BI.StreamSource = new MemoryStream(UI[userImg].image);
                            BI.EndInit();
                        }
                    }
                    UserImage.Source = BI;
                }
                catch { MessageBox.Show("Что-то пошло не так"); }
            }
        }

        private void Avatar_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < UI.Count; i++)
            {
                UI[i].avatar = false;
            }
            UI[userImg].avatar = true;
            MessageBox.Show("Аватар успешно изменён!");
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }
    }
}
