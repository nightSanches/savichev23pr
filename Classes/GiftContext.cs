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
            OleDbDataReader dataGifts = Common.DBConnection.Query("SELECT * FROM [Документы]", connection);
            while (dataGifts.Read())
            {
                GiftContext newGift = new GiftContext();
                newGift.id = dataGifts.GetInt32(0);
                newGift.fio = dataGifts.GetString(1);
                newGift.message = dataGifts.GetString(2);
                newGift.adress = dataGifts.GetString(3);
                newGift.datetime = dataGifts.GetDateTime(5);
                newGift.email = dataGifts.GetString(6);

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
                Common.DBConnection.Query("UPDATE [Документы] " +
                    "SET " + $"[Изображение] = '{this.src}', " +
                    $"[Наименование] = '{this.name}', " +
                    $"[Ответственный] = '{this.user}', " +
                    $"[Код документа] = '{this.id_document}', " +
                    $"[Дата поступления] = '{this.date.ToString("dd.MM.yyyy")}', " +
                    $"[Статус] = {this.status}, " +
                    $"[Направление] = '{this.vector}' " +
                    $"WHERE [Код] = {this.id}", connection);
                Common.DBConnection.CloseConnection(connection);
            }
            else
            {
                OleDbConnection connection = Common.DBConnection.Connection();
                Common.DBConnection.Query("INSERT INTO " +
                    "[Документы]" +
                    "([Изображение], " +
                    "[Наименование], " +
                    "[Ответственный], " +
                    "[Код документа], " +
                    "[Дата поступления], " +
                    "[Статус], " +
                    "[Направление])" + "VALUES (" +
                    $"'{this.src}', " +
                    $"'{this.name}', " +
                    $"'{this.user}', " +
                    $"'{this.id_document}', " +
                    $"'{this.date.ToString("dd.MM.yyyy")}', " +
                    $"'{this.status}', " +
                    $"'{this.vector}')", connection);

                Common.DBConnection.CloseConnection(connection);
            }
        }

        public void Delete()
        {
            OleDbConnection connection = Common.DBConnection.Connection();
            Common.DBConnection.Query($"DELETE FROM [Документы] WHERE [Код] = {this.id}", connection);
            Common.DBConnection.CloseConnection(connection);
        }
    }
}
