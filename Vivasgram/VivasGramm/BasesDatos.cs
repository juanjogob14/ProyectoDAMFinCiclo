﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivasGramm
{
    class BasesDatos
    {
        static string cadenaConexion = "Database = vivasgram; Data Source=localhost; Port = 3306; User id=root; ";

        MySqlConnection conectbd = new MySqlConnection(cadenaConexion);

        MySqlDataReader reader = null;

        public List<string> nombresBase = new List<string>();

        public void ConectarBase()
        {
            conectbd.Open();
        }

        public void CerrarConexion()
        {
            conectbd.Close();
        }

        public bool ComprobarUsuarioRegistrado(string nombre)
        {
            bool registrado = false;
            try
            {
                nombresBase.Clear();
                string consultaSiEsta = "Select nombreusuario from usuarios";
                MySqlCommand comando1 = new MySqlCommand(consultaSiEsta);
                comando1.Connection = conectbd;
                ConectarBase();
                reader = comando1.ExecuteReader();
                while (reader.Read())
                {
                    nombresBase.Add(reader.GetString(0));
                }

                for (int i = 0; i < nombresBase.Count; i++)
                {
                    Console.WriteLine(nombresBase[i]);
                    if (nombresBase[i] == nombre)
                    {
                        registrado = true;
                    }
                    else
                    {
                        registrado = false;
                    }

                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.ErrorCode);

            }
            finally
            {
                CerrarConexion();
            }
            return registrado;
        }
    }
}
