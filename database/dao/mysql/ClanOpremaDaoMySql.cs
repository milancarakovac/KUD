using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class ClanOpremaDaoMySql : ClanOpremaDao
    {
        public int Delete(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from ClanOprema where idClanOprema = @id;";
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

        public List<ClanOprema> GetAll()
        {
            List<ClanOprema> lista = new List<ClanOprema>();
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "select * from ClanOprema;";
            using (connection)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new ClanOprema(Int32.Parse(reader.GetString(0)), reader.GetInt32(1), reader.GetInt32(2),DateTime.Parse(reader.GetString(3)), DateTime.Parse(reader.GetString(4)),reader.GetBoolean(5)));
                }
                ConnectionPool.ReleaseConnection(connection);
                return lista;
            }
        }

        public ClanOprema GetById(int id)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from ClanOprema where idClanOprema = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    ClanOprema clanOprema = new ClanOprema(Int32.Parse(reader.GetString(0)), reader.GetInt32(1), reader.GetInt32(2), DateTime.Parse(reader.GetString(3)), DateTime.Parse(reader.GetString(4)),reader.GetBoolean(5));
                    return clanOprema;
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

        public int Insert(ClanOprema clanOprema)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into ClanOprema values (0,@idClan,@idOprema,@datumOd,@datumDo,@razduzeno)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idClan", clanOprema.IdClan);
                    command.Parameters.AddWithValue("@idOprema", clanOprema.IdOprema);
                    command.Parameters.AddWithValue("@datumOd", clanOprema.DatumOd);
                    command.Parameters.AddWithValue("@datumDo", clanOprema.DatumDo);
                    command.Parameters.AddWithValue("@razduzeno", clanOprema.Razduzeno);
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

        public int Update(ClanOprema clanOprema)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = "update ClanOprema set idOsoba=@idClan,idOprema=@idOprema,datumOd=@datumOd,datumDo=@datumDo,razduzeno=@razduzeno where idClanOprema = @id;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idClan", clanOprema.IdClan);
                    command.Parameters.AddWithValue("@idOprema", clanOprema.IdOprema);
                    command.Parameters.AddWithValue("@datumOd", clanOprema.DatumOd);
                    command.Parameters.AddWithValue("@datumDo", clanOprema.DatumDo);
                    command.Parameters.AddWithValue("@id", clanOprema.IdClanOprema);
                    command.Parameters.AddWithValue("@razduzeno", clanOprema.Razduzeno);
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
