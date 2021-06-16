using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class KoreografijaDaoMySql : KoreografijaDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from Koreografija where idKoreografija = @id;";
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

        public List<Koreografija> GetAll()
        {
            List<Koreografija> lista = new List<Koreografija>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from Koreografija;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Koreografija(reader.GetInt32(0),reader.GetString(1),reader.GetDateTime(2),reader.GetDouble(3),reader.GetInt32(4)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Koreografija GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from Koreografija where idKoreografija = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Koreografija koreografija = new Koreografija(reader.GetInt32(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDouble(3), reader.GetInt32(4));
                    return koreografija;
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

        public int Insert(Koreografija koreografija)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into Koreografija values (0,@vlasnik,@vrijediDo,@cijena,@idOsoba)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@vlasnik", koreografija.Vlasnik);
                    command.Parameters.AddWithValue("@vrijediDo", koreografija.VrijediDo);
                    command.Parameters.AddWithValue("@cijena", koreografija.Cijena);
                    command.Parameters.AddWithValue("@idOsoba", koreografija.IdOsoba);
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

        public int Update(Koreografija koreografija)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"update Koreografija set vlasnik=@vlasnik,vrijediDo=@vrijediDo,cijena=@cijena,idOsoba=@idOsoba where idKoreografija = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@vlasnik", koreografija.Vlasnik);
                    command.Parameters.AddWithValue("@vrijediDo", koreografija.VrijediDo);
                    command.Parameters.AddWithValue("@cijena", koreografija.Cijena);
                    command.Parameters.AddWithValue("@idOsoba", koreografija.IdOsoba);
                    command.Parameters.AddWithValue("@id", koreografija.IdKoreografija);
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
