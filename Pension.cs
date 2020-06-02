using System;

namespace ProyectoEstacionamientos.clases
{
    class Pension
    {
        private string matriculaAuto, modelo, nombrePropietario, apellidoPropietario;
        private DateTime fechaIngreso, fechaVencimiento;
        private int cuota, cajonID;

        public Pension(string matriculaAuto, string modelo,
            string nombrePropietario, string apellidoPropietario, DateTime fechaIngreso,
            DateTime fechaVencimiento, int cuota, int cajonID)
        {
            SetMatriculaAuto(matriculaAuto);
            SetModelo(modelo);
            SetNombrePropietario(nombrePropietario);
            SetApellidoPropietario(apellidoPropietario);
            SetFechaIngreso(fechaIngreso);
            SetFechaVencimiento(fechaVencimiento);
            SetCuota(cuota);
            SetCajonID(cajonID);
        }


        public void SetCajonID(int cajonID)
        {
            this.cajonID = cajonID;
        }

        public int GetCajonID()
        {
            return this.cajonID;
        }

        public string GetMatriculaAuto()
        {
            return matriculaAuto;
        }

        public void SetMatriculaAuto(string matriculaAuto)
        {
            this.matriculaAuto = matriculaAuto;
        }

        public string GetNombrePropietario()
        {
            return nombrePropietario;
        }
        public string GetApellidoPropietario()
        {
            return apellidoPropietario;
        }

        public void SetNombrePropietario(string nombrePropietario)
        {
            this.nombrePropietario = nombrePropietario;
        }

        public void SetApellidoPropietario(string apellidoPropietario)
        {
            this.apellidoPropietario = apellidoPropietario;
        }


        public DateTime GetFechaIngreso()
        {
            return fechaIngreso;
        }

        public void SetFechaIngreso(DateTime fechaIngreso)
        {
            this.fechaIngreso = fechaIngreso;
        }

        public DateTime GetFechaVencimiento()
        {
            return fechaVencimiento;
        }

        public void SetFechaVencimiento(DateTime fechaVencimiento)
        {
            this.fechaVencimiento = fechaVencimiento;
        }

        public int GetCuota()
        {
            return cuota;
        }

        public void SetCuota(int cuota)
        {
            this.cuota = cuota;
        }

        public string GetModelo()
        {
            return modelo;
        }

        public void SetModelo(string modelo)
        {
            this.modelo = modelo;
        }
    }
}
