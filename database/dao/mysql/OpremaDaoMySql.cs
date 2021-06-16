using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class OpremaDaoMySql : OpremaDao
    {
        public int Delete(int id)
        {
            Oprema oprema = GetById(id);
            oprema.Obrisan = true;
            return Update(oprema);
        }

        public List<Oprema> GetAll()
        {
            List<Oprema> lista = new List<Oprema>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from Oprema;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Oprema(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetBoolean(4), reader.GetBoolean(5)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Oprema GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from Oprema where idOprema = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Oprema oprema = new Oprema(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetBoolean(4), reader.GetBoolean(5));
                    return oprema;
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

        public int Insert(Oprema oprema)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into Oprema values (0,@naziv,@serijskiBroj,@opis,@obrisan,@zaduzen)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@naziv", oprema.Naziv);
                    command.Parameters.AddWithValue("@serijskiBroj", oprema.SerijskiBroj);
                    command.Parameters.AddWithValue("@opis", oprema.Opis);
                    command.Parameters.AddWithValue("@obrisan", oprema.Obrisan);
                    command.Parameters.AddWithValue("@zaduzen", oprema.Zaduzen);
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

        public int Update(Oprema oprema)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "update Oprema set naziv=@naziv,serijskiBroj=@serijskiBroj,opis=@opis,obrisan=@obrisan,zaduzen=@zaduzen where idOprema = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@naziv", oprema.Naziv);
                    command.Parameters.AddWithValue("@serijskiBroj", oprema.SerijskiBroj);
                    command.Parameters.AddWithValue("@opis", oprema.Opis); 
                    command.Parameters.AddWithValue("@id", oprema.IdOprema);
                    command.Parameters.AddWithValue("@obrisan", oprema.Obrisan);
                    command.Parameters.AddWithValue("@zaduzen", oprema.Zaduzen);
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
