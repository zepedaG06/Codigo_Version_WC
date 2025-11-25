using MEDICENTER;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace MEDICENTER
{
    public class Sistema
    {
        public List<Paciente> pacientes;
        public List<Doctor> doctores;
        public Queue<string> colaPacientes;
        public DecisionNode arbolDiagnosticoRoot;
        public string[] sintomasFrecuentes;
        public int[,] statsClinica;
        public Dictionary<string, List<string>> grafoSintomas;
        public int contadorPacientes;
        public int contadorDoctores;
        public int contadorRegistros;

        public Sistema()
        {
            pacientes = new List<Paciente>();
            doctores = new List<Doctor>();
            colaPacientes = new Queue<string>();
            contadorPacientes = 1;
            contadorDoctores = 1;
            contadorRegistros = 1;

            sintomasFrecuentes = new string[]
            {
                "Fiebre",
                "Tos",
                "Dolor de cabeza",
                "Dolor de garganta",
                "Fatiga",
                "Nauseas",
                "Dolor abdominal",
                "Dificultad para respirar",
                "Mareos",
                "Dolor muscular"
            };

            statsClinica = new int[4, 2]
            {
                { 85, 45 },
                { 92, 30 },
                { 78, 60 },
                { 88, 40 }
            };

            grafoSintomas = new Dictionary<string, List<string>>();
            InicializarGrafoSintomas();
            InicializarDatosPrueba();
            InicializarArbolDiagnostico();
        }

        public void InicializarDatosPrueba()
        {
            Paciente pacienteTest = new Paciente(
                "P001",
                "Paciente Test",
                "paciente@test.com",
                "pass123",
                30,
                "118 - Policia Nacional de Nicaragua"
            );

            pacientes.Add(pacienteTest);
            contadorPacientes = 2;

            Doctor doctorTest = new Doctor(
                "D001",
                "Doctor Test",
                "doctor@test.com",
                "doc123",
                "Medicina General"
            );

            doctores.Add(doctorTest);
            contadorDoctores = 2;
        }

        public void InicializarGrafoSintomas()
        {
            grafoSintomas.Add("Fiebre", new List<string> { "Dolor de cabeza", "Fatiga", "Dolor muscular" });
            grafoSintomas.Add("Tos", new List<string> { "Dolor de garganta", "Dificultad para respirar" });
            grafoSintomas.Add("Dolor de cabeza", new List<string> { "Fiebre", "Mareos", "Fatiga" });
            grafoSintomas.Add("Nauseas", new List<string> { "Dolor abdominal", "Mareos" });
            grafoSintomas.Add("Dificultad para respirar", new List<string> { "Tos", "Fatiga" });
        }

        public void InicializarArbolDiagnostico()
        {
            arbolDiagnosticoRoot = new DecisionNode("root", "Tiene fiebre?");

            DecisionNode nodoFiebreSi = new DecisionNode("fiebre_si", "Tiene tos persistente?");
            nodoFiebreSi.RespuestaEsperada = "si";

            DecisionNode nodoTosSi = new DecisionNode("tos_si", "Tiene dificultad para respirar?");
            nodoTosSi.RespuestaEsperada = "si";

            DecisionNode diagnostico1 = new DecisionNode("diag1", "CRITICO - Posible Neumonia o COVID-19. CONSULTE DOCTOR INMEDIATAMENTE", true);
            diagnostico1.RespuestaEsperada = "si";

            DecisionNode diagnostico2 = new DecisionNode("diag2", "Posible Gripe o Resfriado Fuerte. Reposo y monitoreo", true);
            diagnostico2.RespuestaEsperada = "no";

            nodoTosSi.AgregarHijo(diagnostico1);
            nodoTosSi.AgregarHijo(diagnostico2);

            DecisionNode nodoTosNo = new DecisionNode("tos_no", "Tiene dolor de cabeza intenso?");
            nodoTosNo.RespuestaEsperada = "no";

            DecisionNode diagnostico3 = new DecisionNode("diag3", "Posible Migrana o Infeccion. Consulte medico", true);
            diagnostico3.RespuestaEsperada = "si";

            DecisionNode diagnostico4 = new DecisionNode("diag4", "Fiebre leve. Reposo y medicamento basico", true);
            diagnostico4.RespuestaEsperada = "no";

            nodoTosNo.AgregarHijo(diagnostico3);
            nodoTosNo.AgregarHijo(diagnostico4);

            nodoFiebreSi.AgregarHijo(nodoTosSi);
            nodoFiebreSi.AgregarHijo(nodoTosNo);

            DecisionNode nodoFiebreNo = new DecisionNode("fiebre_no", "Tiene nauseas o dolor abdominal?");
            nodoFiebreNo.RespuestaEsperada = "no";

            DecisionNode diagnostico5 = new DecisionNode("diag5", "Posible problema gastrointestinal. Dieta ligera", true);
            diagnostico5.RespuestaEsperada = "si";

            DecisionNode diagnostico6 = new DecisionNode("diag6", "Sintomas leves. Monitoreo general", true);
            diagnostico6.RespuestaEsperada = "no";

            nodoFiebreNo.AgregarHijo(diagnostico5);
            nodoFiebreNo.AgregarHijo(diagnostico6);

            arbolDiagnosticoRoot.AgregarHijo(nodoFiebreSi);
            arbolDiagnosticoRoot.AgregarHijo(nodoFiebreNo);
        }

        public void RegistrarPaciente()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("      REGISTRO DE NUEVO PACIENTE");
            Console.WriteLine("========================================");

            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("\nError: El nombre no puede estar vacio.");
                return;
            }

            Console.Write("Email: ");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("\nError: Email invalido.");
                return;
            }

            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                Console.WriteLine("\nError: La contraseña debe tener al menos 6 caracteres.");
                return;
            }

            Console.Write("Edad: ");
            int edad = 0;

            if (!int.TryParse(Console.ReadLine(), out edad) || edad < 0 || edad > 120)
            {
                Console.WriteLine("\nError: Edad invalida. Debe estar entre 0 y 120 años.");
                return;
            }

            Console.Write("Contacto de emergencia (ej: 118 - Policia): ");
            string contacto = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(contacto))
            {
                Console.WriteLine("\nError: El contacto de emergencia no puede estar vacio.");
                return;
            }

            string id = "P" + contadorPacientes.ToString().PadLeft(3, '0');
            contadorPacientes++;

            Paciente nuevoPaciente = new Paciente(id, nombre, email, password, edad, contacto);
            pacientes.Add(nuevoPaciente);

            Console.WriteLine("\nPaciente registrado exitosamente!");
            Console.WriteLine("Su ID es: " + id);
            Console.WriteLine("Guarde su ID y password para iniciar sesion.");
        }

        public void RegistrarDoctor()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("       REGISTRO DE NUEVO DOCTOR");
            Console.WriteLine("========================================");

            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("\nError: El nombre no puede estar vacio.");
                return;
            }

            Console.Write("Email: ");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("\nError: Email invalido.");
                return;
            }

            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                Console.WriteLine("\nError: La contraseña debe tener al menos 6 caracteres.");
                return;
            }

            Console.Write("Especialidad: ");
            string especialidad = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(especialidad))
            {
                Console.WriteLine("\nError: La especialidad no puede estar vacia.");
                return;
            }

            string id = "D" + contadorDoctores.ToString().PadLeft(3, '0');
            contadorDoctores++;

            Doctor nuevoDoctor = new Doctor(id, nombre, email, password, especialidad);
            doctores.Add(nuevoDoctor);

            Console.WriteLine("\nDoctor registrado exitosamente!");
            Console.WriteLine("Su ID es: " + id);
            Console.WriteLine("Guarde su ID y password para iniciar sesion.");
        }

        public Paciente BuscarPaciente(string id, string password)
        {
            foreach (Paciente p in pacientes)
            {
                if (p.Id == id && p.Password == password)
                {
                    return p;
                }
            }
            return null;
        }

        public Doctor BuscarDoctor(string id, string password)
        {
            foreach (Doctor d in doctores)
            {
                if (d.Id == id && d.Password == password)
                {
                    return d;
                }
            }
            return null;
        }
        public void IngresarSintomas(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("        INGRESAR SINTOMAS");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Seleccionar de sintomas frecuentes");
            Console.WriteLine("2. Escribir sintomas manualmente");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();

            RegistroMedico nuevoRegistro = new RegistroMedico();
            nuevoRegistro.IdRegistro = "R" + contadorRegistros.ToString().PadLeft(4, '0');
            contadorRegistros++;

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("\nSintomas frecuentes:");

                    for (int i = 0; i < sintomasFrecuentes.Length; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + sintomasFrecuentes[i]);
                    }

                    Console.WriteLine("\nIngrese los numeros de sintomas separados por coma (ej: 1,3,5):");
                    Console.Write("Sintomas: ");
                    string entrada = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(entrada))
                    {
                        Console.WriteLine("\nError: Debe ingresar al menos un sintoma.");
                        return;
                    }

                    string[] numeros = entrada.Split(',');

                    foreach (string num in numeros)
                    {
                        int indice;
                        if (int.TryParse(num.Trim(), out indice))
                        {
                            if (indice >= 1 && indice <= sintomasFrecuentes.Length)
                            {
                                nuevoRegistro.Sintomas.Add(sintomasFrecuentes[indice - 1]);
                            }
                            else
                            {
                                Console.WriteLine("\nAdvertencia: El numero " + indice + " no es valido.");
                            }
                        }
                    }
                    break;

                case "2":
                    Console.WriteLine("\nEscriba sus sintomas separados por coma:");
                    Console.Write("Sintomas: ");
                    string sintomasTexto = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(sintomasTexto))
                    {
                        Console.WriteLine("\nError: Debe ingresar al menos un sintoma.");
                        return;
                    }

                    string[] sintomasArray = sintomasTexto.Split(',');

                    foreach (string sintoma in sintomasArray)
                    {
                        string sintomaLimpio = sintoma.Trim();
                        if (!string.IsNullOrWhiteSpace(sintomaLimpio))
                        {
                            nuevoRegistro.Sintomas.Add(sintomaLimpio);
                        }
                    }
                    break;

                default:
                    Console.WriteLine("\nOpcion no valida. Operacion cancelada.");
                    return;
            }

            if (nuevoRegistro.Sintomas.Count > 0)
            {
                nuevoRegistro.Diagnostico = "Pendiente de diagnostico";
                paciente.Historial.Add(nuevoRegistro);

                Console.WriteLine("\nSintomas registrados exitosamente!");
                Console.WriteLine("ID del registro: " + nuevoRegistro.IdRegistro);
            }
            else
            {
                Console.WriteLine("\nNo se ingresaron sintomas validos.");
            }
        }

        public void ObtenerDiagnosticoInteractivo(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("     DIAGNOSTICO AUTOMATICO");
            Console.WriteLine("========================================");

            RegistroMedico registroPendiente = null;

            foreach (RegistroMedico reg in paciente.Historial)
            {
                if (reg.Diagnostico.Contains("Pendiente"))
                {
                    registroPendiente = reg;
                    break;
                }
            }

            if (registroPendiente == null)
            {
                Console.WriteLine("\nNo hay registros pendientes de diagnostico.");
                Console.WriteLine("Primero ingrese sintomas (opcion 1).");
                return;
            }

            Console.WriteLine("Responda las siguientes preguntas con 'si' o 'no':\n");

            DecisionNode nodoActual = arbolDiagnosticoRoot;

            while (!nodoActual.EsHoja())
            {
                Console.Write(nodoActual.Pregunta + " (si/no): ");
                string respuesta = Console.ReadLine().ToLower().Trim();

                bool encontrado = false;

                foreach (DecisionNode hijo in nodoActual.Hijos)
                {
                    if (hijo.RespuestaEsperada == respuesta)
                    {
                        nodoActual = hijo;
                        encontrado = true;
                        break;
                    }
                }

                if (!encontrado)
                {
                    Console.WriteLine("Respuesta invalida. Use 'si' o 'no'.");
                }
            }

            string diagnosticoFinal = nodoActual.Diagnostico;
            registroPendiente.Diagnostico = diagnosticoFinal;

            Console.WriteLine("\n========================================");
            Console.WriteLine("         RESULTADO DEL DIAGNOSTICO");
            Console.WriteLine("========================================");
            Console.WriteLine(diagnosticoFinal);
            Console.WriteLine("========================================");

            if (diagnosticoFinal.Contains("CRITICO"))
            {
                Console.WriteLine("\nALERTA: Diagnostico critico detectado.");
                Console.WriteLine("Se recomienda solicitar consulta con doctor (opcion 5).");
            }
        }

        public void MostrarHistorial(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("       HISTORIAL MEDICO");
            Console.WriteLine("========================================");

            if (paciente.Historial.Count == 0)
            {
                Console.WriteLine("No hay registros medicos.");
                return;
            }

            foreach (RegistroMedico registro in paciente.Historial)
            {
                registro.MostrarRegistro();
            }
        }

        public void CompararClinicas()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("     COMPARACION DE CLINICAS");
            Console.WriteLine("========================================");
            Console.WriteLine("Datos simulados actuales:");
            Console.WriteLine("========================================");
            Console.WriteLine("Clinica | Precision(%) | Tiempo(min)");
            Console.WriteLine("--------|--------------|-------------");

            for (int i = 0; i < statsClinica.GetLength(0); i++)
            {
                Console.WriteLine(
                    "   " + (i + 1) + "    |      " +
                    statsClinica[i, 0] + "%      |     " +
                    statsClinica[i, 1] + " min"
                );
            }
            Console.WriteLine("========================================");
        }

        public void SolicitarConsultaDoctor(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("    SOLICITAR CONSULTA CON DOCTOR");
            Console.WriteLine("========================================");

            if (paciente.Historial.Count == 0)
            {
                Console.WriteLine("No tiene registros medicos.");
                Console.WriteLine("Primero ingrese sintomas (opcion 1).");
                return;
            }

            Console.WriteLine("Seleccione el registro para consulta:\n");

            for (int i = 0; i < paciente.Historial.Count; i++)
            {
                RegistroMedico reg = paciente.Historial[i];
                Console.WriteLine((i + 1) + ". " + reg.IdRegistro + " - " +
                                  reg.Fecha.ToString("dd/MM/yyyy") + " - " +
                                  reg.Diagnostico);
            }

            Console.Write("\nNumero de registro: ");
            int seleccion;

            if (!int.TryParse(Console.ReadLine(), out seleccion) ||
                seleccion < 1 || seleccion > paciente.Historial.Count)
            {
                Console.WriteLine("\nSeleccion invalida.");
                return;
            }

            RegistroMedico registroSeleccionado = paciente.Historial[seleccion - 1];
            string claveConsulta = paciente.Id + "|" + registroSeleccionado.IdRegistro;
            colaPacientes.Enqueue(claveConsulta);

            Console.WriteLine("\nSolicitud enviada exitosamente!");
            Console.WriteLine("Posicion en cola: " + colaPacientes.Count);
            Console.WriteLine("Un doctor revisara su caso pronto.");

            MostrarSintomasRelacionados(registroSeleccionado);
        }

        public void MostrarSintomasRelacionados(RegistroMedico registro)
        {
            Console.WriteLine("\n--- Sintomas potencialmente relacionados ---");

            List<string> relacionados = new List<string>();

            foreach (string sintoma in registro.Sintomas)
            {
                if (grafoSintomas.ContainsKey(sintoma))
                {
                    List<string> adyacentes = grafoSintomas[sintoma];

                    foreach (string rel in adyacentes)
                    {
                        if (!relacionados.Contains(rel) && !registro.Sintomas.Contains(rel))
                        {
                            relacionados.Add(rel);
                        }
                    }
                }
            }

            if (relacionados.Count > 0)
            {
                Console.WriteLine("Sintomas asociados: " + string.Join(", ", relacionados));
            }
            else
            {
                Console.WriteLine("No se encontraron sintomas adicionales relacionados.");
            }
        }

        public void ActualizarDatosPaciente(Paciente paciente)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("    ACTUALIZAR DATOS PERSONALES");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Actualizar nombre");
            Console.WriteLine("2. Actualizar email");
            Console.WriteLine("3. Actualizar contacto de emergencia");
            Console.WriteLine("0. Volver");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Nuevo nombre: ");
                    string nombre = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(nombre))
                    {
                        paciente.Nombre = nombre;
                        Console.WriteLine("\nNombre actualizado.");
                    }
                    else
                    {
                        Console.WriteLine("\nError: El nombre no puede estar vacio.");
                    }
                    break;
                case "2":
                    Console.Write("Nuevo email: ");
                    string email = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
                    {
                        paciente.Email = email;
                        Console.WriteLine("\nEmail actualizado.");
                    }
                    else
                    {
                        Console.WriteLine("\nError: Email invalido.");
                    }
                    break;
                case "3":
                    Console.Write("Nuevo contacto de emergencia: ");
                    string contacto = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(contacto))
                    {
                        paciente.ContactoEmergencia = contacto;
                        Console.WriteLine("\nContacto actualizado.");
                    }
                    else
                    {
                        Console.WriteLine("\nError: El contacto no puede estar vacio.");
                    }
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("\nOpcion no valida.");
                    break;
            }
        }
        public void RevisarColaPacientes()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("      COLA DE PACIENTES");
            Console.WriteLine("========================================");

            if (colaPacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes en cola.");
                return;
            }

            Console.WriteLine("Pacientes en espera: " + colaPacientes.Count);
            Console.WriteLine("========================================");

            string[] colaArray = colaPacientes.ToArray();

            for (int i = 0; i < colaArray.Length; i++)
            {
                string[] partes = colaArray[i].Split('|');
                string idPaciente = partes[0];
                string idRegistro = partes[1];

                Console.WriteLine((i + 1) + ". Paciente: " + idPaciente +
                                  " | Registro: " + idRegistro);
            }
            Console.WriteLine("========================================");
        }

        public void ValidarDiagnosticos(Doctor doctor)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("     VALIDAR DIAGNOSTICOS");
            Console.WriteLine("========================================");

            if (colaPacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes en cola.");
                return;
            }

            string claveConsulta = colaPacientes.Dequeue();
            string[] partes = claveConsulta.Split('|');
            string idPaciente = partes[0];
            string idRegistro = partes[1];

            Paciente paciente = null;

            foreach (Paciente p in pacientes)
            {
                if (p.Id == idPaciente)
                {
                    paciente = p;
                    break;
                }
            }

            if (paciente == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                return;
            }

            RegistroMedico registro = null;

            foreach (RegistroMedico r in paciente.Historial)
            {
                if (r.IdRegistro == idRegistro)
                {
                    registro = r;
                    break;
                }
            }

            if (registro == null)
            {
                Console.WriteLine("Registro no encontrado.");
                return;
            }

            Console.WriteLine("\nPaciente: " + paciente.Nombre + " (" + paciente.Id + ")");
            Console.WriteLine("Edad: " + paciente.Edad);
            registro.MostrarRegistro();

            Console.WriteLine("\n1. Confirmar diagnostico");
            Console.WriteLine("2. Modificar diagnostico");
            Console.WriteLine("3. Agregar observaciones");
            Console.WriteLine("0. Volver a la cola");
            Console.Write("\nSeleccione una opcion: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    registro.Confirmado = true;

                    if (!doctor.PacientesAsignados.Contains(paciente.Id))
                    {
                        doctor.PacientesAsignados.Add(paciente.Id);
                    }

                    Console.WriteLine("\nDiagnostico confirmado.");
                    break;

                case "2":
                    Console.Write("Nuevo diagnostico: ");
                    string nuevoDiagnostico = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(nuevoDiagnostico))
                    {
                        registro.Diagnostico = nuevoDiagnostico;
                        registro.Confirmado = true;

                        if (!doctor.PacientesAsignados.Contains(paciente.Id))
                        {
                            doctor.PacientesAsignados.Add(paciente.Id);
                        }

                        Console.WriteLine("\nDiagnostico actualizado y confirmado.");
                    }
                    else
                    {
                        Console.WriteLine("\nError: El diagnostico no puede estar vacio.");
                    }
                    break;

                case "3":
                    Console.Write("Observaciones: ");
                    string observaciones = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(observaciones))
                    {
                        registro.ObservacionDoctor = observaciones;
                        Console.WriteLine("\nObservaciones agregadas.");
                    }
                    else
                    {
                        Console.WriteLine("\nError: Las observaciones no pueden estar vacias.");
                    }
                    break;

                case "0":
                    colaPacientes.Enqueue(claveConsulta);
                    Console.WriteLine("\nPaciente devuelto a la cola.");
                    break;

                default:
                    Console.WriteLine("\nOpcion no valida.");
                    break;
            }
        }

        public void GestionarRegistrosMedicos()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("   GESTIONAR REGISTROS MEDICOS");
            Console.WriteLine("========================================");
            Console.Write("ID del paciente: ");
            string idPaciente = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(idPaciente))
            {
                Console.WriteLine("\nError: Debe ingresar un ID.");
                return;
            }

            Paciente paciente = null;

            foreach (Paciente p in pacientes)
            {
                if (p.Id == idPaciente)
                {
                    paciente = p;
                    break;
                }
            }

            if (paciente == null)
            {
                Console.WriteLine("\nPaciente no encontrado.");
                return;
            }

            paciente.MostrarInformacion();

            if (paciente.Historial.Count == 0)
            {
                Console.WriteLine("\nEste paciente no tiene registros medicos.");
                return;
            }

            Console.WriteLine("\n--- Historial completo ---");

            for (int i = 0; i < paciente.Historial.Count; i++)
            {
                Console.WriteLine("\n" + (i + 1) + ".");
                paciente.Historial[i].MostrarRegistro();
            }
        }

        public void VerEstadisticas()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("         ESTADISTICAS DEL SISTEMA");
            Console.WriteLine("========================================");

            int totalPacientes = pacientes.Count;
            int totalDoctores = doctores.Count;
            int totalRegistros = 0;
            int registrosConfirmados = 0;

            foreach (Paciente p in pacientes)
            {
                totalRegistros += p.Historial.Count;

                foreach (RegistroMedico r in p.Historial)
                {
                    if (r.Confirmado)
                    {
                        registrosConfirmados++;
                    }
                }
            }

            Console.WriteLine("Total de pacientes: " + totalPacientes);
            Console.WriteLine("Total de doctores: " + totalDoctores);
            Console.WriteLine("Total de registros medicos: " + totalRegistros);
            Console.WriteLine("Registros confirmados: " + registrosConfirmados);
            Console.WriteLine("Pacientes en cola: " + colaPacientes.Count);
            Console.WriteLine("========================================");
        }

        public void FuncionBeta(string nombreFuncion)

        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("Version beta: esta funcionalidad se implementara en versiones posteriores.");
            Console.WriteLine("==========================================");
        }
    }

}
