namespace ProyectoEstacionamientos
{
    class VehiculoEstacionado
    {
        public VehiculoEstacionado(string codigoEntrada, string matriculaAuto, string horaEntrada, int cajonID)
        {
            CodigoEntrada = codigoEntrada;
            MatriculaAuto = matriculaAuto;
            HoraEntrada = horaEntrada;
            CajonID = cajonID;
        }

        public string CodigoEntrada { get; set; }
        public string MatriculaAuto { get; set; }
        public string HoraEntrada { get; set; }
        public int CajonID { get; set; }

    }
}
