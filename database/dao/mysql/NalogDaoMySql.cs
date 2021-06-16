using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class NalogDaoMySql : NalogDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from Nalog where idNalog = @id;";
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

        public List<Nalog> GetAll()
        {
            List<Nalog> lista = new List<Nalog>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from Nalog;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var nalog = new Nalog(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3), reader.GetInt32(4))
                    {
                        Jezik = reader.GetString(5),
                        Tema = reader.GetString(6)
                    };
                    lista.Add(nalog);
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public Nalog GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from Nalog where idNalog = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    var nalog = new Nalog(Int32.Parse(reader.GetString(0)), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3), reader.GetInt32(4))
                    {
                        Jezik = reader.GetString(5),
                        Tema = reader.GetString(6)
                    };
                    return nalog;
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

        public int Insert(Nalog nalog)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into Nalog values (0,@korisnickoIme,@lozinka,@administrator,@idOsoba);";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@korisnickoIme", nalog.KorisnickoIme);
                    command.Parameters.AddWithValue("@lozinka", nalog.Lozinka);
                    command.Parameters.AddWithValue("@administrator", nalog.Administrator);
                    command.Parameters.AddWithValue("@idOsoba", nalog.IdOsoba);
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
                    else if (ex.Message.Contains("Duplicate entry")) return 7;
                    else return 5;
                }
                finally
                {
                    ConnectionPool.ReleaseConnection(connection);
                }
            }
        }

        public int Update(Nalog nalog)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            if (nalog.Lozinka.Length < 1) {
                string query = @"update Nalog set korisnickoIme=@korisnickoIme,administrator=@administrator,idOsoba=@idOsoba,jezik=@jezik,tema=@tema where idNalog=@id;";
                using (connection)
                {
                    try
                    {
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@korisnickoIme", nalog.KorisnickoIme);
                        command.Parameters.AddWithValue("@administrator", nalog.Administrator);
                        command.Parameters.AddWithValue("@idOsoba", nalog.IdOsoba);
                        command.Parameters.AddWithValue("@id", nalog.IdNalog);
                        command.Parameters.AddWithValue("@jezik", nalog.Jezik);
                        command.Parameters.AddWithValue("@tema", nalog.Tema);
                        MySqlDataReader reader = command.ExecuteReader();
                        return 0;
                    }
                    catch (MySqlException ex)
                    {
                        if (ex.Message.Contains("Neispravan format email-a")) return 1;
                        else if (ex.Message.Contains("Neispravan format unesenog imena ili prezimena")) return 2;
                        else if (ex.Message.Contains("Neispravan format maticnog broja")) return 3;
                        else if (ex.Message.Contains("Vrijednost mora biti veca od 0")) return 4;
                        else if (ex.Message.Contains("Duplicate entry")) return 7;
                        else return 5;
                    }
                    finally
                    {
                        ConnectionPool.ReleaseConnection(connection);
                    }
                }
            }
            else
            {
                string query = @"update Nalog set korisnickoIme=@korisnickoIme,lozinka=@lozinka,administrator=@administrator,idOsoba=@idOsoba,jezik=@jezik,tema=@tema where idNalog=@id;";
                using (connection)
                {
                    try
                    {
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@korisnickoIme", nalog.KorisnickoIme);
                        command.Parameters.AddWithValue("@lozinka", nalog.Lozinka);
                        command.Parameters.AddWithValue("@administrator", nalog.Administrator);
                        command.Parameters.AddWithValue("@idOsoba", nalog.IdOsoba);
                        command.Parameters.AddWithValue("@id", nalog.IdNalog);
                        command.Parameters.AddWithValue("@jezik", nalog.Jezik);
                        command.Parameters.AddWithValue("@tema", nalog.Tema);
                        MySqlDataReader reader = command.ExecuteReader();
                        return 0;
                    }
                    catch (MySqlException ex)
                    {
                        if (ex.Message.Contains("Neispravan format email-a")) return 1;
                        else if (ex.Message.Contains("Neispravan format unesenog imena ili prezimena")) return 2;
                        else if (ex.Message.Contains("Neispravan format maticnog broja")) return 3;
                        else if (ex.Message.Contains("Vrijednost mora biti veca od 0")) return 4;
                        else if (ex.Message.Contains("Duplicate entry")) return 7;
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
}
