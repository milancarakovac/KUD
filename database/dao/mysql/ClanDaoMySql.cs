using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class ClanDaoMySql : ClanDao
    {
        public int Delete(int id)
        {
            Clan clan = GetById(id);
            clan.Obrisan = true;
            return Update(clan);
        }

        public List<Clan> GetAll()
        {
            List<Clan> lista = new List<Clan>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from selectClan;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Clan(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2), 
                        reader.GetString(3), reader.GetString(4), reader.GetString(5), DateTime.Parse(reader.GetString(6)),reader.GetString(7),reader.GetBoolean(8)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Clan GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from selectClan where idOsoba = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Clan clan = new Clan(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5), DateTime.Parse(reader.GetString(6)), reader.GetString(7), reader.GetBoolean(8));
                    return clan;
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

        public int Insert(Clan clan)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"call insertClan(@ime,@prezime,@jmbg,@brojTelefona,@email,@datumRodjenja,@uzrasnaGrupa);";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ime", clan.Ime);
                    command.Parameters.AddWithValue("@prezime", clan.Prezime);
                    command.Parameters.AddWithValue("@jmbg", clan.Jmbg);
                    command.Parameters.AddWithValue("@brojTelefona", clan.BrojTelefona);
                    command.Parameters.AddWithValue("@email", clan.Email);
                    command.Parameters.AddWithValue("@datumRodjenja", clan.DatumRodjenja);
                    command.Parameters.AddWithValue("@uzrasnaGrupa", clan.UzrasnaGrupa);
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

        public int Update(Clan clan)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"update Osoba inner join Clan on Osoba.idOsoba=Clan.idOsoba set " +
                "ime=@ime,prezime=@prezime,jmbg=@jmbg,brojTelefona=@brojTelefona,email=@email," +
                "datumRodjenja=@datumRodjenja,uzrasnaGrupa=@uzrasnaGrupa,obrisan=@obrisan where Osoba.idOsoba=@id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ime", clan.Ime);
                    command.Parameters.AddWithValue("@prezime", clan.Prezime);
                    command.Parameters.AddWithValue("@jmbg", clan.Jmbg);
                    command.Parameters.AddWithValue("@brojTelefona", clan.BrojTelefona);
                    command.Parameters.AddWithValue("@email", clan.Email);
                    command.Parameters.AddWithValue("@datumRodjenja", clan.DatumRodjenja);
                    command.Parameters.AddWithValue("@uzrasnaGrupa", clan.UzrasnaGrupa);
                    command.Parameters.AddWithValue("@id", clan.IdOsoba);
                    command.Parameters.AddWithValue("@obrisan", clan.Obrisan);
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
