using System;
using System.Collections.Generic;

namespace MEDICENTER
{
    public class Sistema
    {
        public List<Paciente> Pacientes { get; set; }
        public List<PersonalHospitalario> Personal { get; set; }
        public List<Hospital> Hospitales { get; set; }
        public DecisionNode ArbolDiagnostico { get; set; }
        public Dictionary<string, Queue<string>> ColasPorHospital { get; set; }
        public Dictionary<string, List<RegistroMedico>> RegistrosPorHospital { get; set; }

        private GestorArchivos gestor;
        private int contadorPacientes;
        private int contadorPersonal;
        private int contadorRegistros;

        public Sistema()
        {
            gestor = new GestorArchivos();
            ColasPorHospital = new Dictionary<string, Queue<string>>();
            RegistrosPorHospital = new Dictionary<string, List<RegistroMedico>>();

            CargarDatos();

            if (Hospitales.Count == 0)
                InicializarHospitales();
            if (ArbolDiagnostico == null)
                InicializarArbolDiagnostico();
        }

        private void CargarDatos()
        {
            Pacientes = gestor.CargarPacientes();
            Personal = gestor.CargarPersonal();
            Hospitales = gestor.CargarHospitales();
            ArbolDiagnostico = gestor.CargarArbol();

            contadorPacientes = Pacientes.Count + 1;
            contadorPersonal = Personal.Count + 1;
            contadorRegistros = 1;

            foreach (var hospital in Hospitales)
            {
                ColasPorHospital[hospital.Id] = gestor.CargarColaPacientes(hospital.Id);
                RegistrosPorHospital[hospital.Id] = gestor.CargarRegistrosHospital(hospital.Id);

                foreach (var reg in RegistrosPorHospital[hospital.Id])
                {
                    if (!string.IsNullOrEmpty(reg.IdRegistro) &&
                        reg.IdRegistro.Length > 1 &&
                        int.TryParse(reg.IdRegistro.Substring(1), out int num))
                    {
                        if (num >= contadorRegistros)
                            contadorRegistros = num + 1;
                    }
                }
            }
        }

        public void GuardarTodosDatos()
        {
            gestor.GuardarPacientes(Pacientes);
            gestor.GuardarPersonal(Personal);
            gestor.GuardarHospitales(Hospitales);
            gestor.GuardarArbol(ArbolDiagnostico);

            foreach (var kvp in ColasPorHospital)
            {
                gestor.GuardarColaPacientes(kvp.Key, kvp.Value);
            }

            foreach (var kvp in RegistrosPorHospital)
            {
                gestor.GuardarRegistrosHospital(kvp.Key, kvp.Value);
            }
        }

        private void InicializarHospitales()
        {
            Hospitales = new List<Hospital>
            {
                new Hospital
                {
                    Id = "H001",
                    Nombre = "Hospital Nacional de Nicaragua",
                    EsPublico = true,
                    CostoConsulta = 0,
                    PrecisionDiagnostico = 85,
                    TiempoPromedioMin = 45
                },
                new Hospital
                {
                    Id = "H002",
                    Nombre = "Hospital Metropolitano Managua",
                    EsPublico = true,
                    CostoConsulta = 0,
                    PrecisionDiagnostico = 82,
                    TiempoPromedioMin = 50
                },
                new Hospital
                {
                    Id = "H003",
                    Nombre = "Clinica Premium Medical Center",
                    EsPublico = false,
                    CostoConsulta = 150.00m,
                    PrecisionDiagnostico = 95,
                    TiempoPromedioMin = 25
                },
                new Hospital
                {
                    Id = "H004",
                    Nombre = "Centro Medico VIP Salud",
                    EsPublico = false,
                    CostoConsulta = 200.00m,
                    PrecisionDiagnostico = 97,
                    TiempoPromedioMin = 20
                }
            };

            foreach (var hospital in Hospitales)
            {
                ColasPorHospital[hospital.Id] = new Queue<string>();
                RegistrosPorHospital[hospital.Id] = new List<RegistroMedico>();
            }

            InicializarAdministradorPorDefecto();
        }

