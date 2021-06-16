using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class IzvodjackaGrupaDaoMySql : IzvodjackaGrupaDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from IzvodjackaGrupa where idIzvodjackaGrupa = @id;";
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

        public List<IzvodjackaGrupa> GetAll()
        {
            List<IzvodjackaGrupa> lista = new List<IzvodjackaGrupa>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from IzvodjackaGrupa;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new IzvodjackaGrupa(reader.GetInt32(0),reader.GetString(1)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public IzvodjackaGrupa GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from IzvodjackaGrupa where idIzvodjackaGrupa = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    IzvodjackaGrupa izvodjackaGrupa = new IzvodjackaGrupa(reader.GetInt32(0), reader.GetString(1));
                    return izvodjackaGrupa;
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

        public int Insert(IzvodjackaGrupa izvodjackaGrupa)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into IzvodjackaGrupa values (0,@naziv)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@naziv", izvodjackaGrupa.Naziv);
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

        public int Update(IzvodjackaGrupa izvodjackaGrupa)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"update IzvodjackaGrupa set naziv=@naziv where idIzvodjackaGrupa = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@naziv", izvodjackaGrupa.Naziv);
                    command.Parameters.AddWithValue("@id", izvodjackaGrupa.IdIzvodjackaGrupa);
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
