using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class KoncertNastupDaoMySql : KoncertNastupDao
    {
        public int Delete(int idKoncert, int idNastup)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from KoncertNastup where idKoncert = @id and idNastup=@id2;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", idKoncert);
                    command.Parameters.AddWithValue("@id2", idNastup);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.RecordsAffected > 0)
                        return 0;
                    else
                        return 6;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return 5;
                }
                finally
                {
                    ConnectionPool.ReleaseConnection(connection);
                }
            }
        }

        public List<KoncertNastup> GetAll()
        {
            throw new NotImplementedException();
        }

        public KoncertNastup GetById(int idKoncert, int idNastup)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from KoncertNastup where idKoncert = @id and idNastup=@id2;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", idKoncert);
                    command.Parameters.AddWithValue("@id2", idNastup);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    KoncertNastup koncertNastup = new KoncertNastup(reader.GetInt32(0), reader.GetInt32(1));
                    return koncertNastup;
                }
                catch (MySqlException)
                {
                    return null;
                }
                finally
                {
                    ConnectionPool.ReleaseConnection(connection);
                }
            }
        }

        public int Insert(Koncert koncert, Nastup nastup)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into KoncertNastup values (@idNastup,@idKoncert)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idNastup", nastup.IdNastup);
                    command.Parameters.AddWithValue("@idKoncert", koncert.IdKoncert);
                    MySqlDataReader reader = command.ExecuteReader();
                    return 0;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.Message.Contains("Neispravan format email-a")) return 1;
                    else if (ex.Message.Contains("Neispravan format unesenog imena ili prezimena")) return 2;
                    else if (ex.Message.Contains("Neispravan format maticnog broja")) return 3;
                    else if (ex.Message.Contains("Vrijednost mora biti veca od 0")) return 4;
                    else return 5;
                }
                finally
                {
                    ConnectionPool.ReleaseConnection(connection);
                }
            }
        }

        public int Update(Koncert koncert, Nastup nastup)
        {
            throw new NotImplementedException();
        }
    }
}
