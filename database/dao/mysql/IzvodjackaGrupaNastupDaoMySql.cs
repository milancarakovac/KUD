using KUD.database.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace KUD.database.dao.mysql
{
    class IzvodjackaGrupaNastupDaoMySql : IzvodjackaGrupaNastupDao
    {
        public int Delete(int idIzvodjackaGrupa,int idNastup)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"delete from IzvodjackaGrupaNastup where idIzvodjackaGrupa = @id and idNastup=@id2;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", idIzvodjackaGrupa);
                    command.Parameters.AddWithValue("@id2", idNastup);
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

        public List<IzvodjackaGrupaNastup> GetAll()
        {
            throw new NotImplementedException();
        }

        public IzvodjackaGrupaNastup GetById(int idIzvodjackaGrupa, int idNastup)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"select * from IzvodjackaGrupaNastup where idIzvodjackaGrupa = @id and idNastup=@id2;";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", idIzvodjackaGrupa);
                    command.Parameters.AddWithValue("@id2", idNastup);
                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    IzvodjackaGrupaNastup izvodjackaGrupaNastup = new IzvodjackaGrupaNastup(reader.GetInt32(0), reader.GetInt32(1));
                    return izvodjackaGrupaNastup;
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

        public int Insert(IzvodjackaGrupa izvodjackaGrupa, Nastup nastup)
        {
            var connection = ConnectionPool.GetConnection();
            connection.Open();
            string query = @"insert into IzvodjackaGrupaNastup values (@idGrupa,@idNastup)";
            using (connection)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idGrupa", izvodjackaGrupa.IdIzvodjackaGrupa);
                    command.Parameters.AddWithValue("@idNastup", nastup.IdNastup);
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

        public int Update(IzvodjackaGrupaNastup izvodjackaGrupaNastup)
        {
            throw new NotImplementedException();
        }
    }
}
