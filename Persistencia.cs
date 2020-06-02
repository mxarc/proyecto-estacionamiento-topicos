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

        public void AgregarPension(Pension pension)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return;
            }
            string strInsertPropietario = "INSERT INTO Propietarios (Nombre, Apellido) VALUES (@nombre, @apellido)";
            SqlCommand cmdPropietario = new SqlCommand(strInsertPropietario, conn);
            cmdPropietario.Parameters.AddWithValue("@nombre", pension.GetNombrePropietario());
            cmdPropietario.Parameters.AddWithValue("@apellido", pension.GetApellidoPropietario());
            try
            {
                cmdPropietario.ExecuteNonQuery();
            } catch (SqlException e)
            {
                errores = e;
                conn.Close();
                return;
            }
            // ahora necesitamos el ID del insert que hicimos previamente
            string strComandoPropietarioID = "SELECT ID FROM Propietarios WHERE Nombre=@nombre AND Apellido=@apellido";
            SqlCommand cmdPropietarioId = new SqlCommand(strComandoPropietarioID, conn);
            cmdPropietarioId.Parameters.AddWithValue("@nombre", pension.GetNombrePropietario());
            cmdPropietarioId.Parameters.AddWithValue("@apellido", pension.GetApellidoPropietario());
            // sacar el propietario ID para luego poder registrar la pensión con ese ID asociado
            int propietarioID = 1;
            try
            {
                var lector = cmdPropietarioId.ExecuteReader();
                while (lector.Read())
                {
                    propietarioID = lector.GetInt32(0);
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                errores = e;
                conn.Close();
                return;
            }
            cmdPropietarioId.Dispose();
            conn.Close();
            conn.Open();
            // ahora si guardar pensión en la BD
            string strComando = "INSERT INTO Pensiones (MatriculaAuto, ModeloAuto, FechaAlta, FechaVencimiento," +
                "CuotaPago, PropietarioID, CajonID)";
            strComando += " VALUES (@matriculaAuto,@modeloAuto,@fechaAlta,@fechaVencimiento,@cuota,@propietarioID,@cajonID)";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@matriculaAuto", pension.GetMatriculaAuto());
            cmd.Parameters.AddWithValue("@modeloAuto", pension.GetModelo());
            cmd.Parameters.AddWithValue("@fechaAlta", pension.GetFechaIngreso());
            cmd.Parameters.AddWithValue("@fechaVencimiento", pension.GetFechaVencimiento());
            cmd.Parameters.AddWithValue("@cuota", pension.GetCuota());
            cmd.Parameters.AddWithValue("@propietarioID", propietarioID);
            cmd.Parameters.AddWithValue("@cajonID", pension.GetCajonID());
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

        public void AgregarCajonEstacionamiento(clases.VehiculoEstacionado lugarEstacionamiento)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return;
            }
            string strComando = "INSERT INTO Cajones (Clave, Ocupado, Descripcion)";
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

        public void ModificarDescripcionCajon(int cajonID, string descripcion)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return;
            }
            string strComando = "UPDATE Cajones SET descripcion=@descripcion WHERE Clave=@cajonID";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@cajonID", cajonID);
            cmd.Parameters.AddWithValue("@descripcion", descripcion);
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


        public string RegresaDescripcionCajon(int idCajon)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return "";
            }
            string strComando = "SELECT Descripcion FROM Cajones WHERE Clave = @idCajon";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@idCajon", idCajon);
            string descripcion = "";
            try
            {
                var lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    descripcion = lector.GetString(0);
                }
            }
            catch (SqlException e)
            {
                errores = e;
                conn.Close();
                return descripcion;
            }
            conn.Close();
            return descripcion;
        }

        public List<clases.VehiculoEstacionado> RegresaCajones(bool disponibles = false)
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
            List<clases.VehiculoEstacionado> lista = new List<clases.VehiculoEstacionado>();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    int clave = int.Parse(lector.GetValue(0).ToString());
                    bool ocupado = bool.Parse(lector.GetValue(1).ToString());
                    string descripcion = lector.GetValue(2).ToString();
                    clases.VehiculoEstacionado lugarEstacionamiento
                        = new clases.VehiculoEstacionado(clave, descripcion, ocupado);
                    lista.Add(lugarEstacionamiento);
                }
            }
            conn.Close();
            return lista;
        }

        public List<Pension> RegresaPensiones()
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return null;
            }
            SqlDataReader lector;
            string strComand = "SELECT MatriculaAuto, ModeloAuto, FechaAlta, FechaVencimiento, CuotaPago, Nombre, Apellido, CajonID" +
