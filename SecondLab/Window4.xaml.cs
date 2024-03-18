using SecondLab.circusDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SecondLab
{
    /// <summary>
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        customersTableAdapter customers = new customersTableAdapter();

        public Window4()
        {
            InitializeComponent();
            Customers.ItemsSource = customers.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            if (!Regex.IsMatch(Email.Text, emailPattern))
            {
                MessageBox.Show("Введите корректный адрес электронной почты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            customers.InsertQuery(FirstName.Text, Surname.Text, Email.Text);
            Customers.ItemsSource = customers.GetData();

            FirstName.Clear();
            Surname.Clear();
            Email.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Customers.SelectedItem != null)
            {
                string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
                if (!Regex.IsMatch(Email.Text, emailPattern))
                {
                    MessageBox.Show("Введите корректный адрес электронной почты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                object id = (Customers.SelectedItem as DataRowView).Row[0];

                customers.UpdateQuery(FirstName.Text, Surname.Text, Email.Text, Convert.ToInt32(id));
                Customers.ItemsSource = customers.GetData();

                FirstName.Clear();
                Surname.Clear();
                Email.Clear();
            }
            else
            {
                MessageBox.Show("Выберите событие для изменения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Customers.SelectedItem != null)
            {
                object id = (Customers.SelectedItem as DataRowView).Row[0];

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранное событие?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    customers.DeleteQuery(Convert.ToInt32(id));
                    Customers.ItemsSource = customers.GetData();
                }
            }
            else
            {
                MessageBox.Show("Выберите событие для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
