using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DLLMamsheva;
namespace Mamsheva.Pages
{
    /// <summary>
    /// Логика взаимодействия для Dll.xaml
    /// </summary>
    public partial class Dll : Page
    {
        DLLMamsheva.Dll dll = new DLLMamsheva.Dll();
        public Dll()
        {
            InitializeComponent();
        }

        private void AvageAge_Click(object sender, RoutedEventArgs e)
        {
            List<DateTime> dateTimes = new List<DateTime>();
            List<users> UserForDB = BaseConnect.BaseModel.users.ToList();
            foreach (users u in UserForDB)
            {
                dateTimes.Add(u.dr);
            }
            MessageBox.Show("Срединй возраст пользователей: " + dll.AvageAge(dateTimes));
        }

        private void NamesFilter_Click(object sender, RoutedEventArgs e)
        {
            Names.Text = "";
            List<string> names = new List<string>();
            List<users> users = BaseConnect.BaseModel.users.ToList();
            foreach (users userss in users)
            {
                names.Add(userss.name);
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("Введите значение");
            }
            else
            {
                string FoundName = txtName.Text;
                if (dll.Name(names, FoundName).Count == 0)
                {
                    MessageBox.Show("Ничего не найдено");
                }
                else
                {
                    foreach (string u in dll.Name(names, FoundName))
                    {
                        Names.Text += u + "\n";
                    }
                }
            }
        }
    }
}
