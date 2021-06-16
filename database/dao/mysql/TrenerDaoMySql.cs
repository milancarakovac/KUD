using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class TrenerDaoMySql : TrenerDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete Trener, Osoba from Trener inner join Osoba on Osoba.idOsoba=Trener.idOsoba where Trener.idOsoba = @id;";
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

        public List<Trener> GetAll()
        {
            List<Trener> lista = new List<Trener>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from selectTrener;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Trener(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), DateTime.Parse(reader.GetString(6)),reader.GetString(7)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Trener GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from selectTrener where idOsoba = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Trener trener = new Trener(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5), DateTime.Parse(reader.GetString(6)),reader.GetString(7));
                    return trener;
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

        public int Insert(Trener trener)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"call insertTrener(@ime,@prezime,@jmbg,@brojTelefona,@email,@datumRodjenja,@brojLicence);";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ime", trener.Ime);
                    command.Parameters.AddWithValue("@prezime", trener.Prezime);
                    command.Parameters.AddWithValue("@jmbg", trener.Jmbg);
                    command.Parameters.AddWithValue("@brojTelefona", trener.BrojTelefona);
                    command.Parameters.AddWithValue("@email", trener.Email);
                    command.Parameters.AddWithValue("@datumRodjenja", trener.DatumRodjenja);
                    command.Parameters.AddWithValue("@pozicija", trener.BrojLicence);
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

        public int Update(Trener trener)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"update Osoba inner join Trener on Trener.idOsoba=Osoba.idOsoba set ime=@ime,prezime=@prezime,jmbg=@jmbg,brojTelefona=@brojTelefona,email=@email,datumRodjenja=@datumRodjenja,brojLicence=@brojLicence where Osoba.idOsoba=@id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ime", trener.Ime);
                    command.Parameters.AddWithValue("@prezime", trener.Prezime);
                    command.Parameters.AddWithValue("@jmbg", trener.Jmbg);
                    command.Parameters.AddWithValue("@brojTelefona", trener.BrojTelefona);
                    command.Parameters.AddWithValue("@email", trener.Email);
                    command.Parameters.AddWithValue("@datumRodjenja", trener.DatumRodjenja);
                    command.Parameters.AddWithValue("@pozicija", trener.BrojLicence);
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