" FROM Pensiones INNER JOIN Propietarios P on Pensiones.PropietarioID = P.ID";
            lector = UsoBD.Consulta(strComand, conn);
            if (lector == null)
            {
                errores = UsoBD.ESalida;
                conn.Close();
                return null;
            }
            List<Pension> lista = new List<Pension>();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    string matriculaAuto = lector.GetString(0);
                    string modeloAuto = lector.GetString(1);
                    DateTime fechaAlta = lector.GetDateTime(2);
                    DateTime fechaVencimiento = lector.GetDateTime(3);
                    int cuota = lector.GetInt32(4);
                    string nombre = lector.GetString(5);
                    string apellido = lector.GetString(6);
                    int cajonID = lector.GetInt32(7);
                    Pension pension = new Pension(matriculaAuto, modeloAuto, nombre,
                        apellido, fechaAlta, fechaVencimiento, cuota, cajonID);
                    lista.Add(pension);
                }
            }
            conn.Close();
            return lista;
        }


        public int DiferenciaMinutosMatricula(string matriculaAuto)
        {

            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return 0;
            }
            string strComando = "SELECT DiffMinutos=DATEDIFF(minute, HoraEntrada, GETDATE()) FROM RegistroEntradas WHERE MatriculaAuto = @matricula";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@matricula", matriculaAuto);
            int diff = 0;
            try
            {
                var lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    diff = lector.GetInt32(0);
                }
            }
            catch (SqlException e)
            {
                errores = e;
                conn.Close();
                return diff;
            }
            conn.Close();
            return diff;
        }

        public int DiferenciaMinutosCodigo(string codigoEntrada)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return 0;
            }
            string strComando = "SELECT DiffMinutos=DATEDIFF(minute, HoraEntrada, GETDATE()) FROM RegistroEntradas WHERE CodigoEntrada = @codigoEntrada";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@codigoEntrada", codigoEntrada);
            int diff = 0;
            try
            {
                var lector = cmd.ExecuteReader();
                while (lector.Read())
                {
                    diff = lector.GetInt32(0);
                }
            }
            catch (SqlException e)
            {
                errores = e;
                conn.Close();
                return diff;
            }
            conn.Close();
            return diff;
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
            string strComando = "INSERT INTO RegistroEntradas (CodigoEntrada, MatriculaAuto, CajonID)";
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

    

        public void SalidaCodigo(string codigo)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return;
            }
            string strComandoCajonID = "SELECT CajonID FROM RegistroEntradas WHERE CodigoEntrada=@codigo";
            SqlCommand cmdCajonId = new SqlCommand(strComandoCajonID, conn);
            cmdCajonId.Parameters.AddWithValue("@codigo", codigo);
            // sacar el cajon ID para luego cambiar su estado xd
            string cajonId = "";
            try
            {
                var lector = cmdCajonId.ExecuteReader();
                while (lector.Read())
                {
                    cajonId = lector.GetValue(0).ToString();
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                errores = e;
                conn.Close();
                return;
            }
            cmdCajonId.Dispose();
            conn.Close();
            conn.Open();
            // ahora que tenemos el cajon ID podemos proceder a borrar el registro de entrada y también actualizar el estado de esa cajon
            string strComandoDelete = "DELETE FROM RegistroEntradas WHERE CodigoEntrada=@codigo";
            SqlCommand cmdDelete = new SqlCommand(strComandoDelete, conn);
            cmdDelete.Parameters.AddWithValue("@codigo", codigo);
            // actualizar estado de un cajon
            string strComandoUpdate = "UPDATE Cajones SET Ocupado = 0 WHERE Clave=@clave";
            SqlCommand cmdUpdate = new SqlCommand(strComandoUpdate, conn);
            cmdUpdate.Parameters.AddWithValue("@clave", cajonId);
            try
            {
                cmdDelete.ExecuteNonQuery();
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

        public void SalidaVehiculoMatricula(string matricula)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return;
            }
            string strComandoCajonID = "SELECT CajonID FROM RegistroEntradas WHERE MatriculaAuto=@matricula";
            SqlCommand cmdCajonId = new SqlCommand(strComandoCajonID, conn);
            cmdCajonId.Parameters.AddWithValue("@matricula", matricula);
            // sacar el cajon ID para luego cambiar su estado xd
            string cajonId = "";
            try
            {
                var lector = cmdCajonId.ExecuteReader();
                while (lector.Read())
                {
                    cajonId = lector.GetValue(0).ToString();
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                errores = e;
                conn.Close();
                return;
            }
            cmdCajonId.Dispose();
            conn.Close();
            conn.Open();
            // ahora que tenemos el cajon ID podemos proceder a borrar el registro de entrada y también actualizar el estado de esa cajon
            string strComandoDelete = "DELETE FROM RegistroEntradas WHERE MatriculaAuto=@matricula";
            SqlCommand cmdDelete = new SqlCommand(strComandoDelete, conn);
            cmdDelete.Parameters.AddWithValue("@matricula", matricula);
            // actualizar estado de un cajon
            string strComandoUpdate = "UPDATE Cajones SET Ocupado = 0 WHERE Clave=@clave";
            SqlCommand cmdUpdate = new SqlCommand(strComandoUpdate, conn);
            cmdUpdate.Parameters.AddWithValue("@clave", cajonId);
            try
            {
                cmdDelete.ExecuteNonQuery();
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


        public bool MatriculaYaEstaPensionada(string matricula)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return false;
            }
            string strComando = "SELECT * FROM Pensiones WHERE MatriculaAuto = @matricula";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@matricula", matricula);
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

        public bool MatriculaYaEstaEstacionada(string matricula)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return false;
            }
            string strComando = "SELECT * FROM RegistroEntradas WHERE MatriculaAuto = @matricula";
            SqlCommand cmd = new SqlCommand(strComando, conn);
            cmd.Parameters.AddWithValue("@matricula", matricula);
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

        public bool CodigoEntradaExiste(string codigo)
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return false;
            }
            string strComando = "SELECT * FROM RegistroEntradas WHERE CodigoEntrada = @codigo";
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

        public List<VehiculoEstacionado> RegresaVehiculosEstacionados()
        {
            SqlConnection conn = UsoBD.ConectaBD(connection);
            if (conn == null)
            {
                errores = UsoBD.ESalida;
                return null;
            }
            SqlDataReader lector;
            string strComand = "SELECT CodigoEntrada, MatriculaAuto, HoraEntrada, CajonID FROM RegistroEntradas";
            lector = UsoBD.Consulta(strComand, conn);
            if (lector == null)
            {
                errores = UsoBD.ESalida;
                conn.Close();
                return null;
            }
            List<VehiculoEstacionado> lista = new List<VehiculoEstacionado>();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    string codigoEntrada = lector.GetValue(0).ToString();
                    string matriculaAuto = lector.GetValue(1).ToString();
                    string horaEntrada = lector.GetValue(2).ToString();
                    int cajonID = int.Parse(lector.GetValue(3).ToString());
                    VehiculoEstacionado vehiculoEstacionado = new VehiculoEstacionado(codigoEntrada, matriculaAuto, horaEntrada, cajonID);
                    lista.Add(vehiculoEstacionado);
                }
            }
            conn.Close();
            return lista;
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