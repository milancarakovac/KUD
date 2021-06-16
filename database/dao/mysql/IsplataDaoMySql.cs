using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class IsplataDaoMySql : IsplataDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from Isplata where idIsplata = @id;";
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

        public List<Isplata> GetAll()
        {
            List<Isplata> lista = new List<Isplata>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from Isplata;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Isplata(Int32.Parse(reader.GetString(0)), reader.GetDouble(1), reader.GetString(2), DateTime.Parse(reader.GetString(3)),reader.GetString(4),reader.GetInt32(5)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Isplata GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from Isplata where idIsplata = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Isplata isplata = new Isplata(Int32.Parse(reader.GetString(0)), reader.GetDouble(1), reader.GetString(2), DateTime.Parse(reader.GetString(3)), reader.GetString(4), reader.GetInt32(5));
                    return isplata;
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

        public int Insert(Isplata isplata)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into Isplata values (0,@iznos,@primalac,@datum,@idOsoba,@brojRacuna)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@iznos", isplata.Iznos);
                    command.Parameters.AddWithValue("@primalac", isplata.Primalac);
                    command.Parameters.AddWithValue("@datum", isplata.DatumIsplate);
                    command.Parameters.AddWithValue("@idOsoba", isplata.IdOsoba);
                    command.Parameters.AddWithValue("@brojRacuna", isplata.BrojRacuna);
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

        public int Update(Isplata isplata)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"update Isplata set iznos=@iznos,primalac=@primalac,datum=@datum,idOsoba=@idOsoba,brojRacuna=@brojRacuna where idIsplata = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@iznos", isplata.Iznos);
                    command.Parameters.AddWithValue("@primalac", isplata.Primalac);
                    command.Parameters.AddWithValue("@datum", isplata.DatumIsplate);
                    command.Parameters.AddWithValue("@idOsoba", isplata.IdOsoba);
                    command.Parameters.AddWithValue("@brojRacuna", isplata.BrojRacuna);
                    command.Parameters.AddWithValue("@id", isplata.IdIsplata);
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
