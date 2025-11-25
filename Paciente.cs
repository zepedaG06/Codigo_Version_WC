using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    public class Paciente : Usuario
    {
        public int Edad;
        public string ContactoEmergencia;
        public List<RegistroMedico> Historial;

        public Paciente(string pacienteId, string pacienteNombre, string pacienteEmail, string pacientePassword, int pacienteEdad, string pacienteContactoEmergencia)
            : base(pacienteId, pacienteNombre, pacienteEmail, pacientePassword)
        {
            Edad = pacienteEdad;
            ContactoEmergencia = pacienteContactoEmergencia;
            Historial = new List<RegistroMedico>();
        }

        public Paciente() : base()
        {
            Historial = new List<RegistroMedico>();
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("         INFORMACION DEL PACIENTE        ");
            Console.WriteLine("==========================================");
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Nombre: " + Nombre);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Edad: " + Edad);
            Console.WriteLine("Contacto Emergencia: " + ContactoEmergencia);
            Console.WriteLine("Registros medicos: " + Historial.Count);
            Console.WriteLine("==========================================");
        }
    }
}