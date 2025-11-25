using System;

namespace MEDICENTER
{
    public enum TipoSeguro
    {
        SinSeguro,
        SeguroBasico,
        SeguroCompleto
    }

    [Serializable]
    public enum TipoSangre
    {
        A_Positivo,
        A_Negativo,
        B_Positivo,
        B_Negativo,
        O_Positivo,
        O_Negativo,
        AB_Positivo,
        AB_Negativo
    }

    public enum Genero
    {
        Masculino,
        Femenino,
        Otro
    }

    public enum NivelAcceso
    {
        MedicoGeneral,
        Administrador
    }

    public class Usuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Usuario()
        {
            FechaRegistro = DateTime.Now;
        }

        public Usuario(string id, string nombre, string email, string password)
        {
            Id = id;
            Nombre = nombre;
            Email = email;
            Password = password;
            FechaRegistro = DateTime.Now;
        }
    }
}