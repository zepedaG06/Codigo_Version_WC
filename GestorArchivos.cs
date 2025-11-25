using System;
using System.Collections.Generic;
using System.IO;

namespace MEDICENTER
{
    public class GestorArchivos
    {
        private const string CARPETA_DATOS = "DatosMediCenter";
        private const string CARPETA_HOSPITALES = "DatosMediCenter/Hospitales";

        public GestorArchivos()
        {
            if (!Directory.Exists(CARPETA_DATOS))
                Directory.CreateDirectory(CARPETA_DATOS);
            if (!Directory.Exists(CARPETA_HOSPITALES))
                Directory.CreateDirectory(CARPETA_HOSPITALES);
        }

        public void GuardarPacientes(List<Paciente> pacientes)
        {
            string ruta = Path.Combine(CARPETA_DATOS, "pacientes.dat");
            using (BinaryWriter writer = new BinaryWriter(File.Open(ruta, FileMode.Create)))
            {
                writer.Write(pacientes.Count);
                foreach (var p in pacientes)
                {
                    writer.Write(p.Id ?? "");
                    writer.Write(p.Nombre ?? "");
                    writer.Write(p.Email ?? "");
                    writer.Write(p.Password ?? "");
                    writer.Write(p.FechaRegistro.ToBinary());
                    writer.Write(p.Edad);
                    writer.Write(p.Telefono ?? "");
                    writer.Write((int)p.Genero);
                    writer.Write((int)p.TipoSangre);
                    writer.Write((int)p.TipoSeguro);
                    writer.Write(p.NumeroSeguro ?? "");
                    writer.Write(p.ContactoEmergencia ?? "");

                    writer.Write(p.Historial.Count);
                    foreach (var reg in p.Historial)
                    {
                        EscribirRegistroMedico(writer, reg);
                    }
                }
            }
        }

        public List<Paciente> CargarPacientes()
        {
            string ruta = Path.Combine(CARPETA_DATOS, "pacientes.dat");
            if (!File.Exists(ruta))
                return new List<Paciente>();

            try
            {
                List<Paciente> pacientes = new List<Paciente>();
                using (BinaryReader reader = new BinaryReader(File.Open(ruta, FileMode.Open)))
                {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        Paciente p = new Paciente();
                        p.Id = reader.ReadString();
                        p.Nombre = reader.ReadString();
                        p.Email = reader.ReadString();
                        p.Password = reader.ReadString();
                        p.FechaRegistro = DateTime.FromBinary(reader.ReadInt64());
                        p.Edad = reader.ReadInt32();
                        p.Telefono = reader.ReadString();
                        p.Genero = (Genero)reader.ReadInt32();
                        p.TipoSangre = (TipoSangre)reader.ReadInt32();
                        p.TipoSeguro = (TipoSeguro)reader.ReadInt32();
                        p.NumeroSeguro = reader.ReadString();
                        p.ContactoEmergencia = reader.ReadString();

                        int histCount = reader.ReadInt32();
                        for (int j = 0; j < histCount; j++)
                        {
                            p.Historial.Add(LeerRegistroMedico(reader));
                        }

                        pacientes.Add(p);
                    }
                }
                return pacientes;
            }
            catch (Exception)
            {
                File.Delete(ruta);
                return new List<Paciente>();
            }
        }

        public void GuardarPersonal(List<PersonalHospitalario> personal)
        {
            string ruta = Path.Combine(CARPETA_DATOS, "personal.dat");
            using (BinaryWriter writer = new BinaryWriter(File.Open(ruta, FileMode.Create)))
            {
                writer.Write(personal.Count);
                foreach (var p in personal)
                {
                    writer.Write(p.Id ?? "");
                    writer.Write(p.Nombre ?? "");
                    writer.Write(p.Email ?? "");
                    writer.Write(p.Password ?? "");
                    writer.Write(p.FechaRegistro.ToBinary());
                    writer.Write(p.IdHospital ?? "");
                    writer.Write((int)p.NivelAcceso);
                    writer.Write(p.Especialidad ?? "");
                    writer.Write(p.CambioPassword);

                    writer.Write(p.PacientesAsignados.Count);
                    foreach (var id in p.PacientesAsignados)
                    {
                        writer.Write(id ?? "");
                    }
                }
            }
        }

