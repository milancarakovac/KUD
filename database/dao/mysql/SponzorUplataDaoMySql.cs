using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class SponzorUplataDaoMySql : SponzorUplataDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from SponzorUplata where idSponzorUplata = @id;";
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

        public List<SponzorUplata> GetAll()
        {
            List<SponzorUplata> lista = new List<SponzorUplata>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from SponzorUplata;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new SponzorUplata(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public SponzorUplata GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from SponzorUplata where idSponzorUplata = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    SponzorUplata sponzorUplata = new SponzorUplata(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                    return sponzorUplata;
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

        public int Insert(SponzorUplata sponzorUplata)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into SponzorUplata values (0,@idUplata,@idSponzor)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idSponzor", sponzorUplata.IdSponzor);
                    command.Parameters.AddWithValue("@idUplata", sponzorUplata.IdUplata);
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

        public int Update(SponzorUplata sponzorUplata)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"update SponzorUplata set idUplata=@idUplata,idSponzor=@idSponzor where idSponzorUplata = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idSponzor", sponzorUplata.IdSponzor);
                    command.Parameters.AddWithValue("@idUplata", sponzorUplata.IdUplata);
                    command.Parameters.AddWithValue("@id", sponzorUplata.IdSponzorUplata);
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
