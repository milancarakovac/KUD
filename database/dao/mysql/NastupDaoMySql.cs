using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class NastupDaoMySql : NastupDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from Nastup where idNastup = @id;";
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

        public List<Nastup> GetAll()
        {
            List<Nastup> lista = new List<Nastup>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from Nastup;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Nastup(Int32.Parse(reader.GetString(0)), DateTime.Parse(reader.GetString(1)), reader.GetString(2), reader.GetString(3)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Nastup GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from Nastup where idNastup = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(@"id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Nastup nastup = new Nastup(Int32.Parse(reader.GetString(0)),DateTime.Parse( reader.GetString(1)), reader.GetString(2), reader.GetString(3));
                    return nastup;
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

        public int Insert(Nastup nastup)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into Nastup values (0,@datum,@mjesto,@naziv)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@datum", nastup.Datum);
                    command.Parameters.AddWithValue("@mjesto", nastup.Mjesto);
                    command.Parameters.AddWithValue("@naziv", nastup.Naziv);
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

        public int Update(Nastup nastup)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"update Nastup set datum=@datum,mjesto=@mjesto,naziv=@naziv where idNastup = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@datum", nastup.Datum);
                    command.Parameters.AddWithValue("@mjesto", nastup.Mjesto);
                    command.Parameters.AddWithValue("@naziv", nastup.Naziv);
                    command.Parameters.AddWithValue("@id", nastup.IdNastup);
                    MySqlDataReader reader = command.ExecuteReader();
                    return 0;
                }
                catch (MySqlException ex)
                {
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
