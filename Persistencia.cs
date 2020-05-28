using System.Collections.Generic;
using System.Data.SqlClient;
using LibreriaBD;
using System;
using ProyectoEstacionamientos.clases;

namespace ProyectoEstacionamientos
{
    class CapaPersistencia
    {
        public static SqlException errores;
        private static readonly string connection = "Data Source=proyecto-katy.cuudienhvlr5.us-west-1.rds.amazonaws.com;Initial Catalog=estacionamiento;Persist Security Info=True;User ID=admin;Password=puredepapa";



        public void AgregarCajonEstacionamiento(CajonEstacionamiento lugarEstacionamiento)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return;
            }
            string strComando = "INSERT INTO Cajones(Clave, Ocupado, Descripcion)";
            strComando += " VALUES (@clave,@ocupado,@descripcion)";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@clave", lugarEstacionamiento.GetClave());
            cmd.Parameters.AddWithValue("@ocupado", lugarEstacionamiento.GetOcupado());
            cmd.Parameters.AddWithValue("@descripcion", lugarEstacionamiento.GetDescripcion());
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                errores = e;
                conn.Close();
                return;
            }
            conn.Close();
        }

        public List<CajonEstacionamiento> RegresaCajones(bool disponibles = false)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return null;
            }
            SqlDataReader lector;
            string strComand = disponibles ? "SELECT Clave, Ocupado, Descripcion FROM Cajones WHERE Ocupado=0" : "SELECT Clave, Ocupado, Descripcion FROM Cajones";
            lector = UsoBD.Consulta(strComand, conn);
            if (lector == null)
            {
                errores = UsoBD.ESalida;
                conn.Close();
                return null;
            }
            List<CajonEstacionamiento> lista = new List<CajonEstacionamiento>();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    int clave = int.Parse(lector.GetValue(0).ToString());
                    bool ocupado = bool.Parse(lector.GetValue(1).ToString());
                    string descripcion = lector.GetValue(2).ToString();
                    CajonEstacionamiento lugarEstacionamiento
                        = new CajonEstacionamiento(clave, descripcion, ocupado);
                    lista.Add(lugarEstacionamiento);
                }
            }
            conn.Close();
            return lista;
        }

        public bool ClaveCajonExiste(string clave)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return false;
            }
            string strComando = "SELECT * FROM Cajones WHERE Clave = @clave";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@clave", clave);
            bool resultado = false;
            try
            {
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    resultado = true;
                }
            }
            catch (SqlException e)
            {
                errores = e;
                conn.Close();
                return true;
            }
            conn.Close();
            return resultado;
        }

        public void AgregarEntradaVehiculo(string codigo, string matricula, int cajon)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return;
            }
            string strComando = "INSERT INTO RegistroEntradasSalidas(CodigoEntrada, MatriculaAuto, CajonID)";
            strComando += " VALUES (@codigo,@matricula,@cajon)";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@codigo", codigo);
            cmd.Parameters.AddWithValue("@matricula", matricula);
            cmd.Parameters.AddWithValue("@cajon", cajon);
            // actualizar estado de un cajon
            string strComandoUpdate = "UPDATE Cajones SET Ocupado = 1 WHERE Clave=@clave";
            SqlCommand cmdUpdate = new SqlCommand(strComandoUpdate, conn);
            cmdUpdate.Parameters.AddWithValue("@clave", cajon);
            try
            {
                cmd.ExecuteNonQuery();
                cmdUpdate.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                errores = e;
                conn.Close();
                return;
            }
            conn.Close();
        }

        public bool CodigoEntradaExiste(string codigo)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return false;
            }
            string strComando = "SELECT * FROM RegistroEntradasSalidas WHERE CodigoEntrada = @codigo";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@codigo", codigo);
            bool resultado = false;
            try
            {
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    resultado = true;
                }
            }
            catch (SqlException e)
            {
                errores = e;
                conn.Close();
                return true;
            }
            conn.Close();
            return resultado;
        }


        /*
        public List<Auto> RegresaAutosMarca(int idMarca)
        {
            Console.WriteLine(idMarca);
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return null;
            }
            SqlDataReader lector;
            string strComand = "SELECT auto.id, num_serie, nombre, pasajeros, " +
                "tipo_transmision, golpes_defensa, golpes_puertas, golpes_trasera, " +
                "otros, nombre_marca FROM auto INNER JOIN marca_auto ON " +
                "auto.id_marca = marca_auto.id WHERE id_marca = " + idMarca.ToString();
            Console.WriteLine(strComand);
            lector = UsoBD.Consulta(strComand, conn);
            if (lector == null)
            {
                errores = UsoBD.ESalida;
                conn.Close();
                return null;
            }
            List<Auto> lista = new List<Auto>();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    int id = int.Parse(lector.GetValue(0).ToString());
                    int num_serie = int.Parse(lector.GetValue(1).ToString());
                    string nombre = lector.GetValue(2).ToString();
                    int pasajeros = int.Parse(lector.GetValue(3).ToString());
                    string tipo_transmision = lector.GetValue(4).ToString();
                    bool golpes_Defensa = bool.Parse(lector.GetValue(5).ToString());
                    bool golpes_Puertas = bool.Parse(lector.GetValue(6).ToString());
                    bool golpes_Trasera = bool.Parse(lector.GetValue(7).ToString());
                    string otros = lector.GetValue(8).ToString();
                    string marca = lector.GetValue(9).ToString();
                    Auto auto = new Auto(id, num_serie, nombre,
                        pasajeros, tipo_transmision, golpes_Defensa,
                        golpes_Puertas, golpes_Trasera, otros, marca);
                    lista.Add(auto);
                }
            }
            conn.Close();
            return lista;
        }

        public List<Marca> RegresaMarcas()
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return null;
            }
            SqlDataReader lector;
            string strComand = "SELECT ID, NOMBRE_MARCA FROM marca_auto";
            lector = UsoBD.Consulta(strComand, conn);
            if (lector == null)
            {
                errores = UsoBD.ESalida;
                conn.Close();
                return null;
            }
            List<Marca> lista = new List<Marca>();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    string iDMarca = lector.GetValue(0).ToString();
                    string nombreMarca = lector.GetValue(1).ToString();

                    Marca marca = new Marca(iDMarca, nombreMarca);
                    lista.Add(marca);
                }
            }
            conn.Close();
            return lista;
        }

        public bool AgregaAuto(int numSerie, string nombreA, string Marca, int numPasajeros, char tipoTrans, bool golpesDef, bool golpesPuertas, bool golpesTrasera, string otros)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return false;
            }
            string strComando = "INSERT INTO auto(num_serie,id_marca,nombre,pasajeros,tipo_transmision,golpes_defensa,golpes_puertaS,golpes_trasera,otros)";
            strComando += " VALUES (@numSerie,@Marca,@nombreA,@numPasajeros,@tipoTrans,@golpesDefensa,@golpesPuertas,@golpesTrasera,@otros)";

            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@numSerie", numSerie);
            cmd.Parameters.AddWithValue("@Marca", Marca);
            cmd.Parameters.AddWithValue("@nombreA", nombreA);
            cmd.Parameters.AddWithValue("@numPasajeros", numPasajeros);
            cmd.Parameters.AddWithValue("@tipoTrans", tipoTrans);
            cmd.Parameters.AddWithValue("@golpesDefensa", golpesDef);
            cmd.Parameters.AddWithValue("@golpesPuertas", golpesPuertas);
            cmd.Parameters.AddWithValue("@golpesTrasera", golpesTrasera);
            cmd.Parameters.AddWithValue("@otros", otros);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                errores = e;
                conn.Close();
                return false;
            }
            conn.Close();
            return true;
        }

        public string MarcaFromId(int id)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return null;
            }
            string strComando = "SELECT nombre_marca FROM marca_auto WHERE id = @id";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@id", id);
            string resultado = "";
            try
            {
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    r.Read();
                    resultado = r.GetString(0);
                }
            }
            catch (SqlException e)
            {
                errores = e;
                conn.Close();
                return null;
            }
            conn.Close();
            return resultado;
        }

        public bool NumSerieExiste(string numSerie)
        {

            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return false;
            }
            string strComando = "SELECT * FROM auto WHERE num_serie = @numSerie";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@numSerie", numSerie);
            bool resultado = false;
            try
            {
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    resultado = true;
                }
            }
            catch (SqlException e)
            {
                errores = e;
                conn.Close();
                return true;
            }
            conn.Close();
            return resultado;
        }

        public bool MarcaExiste(string nombreMarca)
        {

            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return false;
            }
            string strComando = "SELECT * FROM marca_auto WHERE nombre_marca = @nomMarca";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@nomMarca", nombreMarca);
            bool resultado = false;
            try
            {
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    resultado = true;
                }
            }
            catch (SqlException e)
            {
                errores = e;
                conn.Close();
                return true;
            }
            conn.Close();
            return resultado;
        }

        public bool AgregaMarca(string nombreMarca)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return false;
            }
            string strComando = "INSERT INTO marca_auto(nombre_marca)";
            strComando += " VALUES (@nomMarca)";

            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@nomMarca", nombreMarca);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                errores = e;
                conn.Close();
                return false;
            }
            conn.Close();
            return true;
        }

    }*/
    }
}