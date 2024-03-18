using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SecondLab
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            EventsDGR.ItemsSource = context.circus_events.ToList();
            EventDate.SelectedDate = DateTime.Now;
        }

        private circusEntities context = new circusEntities();
        
        private void Date(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = EventDate.SelectedDate ?? DateTime.Today;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            circus_events newEvent = new circus_events();
            newEvent.title = EventName.Text;
            newEvent.event_date = (DateTime)EventDate.SelectedDate;

            if (decimal.TryParse(EventPrice.Text, out decimal parsedPrice))
            {
                newEvent.price = decimal.Round(parsedPrice, 2);
            }
            else
            {
                MessageBox.Show("Введите корректную цену.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            context.circus_events.Add(newEvent);
            context.SaveChanges();
            EventsDGR.ItemsSource = context.circus_events.ToList();

            EventName.Clear();
            EventPrice.Clear();
        }
        private void EventsDGR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventsDGR.SelectedItem != null)
            {
                var selected = EventsDGR.SelectedItem as circus_events;
                EventName.Text = selected.title;
                EventDate.SelectedDate = selected.event_date;
                EventPrice.Text = selected.price.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (EventsDGR.SelectedItem != null)
            {
                var selected = EventsDGR.SelectedItem as circus_events;
                selected.title = EventName.Text;
                selected.event_date = (DateTime)EventDate.SelectedDate;

                if (decimal.TryParse(EventPrice.Text, out decimal parsedPrice))
                {
                    selected.price = decimal.Round(parsedPrice, 2);
                }
                else
                {
                    MessageBox.Show("Введите корректную цену.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                context.SaveChanges();
                EventsDGR.ItemsSource = context.circus_events.ToList();

                EventName.Clear();
                EventPrice.Clear();
            }
            else
            {
                MessageBox.Show("Выберите событие для изменения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (EventsDGR.SelectedItem != null)
            {
                context.circus_events.Remove(EventsDGR.SelectedItem as circus_events);

                context.SaveChanges();
                EventsDGR.ItemsSource = context.circus_events.ToList();

                EventName.Clear();
                EventPrice.Clear();
            }
            else
            {
                MessageBox.Show("Выберите событие для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
