using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class KoncertDaoMySql : KoncertDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from Koncert where idKoncert = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
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

        public List<Koncert> GetAll()
        {
            List<Koncert> lista = new List<Koncert>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from Koncert;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Koncert(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Koncert GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from Koncert where idKoncert = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Koncert koncert = new Koncert(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3));
                    return koncert;
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

        public int Insert(Koncert koncert)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into Koncert values (0,@naziv,@mjesto,@datum)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@naziv", koncert.Naziv);
                    command.Parameters.AddWithValue("@mjesto", koncert.Mjesto);
                    command.Parameters.AddWithValue("@datum", koncert.Datum);
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

        public int Update(Koncert koncert)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"update Koncert set naziv=@naziv,mjesto=@mjesto,datum=@datum where idKoncert = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@naziv", koncert.Naziv);
                    command.Parameters.AddWithValue("@mjesto", koncert.Mjesto);
                    command.Parameters.AddWithValue("@datum", koncert.Datum);
                    command.Parameters.AddWithValue("@id", koncert.IdKoncert);
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
    }
}
