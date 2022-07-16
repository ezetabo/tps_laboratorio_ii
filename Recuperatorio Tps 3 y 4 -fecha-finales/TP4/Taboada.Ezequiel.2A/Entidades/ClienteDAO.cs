using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class ClienteDAO
    {

        private static string connectionString;
        private static SqlConnection connection;
        private static SqlCommand command;

        static ClienteDAO()
        {
            ClienteDAO.connectionString = @"Server = .; Database = bd-Taboada-Tp4; Trusted_Connection = True;";
            connection = new SqlConnection(ClienteDAO.connectionString);
            command = new SqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = connection;
        }


        public static bool Guardar(Cliente cliente)
        {
            bool ok = true;
            try
            {
                string query = "INSERT INTO dbo.clientes (dni, nombre, apellido, telefono) VALUES (@dni, @nombre, @apellido, @telefono)";
                connection.Open();
                command.CommandText = query;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@dni", cliente.Dni);
                command.Parameters.AddWithValue("@nombre", cliente.Nombre);
                command.Parameters.AddWithValue("@apellido", cliente.Apellido);
                command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                ok = false;
            }
            finally
            {
                if (connection is not null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return ok;
        }

        public static List<Cliente> Leer()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                string query = "SELECT * FROM dbo.clientes";
                connection.Open();
                command.CommandText = query;
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string dni = dataReader.GetString(0);
                    string nombre = dataReader.GetString(1);
                    string apellido = dataReader.GetString(2);
                    string telefono = dataReader.GetString(3);

                    clientes.Add(new Cliente(dni, nombre, apellido, telefono));
                }
                return clientes;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (connection is not null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public static bool  Modificar(Cliente cliente)
        {
            bool ok = true;
            try
            {
                string query = "UPDATE dbo.clientes SET nombre = @nombre, apellido = @apellido, telefono = @telefono WHERE dni = @dni";
                connection.Open();
                command.CommandText = query;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@dni", cliente.Dni);
                command.Parameters.AddWithValue("@nombre", cliente.Nombre);
                command.Parameters.AddWithValue("@apellido", cliente.Apellido);
                command.Parameters.AddWithValue("@telefono", cliente.Telefono);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                ok =  false;
            }
            finally
            {
                if (connection is not null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return ok;
        }

        public static bool Eliminar(string dni)
        {
            bool ok = true;
            try
            {
                string query = "DELETE FROM dbo.clientes WHERE dni = @dniBuscado";
                connection.Open();
                command.CommandText = query;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@dniBuscado", dni);
                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                ok = false;
            }
            finally
            {
                if (connection is not null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return ok;
        }
    }

}
