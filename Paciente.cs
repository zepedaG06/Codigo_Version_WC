using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MEDICENTER
{
    public class Paciente : Usuario
    {
        public int Edad { get; set; }
        public string Telefono { get; set; }
        public Genero Genero { get; set; }
        public TipoSangre TipoSangre { get; set; }
        public TipoSeguro TipoSeguro { get; set; }
        public string NumeroSeguro { get; set; }
        public string ContactoEmergencia { get; set; }
        public List<RegistroMedico> Historial { get; set; }

        public Paciente() : base()
        {
            Historial = new List<RegistroMedico>();
        }

        public void MostrarInformacion()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║         INFORMACION DEL PACIENTE                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.WriteLine($"  ID: {Id}");
            Console.WriteLine($"  Nombre: {Nombre}");
            Console.WriteLine($"  Email: {Email}");
            Console.WriteLine($"  Edad: {Edad} años");
            Console.WriteLine($"  Telefono: {Telefono}");
            Console.WriteLine($"  Genero: {Genero}");
            Console.WriteLine($"  Tipo de Sangre: {FormatearTipoSangre(TipoSangre)}");
            Console.WriteLine($"  Seguro Medico: {FormatearSeguro(TipoSeguro)}");
            if (!string.IsNullOrEmpty(NumeroSeguro))
                Console.WriteLine($"  Numero de Seguro: {NumeroSeguro}");
            Console.WriteLine($"  Contacto Emergencia: {ContactoEmergencia}");
            Console.WriteLine($"  Registros Medicos: {Historial.Count}");
            Console.WriteLine("════════════════════════════════════════════════════");
        }

        private string FormatearTipoSangre(TipoSangre tipo)
        {
            return tipo.ToString().Replace("_Positivo", "+").Replace("_Negativo", "-");
        }

        private string FormatearSeguro(TipoSeguro tipo)
        {
            switch (tipo)
            {
                case TipoSeguro.SinSeguro: return "Sin Seguro";
                case TipoSeguro.SeguroBasico: return "Seguro Basico";
                case TipoSeguro.SeguroCompleto: return "Seguro Completo";
                default: return "No especificado";
            }
        }
    }
}