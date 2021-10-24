using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mamsheva.Pages
{
    /// <summary>
    /// Логика взаимодействия для Info.xaml
    /// </summary>
    public partial class Info : Page
    {
       
        public Info(auth CurrentUser)
        {
            InitializeComponent();
            try
            {
                tbName.Text = CurrentUser.users.name;
                tbDR.Text = CurrentUser.users.dr.ToString("yyyy MMMM dd");
                tbGender.Text = CurrentUser.users.genders.gender;
                List<users_to_traits> LUTT = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == CurrentUser.id).ToList();
                foreach (users_to_traits UT in LUTT)
                {
                    tbTraits.Text += UT.traits.trait + "; ";
                }
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
    }
}
