using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MEDICENTER
{
    public class PersonalHospitalario : Usuario
    {
        public string IdHospital { get; set; }
        public NivelAcceso NivelAcceso { get; set; }
        public string Especialidad { get; set; }
        public List<string> PacientesAsignados { get; set; }
        public bool CambioPassword { get; set; }

        public PersonalHospitalario() : base()
        {
            PacientesAsignados = new List<string>();
            CambioPassword = false;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║         INFORMACION DEL PERSONAL                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.WriteLine($"  ID: {Id}");
            Console.WriteLine($"  Nombre: {Nombre}");
            Console.WriteLine($"  Email: {Email}");
            Console.WriteLine($"  Hospital: {IdHospital}");
            Console.WriteLine($"  Nivel de Acceso: {NivelAcceso}");
            if (!string.IsNullOrEmpty(Especialidad))
                Console.WriteLine($"  Especialidad: {Especialidad}");
            Console.WriteLine($"  Pacientes Asignados: {PacientesAsignados.Count}");
            Console.WriteLine("════════════════════════════════════════════════════");
        }
    }
}