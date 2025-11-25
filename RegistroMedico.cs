using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    public class RegistroMedico
    {
        public string IdRegistro { get; set; }
        public string IdPaciente { get; set; }
        public string IdHospital { get; set; }
        public DateTime Fecha { get; set; }
        public List<string> Sintomas { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public bool Confirmado { get; set; }
        public string IdMedico { get; set; }
        public string ObservacionDoctor { get; set; }

        public RegistroMedico()
        {
            Sintomas = new List<string>();
            Fecha = DateTime.Now;
            Confirmado = false;
        }

        public void MostrarRegistro()
        {
            Console.WriteLine("\n────────────────────────────────────────────────────");
            Console.WriteLine($"  ID Registro: {IdRegistro}");
            Console.WriteLine($"  Fecha: {Fecha:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"  Hospital: {IdHospital}");
            Console.WriteLine($"  Sintomas: {string.Join(", ", Sintomas)}");
            Console.WriteLine($"  Diagnostico: {Diagnostico}");
            if (!string.IsNullOrEmpty(Tratamiento))
                Console.WriteLine($"  Tratamiento: {Tratamiento}");
            Console.WriteLine($"  Estado: {(Confirmado ? "Confirmado" : "Pendiente")}");
            if (!string.IsNullOrEmpty(IdMedico))
                Console.WriteLine($"  Medico: {IdMedico}");
            if (!string.IsNullOrEmpty(ObservacionDoctor))
                Console.WriteLine($"  Observaciones: {ObservacionDoctor}");
            Console.WriteLine("────────────────────────────────────────────────────");
        }
    }
}