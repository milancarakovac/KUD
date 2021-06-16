using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class IzvodjackaGrupaKoreografijaDaoMySql : IzvodjackaGrupaKoreografijaDao
    {
        public int Delete(int idKoreografija, int idIzvodjackaGrupa)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from IzvodjackaGrupaKoreografija where idIzvodjackaGrupa = @id and idKoreografija=@id2;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", idIzvodjackaGrupa);
                    command.Parameters.AddWithValue("@id2", idKoreografija);
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

        public IzvodjackaGrupaKoreografija GetById(int idKoreografija, int idIzvodjackaGrupa)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from IzvodjackaGrupaKoreografija where idIzvodjackaGrupa = @id and idKoreografija=@id2;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", idIzvodjackaGrupa);
                    command.Parameters.AddWithValue("@id2", idKoreografija);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    IzvodjackaGrupaKoreografija izvodjackaGrupaKoreografija = new IzvodjackaGrupaKoreografija(reader.GetInt32(0), reader.GetInt32(1));
                    return izvodjackaGrupaKoreografija;
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

        public int Insert(IzvodjackaGrupa grupa, Koreografija koreografija)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into IzvodjackaGrupaKoreografija values (@idGrupa,@idKoreografija)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idGrupa", grupa.IdIzvodjackaGrupa);
                    command.Parameters.AddWithValue("@idKoreografija", koreografija.IdKoreografija);
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
