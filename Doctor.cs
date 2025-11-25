using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    public class Doctor : Usuario
    {
        public string Especialidad;
        public List<string> PacientesAsignados;

        public Doctor(string doctorId, string doctorNombre, string doctorEmail, string doctorPassword, string doctorEspecialidad)
            : base(doctorId, doctorNombre, doctorEmail, doctorPassword)
        {
            Especialidad = doctorEspecialidad;
            PacientesAsignados = new List<string>();
        }

        public Doctor() : base()
        {
            PacientesAsignados = new List<string>();
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("         INFORMACION DEL DOCTOR          ");
            Console.WriteLine("==========================================");
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Nombre: " + Nombre);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Especialidad: " + Especialidad);
            Console.WriteLine("Pacientes asignados: " + PacientesAsignados.Count);
            Console.WriteLine("==========================================");
        }
    }
}