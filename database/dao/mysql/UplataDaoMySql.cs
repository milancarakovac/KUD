using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class UplataDaoMySql : UplataDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from Uplata where idUplata = @id;";
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

        public List<Uplata> GetAll()
        {
            List<Uplata> lista = new List<Uplata>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from Uplata;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Uplata(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDouble(2), reader.GetInt32(3), reader.GetInt32(4)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Uplata GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from Uplata where idUplata = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Uplata uplata = new Uplata(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDouble(2), reader.GetInt32(3), reader.GetInt32(4));
                    return uplata;
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

        public int Insert(Uplata uplata)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into Uplata values (0,@datum,@iznos,@idOsoba,@idClan);";
            string query2 = @"insert into UplataClanarina values (@idUplata,@idClan);";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@iznos", uplata.IznosUplate);
                    command.Parameters.AddWithValue("@datum", uplata.DatumUplate);
                    command.Parameters.AddWithValue("@idOsoba", uplata.IdOsoba);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (uplata.IdClan > 0)
                    {
                        command = new MySqlCommand(query2, connection);
                        command.Parameters.AddWithValue("@idUplata", uplata.IdUplata);
                        command.Parameters.AddWithValue("@idClan", uplata.IdClan);
                        reader = command.ExecuteReader();
                    }
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

        public int Update(Uplata uplata)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "update Uplata set iznos=@iznos,datumUplate=@datum,idOsoba=@idOsoba,idClan=@idClan where idUplata = @id;";
            string query2 = "update UplataClanarina set idClan=@idClan where idUplata = @id";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@iznos", uplata.IznosUplate);
                    command.Parameters.AddWithValue("@datum", uplata.DatumUplate);
                    command.Parameters.AddWithValue("@idOsoba", uplata.IdOsoba);
                    command.Parameters.AddWithValue("@idClan", uplata.IdClan);
                    command.Parameters.AddWithValue("@id", uplata.IdUplata);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (uplata.IdClan > 0)
                    {
                        command = new MySqlCommand(query2, connection);
                        command.Parameters.AddWithValue("@idClan", uplata.IdClan);
                        command.Parameters.AddWithValue("@id", uplata.IdUplata);
                        reader = command.ExecuteReader();
                    }
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
