using savichev23pr.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace savichev23pr.Elements
{
    /// <summary>
    /// Логика взаимодействия для Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        GiftContext Gift;
        public Item(GiftContext Gift)
        {
            InitializeComponent();
            lFIO.Content = Gift.fio;
            lMessage.Content = $"Сообщение: {Gift.message}";
            lAdress.Content = $"Адрес: {Gift.adress}";
            lDatetime.Content = $"Дата и врема отправки: {Gift.datetime.ToString()}";
            lEmail.Content = $"Почта: {Gift.email}";
            this.Gift = Gift;
        }

        private void EditGift(object sender, RoutedEventArgs e)
        {
            MainWindow.init.frame.Navigate(new Pages.Add(Gift));
        }

        private void DeleteGift(object sender, RoutedEventArgs e)
        {
            Gift.Delete();
            MainWindow.init.AllGifts = new GiftContext().AllGifts();
            MainWindow.init.OpenPages(MainWindow.pages.main);
        }
    }
}
