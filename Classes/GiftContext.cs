using savichev23pr.Interfaces;
using savichev23pr.Models;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace savichev23pr.Classes
{
    public class GiftContext : Gift, IGift
    {
        public List<GiftContext> AllGifts()
        {
            List<GiftContext> allGifts = new List<GiftContext>();

            OleDbConnection connection = Common.DBConnection.Connection();
            OleDbDataReader dataGifts = Common.DBConnection.Query("SELECT * FROM [Заказы]", connection);
            while (dataGifts.Read())
            {
                GiftContext newGift = new GiftContext();
                newGift.id = dataGifts.GetInt32(0);
                newGift.fio = dataGifts.GetString(1);
                newGift.message = dataGifts.GetString(2);
                newGift.adress = dataGifts.GetString(3);
                newGift.dt = dataGifts.GetDateTime(4);
                newGift.email = dataGifts.GetString(5);

                allGifts.Add(newGift);
            }
            Common.DBConnection.CloseConnection(connection);

            return allGifts;
        }

        public void Save(bool Update = false)
        {
            if (Update)
            {
                OleDbConnection connection = Common.DBConnection.Connection();
                Common.DBConnection.Query("UPDATE [Заказы] " +
                    "SET " + $"[ФИО] = '{this.fio}', " +
                    $"[Сообщение] = '{this.message}', " +
                    $"[Адрес] = '{this.adress}', " +
                    $"[Дата и время] = '{this.dt.ToString("dd.MM.yyyy HH:mm:ss")}', " +
                    $"[Почта] = '{this.email}' " +
                    $"WHERE [Код] = {this.id}", connection);
                Common.DBConnection.CloseConnection(connection);
            }
            else
            {
                OleDbConnection connection = Common.DBConnection.Connection();
                Common.DBConnection.Query("INSERT INTO " +
                    "[Заказы]" +
                    "([ФИО], " +
                    "[Сообщение], " +
                    "[Адрес], " +
                    "[Дата и время], " +
                    "[Почта])" + "VALUES (" +
                    $"'{this.fio}', " +
                    $"'{this.message}', " +
                    $"'{this.adress}', " +
                    $"'{this.dt.ToString("dd.MM.yyyy HH:mm:ss")}', " +
                    $"'{this.email}')", connection);

                Common.DBConnection.CloseConnection(connection);
            }
        }

        public void Delete()
        {
            OleDbConnection connection = Common.DBConnection.Connection();
            Common.DBConnection.Query($"DELETE FROM [Заказы] WHERE [Код] = {this.id}", connection);
            Common.DBConnection.CloseConnection(connection);
        }
    }
}
