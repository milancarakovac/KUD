using MySql.Data.MySqlClient;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Windows.UI.Xaml;

namespace KUD.database
{
    class ConnectionPool
    {
        private static readonly string CONNECTION_STRING = getConnectionString();
        private static MySqlConnection[] connections;
        private static bool[] takenConnections;
        private static int availableConnections;
        private static int currentConnections;
        private static readonly int INITIAL_CONNECTIONS = 30;

        public static void Initialize()
        {
            connections = new MySqlConnection[INITIAL_CONNECTIONS];
            takenConnections = new bool[INITIAL_CONNECTIONS];
            availableConnections = INITIAL_CONNECTIONS;
            currentConnections = INITIAL_CONNECTIONS;
            for (int i = 0; i < INITIAL_CONNECTIONS; i++)
            {
                try
                {
                    connections[i] = new MySqlConnection(CONNECTION_STRING);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static MySqlConnection GetConnection()
        {
            MySqlConnection connection = null;
            if (availableConnections > 0)
            {
                for(int i = 0; i < currentConnections; i++)
                {
                    if (!takenConnections[i])
                    {
                        takenConnections[i] = true;
                        availableConnections--;
                        connection = connections[i];
                        break;
                    }
                }
            }
            else
            {
                MySqlConnection[] tempConnections = new MySqlConnection[currentConnections * 2];
                bool[] tempTakenConnections = new bool[currentConnections * 2];
                for(int i = 0; i < currentConnections * 2; i++)
                {
                    tempConnections[i] = (i < currentConnections) ? connections[i] : (new MySqlConnection(CONNECTION_STRING));
                    tempTakenConnections[i] = i < currentConnections && takenConnections[i];
                }
                availableConnections = currentConnections;
                connections = tempConnections;
                takenConnections = tempTakenConnections;
                takenConnections[currentConnections] = true;
                connection = connections[currentConnections];
                currentConnections *= 2;
            }
            return connection;
        }
        public static void ReleaseConnection(MySqlConnection connection)
        {
            int index = Enumerable.ToList<MySqlConnection>(connections).IndexOf(connection);
            takenConnections[index] = false;
            availableConnections++;
        }
        private static string getConnectionString()
        {
            string rootDirectory = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string[] file = System.IO.File.ReadAllLines(rootDirectory + "/Assets/settings.txt");
            string ConnectionString = "Server=";
            if (file.Length > 3)
            {
                ConnectionString += file[0].Split("=")[1];
                ConnectionString += "; Database=";
                ConnectionString += file[1].Split("=")[1];
                ConnectionString += "; Uid=";
                ConnectionString += file[2].Split("=")[1];
                ConnectionString += "; Pwd=";
                ConnectionString += file[3].Split("=")[1];
            }
            ConnectionString = "Server=localhost; Database=kud; Uid=root; Pwd=root";
            return ConnectionString;
        }
    }
}
