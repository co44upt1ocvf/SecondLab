using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SecondLab.circusDataSetTableAdapters;

namespace SecondLab
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        circus_eventsTableAdapter circus_events = new circus_eventsTableAdapter();

        public Window3()
        {
            InitializeComponent();
            Events.ItemsSource = circus_events.GetData();
            EventDate.SelectedDate = DateTime.Now;
        }
        private void Date(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = EventDate.SelectedDate ?? DateTime.Today;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal price = decimal.Parse(EventPrice.Text);

                if (price.ToString("0.00") != EventPrice.Text)
                {
                    MessageBox.Show("Цена должна иметь формат (10,2).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                circus_events.InsertQuery(EventName.Text, EventDate.Text, price);
                Events.ItemsSource = circus_events.GetData();

                EventName.Clear();
                EventPrice.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректное значение цены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Events.SelectedItem != null)
            {
                object id = (Events.SelectedItem as DataRowView).Row[0];

                try
                {
                    decimal price = decimal.Parse(EventPrice.Text);

                    if (price.ToString("0.00") != EventPrice.Text)
                    {
                        MessageBox.Show("Цена должна иметь формат (10,2).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    circus_events.UpdateQuery(EventName.Text, EventDate.Text, price, Convert.ToInt32(id));
                    Events.ItemsSource = circus_events.GetData();

                    EventName.Clear();
                    EventPrice.Clear();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Введите корректное значение цены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите событие для изменения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Events.SelectedItem != null)
            {
                object id = (Events.SelectedItem as DataRowView).Row[0];

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранное событие?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    circus_events.DeleteQuery(Convert.ToInt32(id));
                    Events.ItemsSource = circus_events.GetData();
                }
            }
            else
            {
                MessageBox.Show("Выберите событие для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
