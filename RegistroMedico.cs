using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    public class RegistroMedico
    {
        public string IdRegistro;
        public DateTime Fecha;
        public List<string> Sintomas;
        public string Diagnostico;
        public bool Confirmado;
        public string ObservacionDoctor;

        public RegistroMedico()
        {
            Sintomas = new List<string>();
            Fecha = DateTime.Now;
            Confirmado = false;
        }

        public void MostrarRegistro()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("ID Registro: " + IdRegistro);
            Console.WriteLine("Fecha: " + Fecha.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("Sintomas: " + string.Join(", ", Sintomas));
            Console.WriteLine("Diagnostico: " + Diagnostico);
            Console.WriteLine("Confirmado por doctor: " + (Confirmado ? "Si" : "No"));

            if (!string.IsNullOrEmpty(ObservacionDoctor))
            {
                Console.WriteLine("Observaciones: " + ObservacionDoctor);
            }
            Console.WriteLine("------------------------------------------");
        }
    }
}