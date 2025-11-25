using System;

namespace MEDICENTER
{
    public class Usuario
    {
        public string Id;
        public string Nombre;
        public string Email;
        public string Password;

        public Usuario(string usuarioId, string usuarioNombre, string usuarioEmail, string usuarioPassword)
        {
            Id = usuarioId;
            Nombre = usuarioNombre;
            Email = usuarioEmail;
            Password = usuarioPassword;
        }

        public Usuario()
        {
        }
    }
}