        public List<PersonalHospitalario> CargarPersonal()
        {
            string ruta = Path.Combine(CARPETA_DATOS, "personal.dat");
            if (!File.Exists(ruta))
                return new List<PersonalHospitalario>();

            List<PersonalHospitalario> personal = new List<PersonalHospitalario>();
            using (BinaryReader reader = new BinaryReader(File.Open(ruta, FileMode.Open)))
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    PersonalHospitalario p = new PersonalHospitalario();
                    p.Id = reader.ReadString();
                    p.Nombre = reader.ReadString();
                    p.Email = reader.ReadString();
                    p.Password = reader.ReadString();
                    p.FechaRegistro = DateTime.FromBinary(reader.ReadInt64());
                    p.IdHospital = reader.ReadString();
                    p.NivelAcceso = (NivelAcceso)reader.ReadInt32();
                    p.Especialidad = reader.ReadString();
                    p.CambioPassword = reader.ReadBoolean();

                    int pacCount = reader.ReadInt32();
                    for (int j = 0; j < pacCount; j++)
                    {
                        p.PacientesAsignados.Add(reader.ReadString());
                    }

                    personal.Add(p);
                }
            }
            return personal;
        }

        public void GuardarHospitales(List<Hospital> hospitales)
        {
            string ruta = Path.Combine(CARPETA_DATOS, "hospitales.dat");
            using (BinaryWriter writer = new BinaryWriter(File.Open(ruta, FileMode.Create)))
            {
                writer.Write(hospitales.Count);
                foreach (var h in hospitales)
                {
                    writer.Write(h.Id ?? "");
                    writer.Write(h.Nombre ?? "");
                    writer.Write(h.EsPublico);
                    writer.Write(h.CostoConsulta);
                    writer.Write(h.PrecisionDiagnostico);
                    writer.Write(h.TiempoPromedioMin);

                    writer.Write(h.PersonalIds.Count);
                    foreach (var id in h.PersonalIds)
                    {
                        writer.Write(id ?? "");
                    }

                    writer.Write(h.PacientesAtendidos.Count);
                    foreach (var id in h.PacientesAtendidos)
                    {
                        writer.Write(id ?? "");
                    }
                }
            }
        }

        public List<Hospital> CargarHospitales()
        {
            string ruta = Path.Combine(CARPETA_DATOS, "hospitales.dat");
            if (!File.Exists(ruta))
                return new List<Hospital>();

            List<Hospital> hospitales = new List<Hospital>();
            using (BinaryReader reader = new BinaryReader(File.Open(ruta, FileMode.Open)))
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    Hospital h = new Hospital();
                    h.Id = reader.ReadString();
                    h.Nombre = reader.ReadString();
                    h.EsPublico = reader.ReadBoolean();
                    h.CostoConsulta = reader.ReadDecimal();
                    h.PrecisionDiagnostico = reader.ReadInt32();
                    h.TiempoPromedioMin = reader.ReadInt32();

                    int persCount = reader.ReadInt32();
                    for (int j = 0; j < persCount; j++)
                    {
                        h.PersonalIds.Add(reader.ReadString());
                    }

                    int pacCount = reader.ReadInt32();
                    for (int j = 0; j < pacCount; j++)
                    {
                        h.PacientesAtendidos.Add(reader.ReadString());
                    }

                    hospitales.Add(h);
                }
            }
            return hospitales;
        }

        public void GuardarRegistrosHospital(string idHospital, List<RegistroMedico> registros)
        {
            string ruta = Path.Combine(CARPETA_HOSPITALES, idHospital + "_registros.dat");
            using (BinaryWriter writer = new BinaryWriter(File.Open(ruta, FileMode.Create)))
            {
                writer.Write(registros.Count);
                foreach (var reg in registros)
                {
                    EscribirRegistroMedico(writer, reg);
                }
            }
        }

        public List<RegistroMedico> CargarRegistrosHospital(string idHospital)
        {
            string ruta = Path.Combine(CARPETA_HOSPITALES, idHospital + "_registros.dat");
            if (!File.Exists(ruta))
                return new List<RegistroMedico>();

            List<RegistroMedico> registros = new List<RegistroMedico>();
            using (BinaryReader reader = new BinaryReader(File.Open(ruta, FileMode.Open)))
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    registros.Add(LeerRegistroMedico(reader));
                }
            }
            return registros;
        }

        public void GuardarColaPacientes(string idHospital, Queue<string> cola)
        {
            string ruta = Path.Combine(CARPETA_HOSPITALES, idHospital + "_cola.dat");
            using (BinaryWriter writer = new BinaryWriter(File.Open(ruta, FileMode.Create)))
            {
                writer.Write(cola.Count);
                foreach (var item in cola)
                {
                    writer.Write(item ?? "");
                }
            }
        }

        public Queue<string> CargarColaPacientes(string idHospital)
        {
            string ruta = Path.Combine(CARPETA_HOSPITALES, idHospital + "_cola.dat");
            if (!File.Exists(ruta))
                return new Queue<string>();

            Queue<string> cola = new Queue<string>();
            using (BinaryReader reader = new BinaryReader(File.Open(ruta, FileMode.Open)))
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    cola.Enqueue(reader.ReadString());
                }
            }
            return cola;
        }

        public void GuardarArbol(DecisionNode raiz)
        {
            string ruta = Path.Combine(CARPETA_DATOS, "arbol_diagnostico.dat");
            using (BinaryWriter writer = new BinaryWriter(File.Open(ruta, FileMode.Create)))
            {
                EscribirNodo(writer, raiz);
            }
        }

        public DecisionNode CargarArbol()
        {
            string ruta = Path.Combine(CARPETA_DATOS, "arbol_diagnostico.dat");
            if (!File.Exists(ruta))
                return null;

            using (BinaryReader reader = new BinaryReader(File.Open(ruta, FileMode.Open)))
            {
                return LeerNodo(reader);
            }
        }

        private void EscribirRegistroMedico(BinaryWriter writer, RegistroMedico reg)
        {
            writer.Write(reg.IdRegistro ?? "");
            writer.Write(reg.IdPaciente ?? "");
            writer.Write(reg.IdHospital ?? "");
            writer.Write(reg.Fecha.ToBinary());

            writer.Write(reg.Sintomas.Count);
            foreach (var s in reg.Sintomas)
            {
                writer.Write(s ?? "");
            }

            writer.Write(reg.Diagnostico ?? "");
            writer.Write(reg.Tratamiento ?? "");
            writer.Write(reg.Confirmado);
            writer.Write(reg.IdMedico ?? "");
            writer.Write(reg.ObservacionDoctor ?? "");
        }

        private RegistroMedico LeerRegistroMedico(BinaryReader reader)
        {
            RegistroMedico reg = new RegistroMedico();
            reg.IdRegistro = reader.ReadString();
            reg.IdPaciente = reader.ReadString();
            reg.IdHospital = reader.ReadString();
            reg.Fecha = DateTime.FromBinary(reader.ReadInt64());

            int sintCount = reader.ReadInt32();
            for (int i = 0; i < sintCount; i++)
            {
                reg.Sintomas.Add(reader.ReadString());
            }

            reg.Diagnostico = reader.ReadString();
            reg.Tratamiento = reader.ReadString();
            reg.Confirmado = reader.ReadBoolean();
            reg.IdMedico = reader.ReadString();
            reg.ObservacionDoctor = reader.ReadString();

            return reg;
        }

        private void EscribirNodo(BinaryWriter writer, DecisionNode nodo)
        {
            if (nodo == null)
            {
                writer.Write(false);
                return;
            }

            writer.Write(true);
            writer.Write(nodo.Id ?? "");
            writer.Write(nodo.Pregunta ?? "");
            writer.Write(nodo.Diagnostico ?? "");
            writer.Write(nodo.RespuestaEsperada ?? "");

            writer.Write(nodo.Hijos.Count);
            foreach (var hijo in nodo.Hijos)
            {
                EscribirNodo(writer, hijo);
            }
        }

        private DecisionNode LeerNodo(BinaryReader reader)
        {
            bool existe = reader.ReadBoolean();
            if (!existe)
                return null;

            string id = reader.ReadString();
            string pregunta = reader.ReadString();
            string diagnostico = reader.ReadString();
            string respuesta = reader.ReadString();

            DecisionNode nodo;
            if (!string.IsNullOrEmpty(diagnostico))
            {
                nodo = new DecisionNode(id, diagnostico, true);
            }
            else
            {
                nodo = new DecisionNode(id, pregunta);
            }

            nodo.RespuestaEsperada = respuesta;

            int hijosCount = reader.ReadInt32();
            for (int i = 0; i < hijosCount; i++)
            {
                DecisionNode hijo = LeerNodo(reader);
                if (hijo != null)
                {
                    nodo.AgregarHijo(hijo);
                }
            }

            return nodo;
        }
    }
}