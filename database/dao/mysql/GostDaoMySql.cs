using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class GostDaoMySql : GostDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from Gost where idGost = @id;";
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

        public List<Gost> GetAll()
        {
            List<Gost> lista = new List<Gost>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from Gost;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Gost(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Gost GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from Gost where idGost = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Gost gost = new Gost(reader.GetInt32(0), reader.GetString(1),reader.GetString(2),reader.GetInt32(3));
                    return gost;
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

        public int Insert(Gost gost)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into Gost values (0,@ime,@mjesto,@idKoncert)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ime", gost.Ime);
                    command.Parameters.AddWithValue("@mjesto", gost.Mjesto);
                    command.Parameters.AddWithValue("@idKoncert", gost.IdKoncert);
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

        public int Update(Gost gost)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"update Gost set ime=@ime,mjesto=@mjesto,idKoncert=@idKoncert where id=Gost = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ime", gost.Ime);
                    command.Parameters.AddWithValue("@mjesto", gost.Mjesto);
                    command.Parameters.AddWithValue("@idKoncert", gost.IdKoncert);
                    command.Parameters.AddWithValue("@id", gost.IdGost);
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
