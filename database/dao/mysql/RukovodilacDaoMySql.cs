using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class RukovodilacDaoMySql : RukovodilacDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete Rukovodilac, Osoba from Rukovodilac inner join Osoba on Osoba.idOsoba=Rukovodilac.idOsoba where Rukovodilac.idOsoba = @id;";
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

        public List<Rukovodilac> GetAll()
        {
            List<Rukovodilac> lista = new List<Rukovodilac>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from selectRukovodilac;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Rukovodilac(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), DateTime.Parse(reader.GetString(6)), reader.GetString(7)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Rukovodilac GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from selectRukovodilac where idOsoba = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Rukovodilac rukovodilac = new Rukovodilac(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5), DateTime.Parse(reader.GetString(6)), reader.GetString(7));
                    return rukovodilac;
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

        public int Insert(Rukovodilac rukovodilac)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"call insertRukovodilac(@ime,@prezime,@jmbg,@brojTelefona,@email,@datumRodjenja,@pozicija);";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ime", rukovodilac.Ime);
                    command.Parameters.AddWithValue("@prezime", rukovodilac.Prezime);
                    command.Parameters.AddWithValue("@jmbg", rukovodilac.Jmbg);
                    command.Parameters.AddWithValue("@brojTelefona", rukovodilac.BrojTelefona);
                    command.Parameters.AddWithValue("@email", rukovodilac.Email);
                    command.Parameters.AddWithValue("@datumRodjenja", rukovodilac.DatumRodjenja);
                    command.Parameters.AddWithValue("@pozicija", rukovodilac.Pozicija);
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

        public int Update(Rukovodilac rukovodilac)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "update Osoba inner join Rukovodilac on Rukovodilac.idOsoba=Osoba.idOsoba set ime=@ime,prezime=@prezime,jmbg=@jmbg,brojTelefona=@brojTelefona,email=@email,datumRodjenja=@datumRodjenja,pozicija=@pozicija where Osoba.idOsoba=@id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ime", rukovodilac.Ime);
                    command.Parameters.AddWithValue("@prezime", rukovodilac.Prezime);
                    command.Parameters.AddWithValue("@jmbg", rukovodilac.Jmbg);
                    command.Parameters.AddWithValue("@brojTelefona", rukovodilac.BrojTelefona);
                    command.Parameters.AddWithValue("@email", rukovodilac.Email);
                    command.Parameters.AddWithValue("@datumRodjenja", rukovodilac.DatumRodjenja);
                    command.Parameters.AddWithValue("@pozicija", rukovodilac.Pozicija);
                    command.Parameters.AddWithValue("@id", rukovodilac.IdOsoba);
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
