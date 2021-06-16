using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace KUD.database.dao.mysql
{
    class BlagajnikDaoMySql : BlagajnikDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete Blagajnik, Osoba from Blagajnik inner join Osoba on Osoba.idOsoba=Blagajnik.idOsoba where Blagajnik.idOsoba = @id;";
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

        public List<Blagajnik> GetAll()
        {
            List<Blagajnik> lista = new List<Blagajnik>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from selectBlagajnik;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Blagajnik(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), DateTime.Parse(reader.GetString(6))));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Blagajnik GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from selectBlagajnik where idOsoba = @id";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Blagajnik blagajnik = new Blagajnik(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2), 
                        reader.GetString(3), reader.GetString(4), reader.GetString(5), DateTime.Parse(reader.GetString(6)));
                    return blagajnik;
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

        public int Insert(Blagajnik blagajnik)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"call insertBlagajnik(@ime,@prezime,@jmbg,@brojTelefona,@email,@datumRodjenja);";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ime", blagajnik.Ime);
                    command.Parameters.AddWithValue("@prezime", blagajnik.Prezime);
                    command.Parameters.AddWithValue("@jmbg", blagajnik.Jmbg);
                    command.Parameters.AddWithValue("@brojTelefona", blagajnik.BrojTelefona);
                    command.Parameters.AddWithValue("@email", blagajnik.Email);
                    command.Parameters.AddWithValue("@datumRodjenja", blagajnik.DatumRodjenja);
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

        public int Update(Blagajnik blagajnik)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"update Osoba set ime=@ime,prezime=@prezime,jmbg=@jmbg,brojTelefona=@brojTelefona,email=@email,datumRodjenja=@datumRodjenja where idOsoba=@id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ime",blagajnik.Ime);
                    command.Parameters.AddWithValue("@prezime",blagajnik.Prezime);
                    command.Parameters.AddWithValue("@jmbg",blagajnik.Jmbg);
                    command.Parameters.AddWithValue("@brojTelefona",blagajnik.BrojTelefona);
                    command.Parameters.AddWithValue("@email",blagajnik.Email);
                    command.Parameters.AddWithValue("@datumRodjenja",blagajnik.DatumRodjenja);
                    command.Parameters.AddWithValue("@id",blagajnik.IdOsoba);
                    MySqlDataReader reader = command.ExecuteReader();
                    return 0;
                }
                catch(MySqlException ex)
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
