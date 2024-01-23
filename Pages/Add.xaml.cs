using savichev23pr.Classes;
using savichev23pr.Models;
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
using System.Xml.Linq;

namespace savichev23pr.Pages
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Gift Gift;
        public Add(Gift Gift = null)
        {
            InitializeComponent();
            if (Gift != null)
            {
                this.Gift = Gift;
                tb_fio.Text = this.Gift.fio;
                tb_message.Text = this.Gift.message;
                tb_adress.Text = this.Gift.adress;
                tb_datetime.Text = this.Gift.dt.ToString("dd.MM.yyyy HH:mm:ss");
                tb_email.Text = this.Gift.email;
                bthAdd.Content = "Изменить";
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.main);
        }

        private void AddGift(object sender, RoutedEventArgs e)
        {
            if (tb_fio.Text.Length == 0)
            {
                MessageBox.Show("Укажите ФИО");
                return;
            }
            if (tb_message.Text.Length == 0)
            {
                MessageBox.Show("Укажите сообщение");
                return;
            }
            if (tb_adress.Text.Length == 0)
            {
                MessageBox.Show("Укажите адрес");
                return;
            }
            if (tb_datetime.Text.Length == 0)
            {
                MessageBox.Show("Укажите дату и время");
                return;
            }
            if (tb_email.Text.Length == 0)
            {
                MessageBox.Show("Укажите почту");
                return;
            }
            if (Gift == null) // если входящий документ отсутству
            {
                GiftContext newGift = new GiftContext();
                newGift.fio = tb_fio.Text;
                newGift.message = tb_message.Text;
                newGift.adress = tb_adress.Text;
                DateTime newDate = new DateTime();
                DateTime.TryParse(tb_datetime.Text, out newDate);
                newGift.dt = newDate;
                newGift.email = tb_email.Text;
                newGift.Save();
                MessageBox.Show("Документ добавлен.");
            }
            else
            {
                GiftContext newGift = new GiftContext();
                newGift.id = Gift.id;
                newGift.fio = tb_fio.Text;
                newGift.message = tb_message.Text;
                newGift.adress = tb_adress.Text;
                DateTime newDate = new DateTime();
                DateTime.TryParse(tb_datetime.Text, out newDate);
                newGift.dt = newDate;
                newGift.email = tb_email.Text;
                newGift.Save(true);
                MessageBox.Show("Документ изменён.");
            }
            MainWindow.init.AllGifts = new GiftContext().AllGifts();
            MainWindow.init.OpenPages(MainWindow.pages.main);
        }
    }
}