        private void InicializarAdministradorPorDefecto()
        {
            if (Personal.Exists(p => p.Id == "ADMIN001"))
                return;

            PersonalHospitalario adminDefault = new PersonalHospitalario
            {
                Id = "ADMIN001",
                Nombre = "Administrador General",
                Email = "admin@medicenter.com",
                Password = "admin123",
                IdHospital = "H001",
                NivelAcceso = NivelAcceso.Administrador,
                CambioPassword = true
            };

            Personal.Add(adminDefault);
            Hospitales[0].PersonalIds.Add("ADMIN001");

            Console.WriteLine();
            Console.WriteLine("Administrador por defecto creado:");
            Console.WriteLine("ID: ADMIN001");
            Console.WriteLine("Password: admin123");
            Console.WriteLine();
        }

        private void InicializarArbolDiagnostico()
        {
            ArbolDiagnostico = new DecisionNode("root", "Tiene fiebre?");

            DecisionNode nodoFiebreSi = new DecisionNode("fiebre_si", "Tiene tos persistente?");
            nodoFiebreSi.RespuestaEsperada = "si";

            DecisionNode nodoTosSi = new DecisionNode("tos_si", "Tiene dificultad para respirar?");
            nodoTosSi.RespuestaEsperada = "si";

            DecisionNode diag1 = new DecisionNode("diag1", "CRITICO: Posible neumonia o COVID-19. Consulte medico inmediatamente", true);
            diag1.RespuestaEsperada = "si";

            DecisionNode diag2 = new DecisionNode("diag2", "Posible gripe o resfriado fuerte. Reposo y monitoreo", true);
            diag2.RespuestaEsperada = "no";

            nodoTosSi.AgregarHijo(diag1);
            nodoTosSi.AgregarHijo(diag2);

            DecisionNode nodoTosNo = new DecisionNode("tos_no", "Tiene dolor de cabeza intenso?");
            nodoTosNo.RespuestaEsperada = "no";

            DecisionNode diag3 = new DecisionNode("diag3", "Posible migraña o infeccion. Consulte medico", true);
            diag3.RespuestaEsperada = "si";

            DecisionNode diag4 = new DecisionNode("diag4", "Fiebre leve. Reposo y medicamento basico", true);
            diag4.RespuestaEsperada = "no";

            nodoTosNo.AgregarHijo(diag3);
            nodoTosNo.AgregarHijo(diag4);

            nodoFiebreSi.AgregarHijo(nodoTosSi);
            nodoFiebreSi.AgregarHijo(nodoTosNo);

            DecisionNode nodoFiebreNo = new DecisionNode("fiebre_no", "Tiene nauseas o dolor abdominal?");
            nodoFiebreNo.RespuestaEsperada = "no";

            DecisionNode diag5 = new DecisionNode("diag5", "Posible problema gastrointestinal. Dieta ligera", true);
            diag5.RespuestaEsperada = "si";

            DecisionNode diag6 = new DecisionNode("diag6", "Sintomas leves. Monitoreo general", true);
            diag6.RespuestaEsperada = "no";

            nodoFiebreNo.AgregarHijo(diag5);
            nodoFiebreNo.AgregarHijo(diag6);

            ArbolDiagnostico.AgregarHijo(nodoFiebreSi);
            ArbolDiagnostico.AgregarHijo(nodoFiebreNo);
        }

        public Paciente BuscarPaciente(string id, string password)
        {
            return Pacientes.Find(p => p.Id == id && p.Password == password);
        }

        public PersonalHospitalario BuscarPersonal(string id, string password)
        {
            return Personal.Find(p => p.Id == id && p.Password == password);
        }

        public Hospital BuscarHospital(string id)
        {
            return Hospitales.Find(h => h.Id == id);
        }

        public List<Hospital> ObtenerHospitalesDisponibles(TipoSeguro seguro)
        {
            List<Hospital> disponibles = new List<Hospital>();

            switch (seguro)
            {
                case TipoSeguro.SeguroCompleto:
                    return new List<Hospital>(Hospitales);
                case TipoSeguro.SeguroBasico:
                case TipoSeguro.SinSeguro:
                    disponibles = Hospitales.FindAll(h => h.EsPublico);
                    break;
            }

            return disponibles;
        }

        public List<Hospital> ObtenerHospitalesPrivados()
        {
            return Hospitales.FindAll(h => !h.EsPublico);
        }

        public string GenerarIdPaciente()
        {
            string id = $"P{contadorPacientes:D4}";
            contadorPacientes++;
            return id;
        }

        public string GenerarIdPersonal()
        {
            string id = $"M{contadorPersonal:D4}";
            contadorPersonal++;
            return id;
        }

        public string GenerarIdRegistro()
        {
            string id = $"R{contadorRegistros:D5}";
            contadorRegistros++;
            return id;
        }
    }
}