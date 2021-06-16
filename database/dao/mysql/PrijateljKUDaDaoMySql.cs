using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class PrijateljKUDaDaoMySql : PrijateljKUDaDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete PrijateljKUDa, Osoba from PrijateljKUDa inner join Osoba on Osoba.idOsoba=PrijateljKUDa.idOsoba where PrijateljKUDa.idOsoba = @id;";
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

        public List<PrijateljKUDa> GetAll()
        {
            List<PrijateljKUDa> lista = new List<PrijateljKUDa>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from selectPrijateljKUDa;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new PrijateljKUDa(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), DateTime.Parse(reader.GetString(6))));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public PrijateljKUDa GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from selectPrijateljKUDa where idOsoba = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    PrijateljKUDa prijateljKUDa = new PrijateljKUDa(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5), DateTime.Parse(reader.GetString(6)));
                    return prijateljKUDa;
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

        public int Insert(PrijateljKUDa prijateljKUDa)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"call insertPrijateljKUDa(@ime,@prezime,@jmbg,@brojTelefona,@email,@datumRodjenja);";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ime", prijateljKUDa.Ime);
                    command.Parameters.AddWithValue("@prezime", prijateljKUDa.Prezime);
                    command.Parameters.AddWithValue("@jmbg", prijateljKUDa.Jmbg);
                    command.Parameters.AddWithValue("@brojTelefona", prijateljKUDa.BrojTelefona);
                    command.Parameters.AddWithValue("@email", prijateljKUDa.Email);
                    command.Parameters.AddWithValue("@datumRodjenja", prijateljKUDa.DatumRodjenja);
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

        public int Update(PrijateljKUDa prijateljKUDa)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "update Osoba set ime=@ime,prezime=@prezime,jmbg=@jmbg,brojTelefona=@brojTelefona,email=@email,datumRodjenja=@datumRodjenja where idOsoba=@id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ime", prijateljKUDa.Ime);
                    command.Parameters.AddWithValue("@prezime", prijateljKUDa.Prezime);
                    command.Parameters.AddWithValue("@jmbg", prijateljKUDa.Jmbg);
                    command.Parameters.AddWithValue("@brojTelefona", prijateljKUDa.BrojTelefona);
                    command.Parameters.AddWithValue("@email", prijateljKUDa.Email);
                    command.Parameters.AddWithValue("@datumRodjenja", prijateljKUDa.DatumRodjenja);
                    command.Parameters.AddWithValue("@id", prijateljKUDa.IdOsoba);
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
