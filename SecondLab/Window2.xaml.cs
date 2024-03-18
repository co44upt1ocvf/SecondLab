using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            CustomersDGR.ItemsSource = context.customers.ToList();
        }

        private circusEntities context = new circusEntities();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            customers newCustomer = new customers();
            newCustomer.firstname = FirstName.Text;
            newCustomer.surnamre = Surname.Text;

            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            if (!Regex.IsMatch(Email.Text, emailPattern))
            {
                MessageBox.Show("Введите корректный адрес электронной почты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            newCustomer.email = Email.Text;

            context.customers.Add(newCustomer);
            context.SaveChanges();
            CustomersDGR.ItemsSource = context.customers.ToList();

            FirstName.Clear();
            Surname.Clear();
            Email.Clear();
        }

        private void CustomersDGR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomersDGR.SelectedItem != null)
            {
                var selected = CustomersDGR.SelectedItem as customers;
                FirstName.Text = selected.firstname;
                Surname.Text = selected.surnamre;
                Email.Text = selected.email;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (CustomersDGR.SelectedItem != null)
            {
                var selected = CustomersDGR.SelectedItem as customers;
                selected.firstname = FirstName.Text;
                selected.surnamre = Surname.Text;

                string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
                if (!Regex.IsMatch(Email.Text, emailPattern))
                {
                    MessageBox.Show("Введите корректный адрес электронной почты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                selected.email = Email.Text;

                context.SaveChanges();
                CustomersDGR.ItemsSource = context.customers.ToList();

                FirstName.Clear();
                Surname.Clear();
                Email.Clear();
            }
            else
            {
                MessageBox.Show("Выберите клиента для изменения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (CustomersDGR.SelectedItem != null)
            {
                context.customers.Remove(CustomersDGR.SelectedItem as customers);

                context.SaveChanges();
                CustomersDGR.ItemsSource = context.customers.ToList();

                FirstName.Clear();
                Surname.Clear();
                Email.Clear();
            }
            else
            {
                MessageBox.Show("Выберите клиента для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
