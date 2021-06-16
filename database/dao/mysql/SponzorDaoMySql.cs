using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class SponzorDaoMySql : SponzorDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();

            string query = @"delete from Sponzor where idSponzor = @id;";
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

        public List<Sponzor> GetAll()
        {
            List<Sponzor> lista = new List<Sponzor>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from Sponzor;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Sponzor(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Sponzor GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from Sponzor where idSponzor = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Sponzor sponzor = new Sponzor(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    return sponzor;
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

        public int Insert(Sponzor sponzor)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into Sponzor values (0,@naziv,@brojTelefona,@email)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@naziv", sponzor.Naziv);
                    command.Parameters.AddWithValue("@brojTelefona", sponzor.BrojTelefona);
                    command.Parameters.AddWithValue("@email", sponzor.Email);
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

        public int Update(Sponzor sponzor)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "update Sponzor set naziv=@naziv,brojTelefona=@brojTelefona,email=@email where idSponzor = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@naziv", sponzor.Naziv);
                    command.Parameters.AddWithValue("@brojTelefona", sponzor.BrojTelefona);
                    command.Parameters.AddWithValue("@email", sponzor.Email);
                    command.Parameters.AddWithValue("@id", sponzor.IdSponzor);
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
