using System;
using System.Threading;
using System.Collections.Generic;

namespace MEDICENTER
{
    public class Menu
    {
        private Sistema sistema;

        public Menu()
        {
            sistema = new Sistema();
        }

        public void MostrarMenuPrincipal()
        {
            bool salir = false;

            do
            {
                Console.Clear();
                Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║                                                    ║");
                Console.WriteLine("║                 MEDICENTER v2.0                    ║");
                Console.WriteLine("║           Sistema de Gestion Medica Integral       ║");
                Console.WriteLine("║                                                    ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.WriteLine("\n  ┌──────────────────────────────────────────────┐");
                Console.WriteLine("  │  Seleccione el tipo de usuario:             │");
                Console.WriteLine("  ├──────────────────────────────────────────────┤");
                Console.WriteLine("  │  1. Paciente                                │");
                Console.WriteLine("  │  2. Personal Hospitalario                   │");
                Console.WriteLine("  │  0. Salir                                   │");
                Console.WriteLine("  └──────────────────────────────────────────────┘");
                Console.Write("\n  Opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        MenuPacienteInicial();
                        break;
                    case "2":
                        MenuPersonalInicial();
                        break;
                    case "0":
                        Console.WriteLine("\n  Guardando datos...");
                        sistema.GuardarTodosDatos();
                        Console.WriteLine("  Datos guardados exitosamente");
                        Console.WriteLine("\n  ╔════════════════════════════════════════════╗");
                        Console.WriteLine("  ║  Gracias por usar MEDICENTER              ║");
                        Console.WriteLine("  ║  Cuidese y hasta pronto                   ║");
                        Console.WriteLine("  ╚════════════════════════════════════════════╝");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\n  Opcion no valida");
                        Console.ReadKey();
                        break;
                }

            } while (!salir);
        }

        private void MenuPacienteInicial()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║              AREA DE PACIENTES                     ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.WriteLine("\n  1. Iniciar Sesion");
                Console.WriteLine("  2. Registrarse");
                Console.WriteLine("  0. Volver");
                Console.Write("\n  Opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        IniciarSesionPaciente();
                        break;
                    case "2":
                        RegistrarPaciente();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\n  Opcion no valida");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void IniciarSesionPaciente()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║           INICIO DE SESION - PACIENTE              ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");

                string id;
                while (true)
                {
                    Console.Write("\n  ID de usuario: ");
                    id = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(id))
                        break;
                    Console.WriteLine("  El ID no puede estar vacio");
                }

                Console.Write("  Contraseña: ");
                string password = LeerPassword();

                Paciente paciente = sistema.BuscarPaciente(id, password);

                if (paciente != null)
                {
                    Console.WriteLine("\n  Acceso concedido");
                    Console.WriteLine($"  Bienvenido, {paciente.Nombre}");
                    Console.ReadKey();
                    MenuPaciente(paciente);
                    break;
                }
                else
                {
                    Console.WriteLine("\n  Credenciales incorrectas");
                    Console.Write("  Desea intentar de nuevo? (s/n): ");
                    string r = Console.ReadLine().ToLower().Trim();
                    if (r != "s")
                        break;
                }
            }
        }

        private void RegistrarPaciente()
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║           REGISTRO DE NUEVO PACIENTE               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            Paciente nuevoPaciente = new Paciente();
            nuevoPaciente.Id = sistema.GenerarIdPaciente();

            while (true)
            {
                Console.Write("\n  Nombre completo: ");
                string nombre = Console.ReadLine();
                if (EsNombreValido(nombre))
                {
                    nuevoPaciente.Nombre = nombre.Trim();
                    break;
                }
                Console.WriteLine("  El nombre solo puede contener letras y espacios");
            }

            while (true)
            {
                Console.Write("  Email: ");
                string email = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
                {
                    nuevoPaciente.Email = email.Trim();
                    break;
                }
                Console.WriteLine("  Email invalido");
            }

            while (true)
            {
                Console.Write("  Contraseña: ");
                string password = LeerPassword();
                if (!string.IsNullOrWhiteSpace(password) && password.Length >= 6)
                {
                    nuevoPaciente.Password = password;
                    break;
                }
                Console.WriteLine("\n  La contraseña debe tener al menos 6 caracteres");
            }

            while (true)
            {
                Console.Write("\n  Edad: ");
                string edadTexto = Console.ReadLine();
                if (int.TryParse(edadTexto, out int edad) && edad > 0 && edad <= 120)
                {
                    nuevoPaciente.Edad = edad;
                    break;
                }
                Console.WriteLine("  Edad invalida");
            }

            while (true)
            {
                Console.Write("  Telefono: ");
                string tel = Console.ReadLine();
                if (EsTelefonoValido(tel))
                {
                    nuevoPaciente.Telefono = tel.Trim();
                    break;
                }
                Console.WriteLine("  El telefono solo puede contener numeros y debe tener al menos 8 digitos");
            }

            while (true)
            {
                Console.WriteLine("\n  Genero:");
                Console.WriteLine("     1. Masculino");
                Console.WriteLine("     2. Femenino");
                Console.Write("  Opcion: ");
                string opGenero = Console.ReadLine();
                if (opGenero == "1")
                {
                    nuevoPaciente.Genero = Genero.Masculino;
                    break;
                }
                if (opGenero == "2")
                {
                    nuevoPaciente.Genero = Genero.Femenino;
                    break;
                }
                Console.WriteLine("  Opcion no valida");
            }

            while (true)
            {
                Console.WriteLine("\n  Tipo de Sangre:");
                Console.WriteLine("     1. A+    2. A-    3. B+    4. B-");
                Console.WriteLine("     5. O+    6. O-    7. AB+   8. AB-");
                Console.Write("  Opcion: ");
                string opSangre = Console.ReadLine();
                bool ok = true;
                switch (opSangre)
                {
                    case "1": nuevoPaciente.TipoSangre = TipoSangre.A_Positivo; break;
                    case "2": nuevoPaciente.TipoSangre = TipoSangre.A_Negativo; break;
                    case "3": nuevoPaciente.TipoSangre = TipoSangre.B_Positivo; break;
                    case "4": nuevoPaciente.TipoSangre = TipoSangre.B_Negativo; break;
                    case "5": nuevoPaciente.TipoSangre = TipoSangre.O_Positivo; break;
                    case "6": nuevoPaciente.TipoSangre = TipoSangre.O_Negativo; break;
                    case "7": nuevoPaciente.TipoSangre = TipoSangre.AB_Positivo; break;
                    case "8": nuevoPaciente.TipoSangre = TipoSangre.AB_Negativo; break;
                    default:
                        Console.WriteLine("  Opcion no valida");
                        ok = false;
                        break;
                }
                if (ok) break;
            }

            while (true)
            {
                Console.WriteLine("\n  Seguro Medico:");
                Console.WriteLine("     1. Sin seguro (Acceso solo a hospitales publicos)");
                Console.WriteLine("     2. Seguro basico (Acceso a hospitales publicos)");
                Console.WriteLine("     3. Seguro completo (Acceso a todos los hospitales)");
                Console.Write("  Opcion: ");
                string opSeguro = Console.ReadLine();
                bool ok = true;
                switch (opSeguro)
                {
                    case "1": nuevoPaciente.TipoSeguro = TipoSeguro.SinSeguro; break;
                    case "2": nuevoPaciente.TipoSeguro = TipoSeguro.SeguroBasico; break;
                    case "3": nuevoPaciente.TipoSeguro = TipoSeguro.SeguroCompleto; break;
                    default:
                        Console.WriteLine("  Opcion no valida");
                        ok = false;
                        break;
                }
                if (ok) break;
            }

            if (nuevoPaciente.TipoSeguro != TipoSeguro.SinSeguro)
            {
                while (true)
                {
                    Console.Write("  Numero de seguro medico: ");
                    string numSeg = Console.ReadLine();
                    if (EsTelefonoValido(numSeg))
                    {
                        nuevoPaciente.NumeroSeguro = numSeg.Trim();
                        break;
                    }
                    Console.WriteLine("  El numero de seguro solo puede contener numeros y debe tener al menos 8 digitos");
                }
            }

            while (true)
            {
                Console.Write("\n  Contacto de emergencia (nombre y telefono): ");
                string contacto = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(contacto))
                {
                    nuevoPaciente.ContactoEmergencia = contacto.Trim();
                    break;
                }
                Console.WriteLine("  El contacto de emergencia no puede estar vacio");
            }

            sistema.Pacientes.Add(nuevoPaciente);
            sistema.GuardarTodosDatos();

            Console.WriteLine("\n  Registro exitoso");
            Console.WriteLine($"  Su ID es: {nuevoPaciente.Id}");
            Console.WriteLine("  Guarde su ID y contraseña para iniciar sesion");
            Console.ReadKey();
        }

        private void MenuPaciente(Paciente paciente)
        {
            bool salir = false;

            do
            {
                Console.Clear();
                Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
                Console.WriteLine($"║  Paciente: {paciente.Nombre,-37} ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.WriteLine("\n  1. Seleccionar Hospital y Solicitar Consulta");
                Console.WriteLine("  2. Ver mi Historial Medico");
                Console.WriteLine("  3. Comparar Hospitales");
                Console.WriteLine("  4. Ver mi Informacion Personal");
                Console.WriteLine("  5. Actualizar mis Datos");
                Console.WriteLine("  0. Cerrar Sesion");
                Console.Write("\n  Opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        SeleccionarHospitalYConsultar(paciente);
                        break;
                    case "2":
                        MostrarHistorialPaciente(paciente);
                        break;
                    case "3":
                        CompararHospitales(paciente);
                        break;
                    case "4":
                        paciente.MostrarInformacion();
                        Console.ReadKey();
                        break;
                    case "5":
                        ActualizarDatosPaciente(paciente);
                        break;
                    case "0":
                        sistema.GuardarTodosDatos();
                        Console.WriteLine("\n  Sesion guardada. Hasta pronto");
                        Console.ReadKey();
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\n  Opcion no valida");
                        Console.ReadKey();
                        break;
                }

            } while (!salir);
        }

        private void SeleccionarHospitalYConsultar(Paciente paciente)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║          SELECCIONAR HOSPITAL                      ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");

                List<Hospital> disponibles = sistema.ObtenerHospitalesDisponibles(paciente.TipoSeguro);
                List<Hospital> privados = sistema.ObtenerHospitalesPrivados();

                if (disponibles.Count > 0)
                {
                    Console.WriteLine("\n  Hospitales Disponibles (segun su seguro):");
                    Console.WriteLine("  ════════════════════════════════════════════════");
                    for (int i = 0; i < disponibles.Count; i++)
                    {
                        Console.WriteLine($"\n  {i + 1}.");
                        disponibles[i].MostrarInformacion();
                    }
                }

                if (paciente.TipoSeguro != TipoSeguro.SeguroCompleto)
                {
                    Console.WriteLine("\n  ─────────────────────────────────────────────────");
                    Console.WriteLine("  Otros hospitales (requieren pago de consulta):");
                    Console.WriteLine("  ─────────────────────────────────────────────────");

                    int offset = disponibles.Count;
                    for (int i = 0; i < privados.Count; i++)
                    {
                        Console.WriteLine($"\n  {offset + i + 1}.");
                        privados[i].MostrarInformacion();
                    }
                }

                Console.Write("\n  Seleccione el hospital (0 para cancelar): ");
                string selTexto = Console.ReadLine();
                if (!int.TryParse(selTexto, out int seleccion))
                {
                    Console.WriteLine("\n  Opcion no valida");
                    Console.ReadKey();
                    continue;
                }

                if (seleccion == 0)
                    return;

                Hospital hospitalSeleccionado = null;
                bool requierePago = false;

                if (seleccion > 0 && seleccion <= disponibles.Count)
                {
                    hospitalSeleccionado = disponibles[seleccion - 1];
                }
                else if (seleccion > disponibles.Count && seleccion <= disponibles.Count + privados.Count)
                {
                    hospitalSeleccionado = privados[seleccion - disponibles.Count - 1];
                    requierePago = true;
                }

                if (hospitalSeleccionado == null)
                {
                    Console.WriteLine("\n  Opcion no valida");
                    Console.ReadKey();
                    continue;
                }

                if (requierePago)
                {
                    Console.WriteLine($"\n  Este hospital requiere pago de consulta: ${hospitalSeleccionado.CostoConsulta:F2}");
                    Console.Write("  Desea continuar? (s/n): ");
                    string r = Console.ReadLine().ToLower().Trim();
                    if (r != "s")
                        return;
                }

                IngresarSintomasYDiagnostico(paciente, hospitalSeleccionado);
                break;
            }
        }

        private void IngresarSintomasYDiagnostico(Paciente paciente, Hospital hospital)
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║            INGRESAR SINTOMAS                       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.WriteLine($"\n  Hospital: {hospital.Nombre}");

            RegistroMedico nuevoRegistro = new RegistroMedico();
            nuevoRegistro.IdRegistro = sistema.GenerarIdRegistro();
            nuevoRegistro.IdPaciente = paciente.Id;
            nuevoRegistro.IdHospital = hospital.Id;

            string[] sintomasFrecuentes =
            {
                "Fiebre", "Tos", "Dolor de cabeza", "Dolor de garganta",
                "Fatiga", "Nauseas", "Dolor abdominal", "Dificultad para respirar",
                "Mareos", "Dolor muscular"
            };

            while (true)
            {
                Console.WriteLine("\n  Sintomas frecuentes:");
                for (int i = 0; i < sintomasFrecuentes.Length; i++)
                {
                    Console.WriteLine($"  {i + 1}. {sintomasFrecuentes[i]}");
                }

                Console.WriteLine("\n  Ingrese los numeros de sintomas separados por coma (ej: 1,3,5)");
                Console.Write("  Sintomas: ");
                string entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("\n  Debe ingresar al menos un sintoma");
                    continue;
                }

                nuevoRegistro.Sintomas.Clear();
                string[] numeros = entrada.Split(',');
                foreach (string num in numeros)
                {
                    string valor = num.Trim();
                    if (valor.Length == 0)
                        continue;

                    if (int.TryParse(valor, out int indice) &&
                        indice >= 1 && indice <= sintomasFrecuentes.Length)
                    {
                        nuevoRegistro.Sintomas.Add(sintomasFrecuentes[indice - 1]);
                    }
                    else
                    {
                        Console.WriteLine($"  Advertencia: el valor {valor} no es valido");
                    }
                }

                if (nuevoRegistro.Sintomas.Count == 0)
                {
                    Console.WriteLine("\n  No se ingresaron sintomas validos");
                    continue;
                }

                break;
            }

            Console.WriteLine("\n  Analizando sintomas...");
            Thread.Sleep(1000);

            string diagnostico = RealizarDiagnosticoInteractivo();
            nuevoRegistro.Diagnostico = diagnostico;
            nuevoRegistro.Tratamiento = "Pendiente de revision medica";

            if (!sistema.RegistrosPorHospital.ContainsKey(hospital.Id))
            {
                sistema.RegistrosPorHospital[hospital.Id] = new List<RegistroMedico>();
            }
            sistema.RegistrosPorHospital[hospital.Id].Add(nuevoRegistro);

            paciente.Historial.Add(nuevoRegistro);

            if (!sistema.ColasPorHospital.ContainsKey(hospital.Id))
            {
                sistema.ColasPorHospital[hospital.Id] = new Queue<string>();
            }
            string clave = paciente.Id + "|" + nuevoRegistro.IdRegistro;
            sistema.ColasPorHospital[hospital.Id].Enqueue(clave);

            sistema.GuardarTodosDatos();

            Console.WriteLine("\n  ════════════════════════════════════════════════════");
            Console.WriteLine("  Consulta registrada exitosamente");
            Console.WriteLine($"  ID de Registro: {nuevoRegistro.IdRegistro}");
            Console.WriteLine($"  Hospital: {hospital.Nombre}");
            Console.WriteLine($"  Diagnostico Preliminar: {diagnostico}");
            Console.WriteLine("  Un medico revisara su caso pronto");
            Console.WriteLine($"  Posicion en cola: {sistema.ColasPorHospital[hospital.Id].Count}");
            Console.WriteLine("  ════════════════════════════════════════════════════");
            Console.ReadKey();
        }

        private string RealizarDiagnosticoInteractivo()
        {
            Console.WriteLine("\n  Responda las siguientes preguntas con 'si' o 'no':\n");

            DecisionNode nodoActual = sistema.ArbolDiagnostico;

            while (!nodoActual.EsHoja())
            {
                Console.Write($"  {nodoActual.Pregunta} (si/no): ");
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
                    Console.WriteLine("  Respuesta invalida. Use 'si' o 'no'");
                }
            }

            return nodoActual.Diagnostico;
        }

        private void MostrarHistorialPaciente(Paciente paciente)
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║            HISTORIAL MEDICO                        ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            if (paciente.Historial.Count == 0)
            {
                Console.WriteLine("\n  No tiene registros medicos");
                Console.ReadKey();
                return;
            }

            foreach (var registro in paciente.Historial)
            {
                registro.MostrarRegistro();
            }

            Console.WriteLine("\n  Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void CompararHospitales(Paciente paciente)
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║          COMPARACION DE HOSPITALES                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            Console.WriteLine();
            Console.WriteLine("╔═════╦════════════════════════════════════╦══════════╦════════╦════════╗");
            Console.WriteLine("║ ID  ║ Hospital                         ║ Tipo     ║ Precis.║ Tiempo ║");
            Console.WriteLine("╠═════╬════════════════════════════════════╬══════════╬════════╬════════╣");

            foreach (var hospital in sistema.Hospitales)
            {
                string tipo = hospital.EsPublico ? "Publico" : "Privado";
                Console.WriteLine(
                    $"║ {hospital.Id,-3} ║ {Recortar(hospital.Nombre, 34),-34} ║ {tipo,-8} ║ {hospital.PrecisionDiagnostico,4}% ║ {hospital.TiempoPromedioMin,3} min ║");
            }

            Console.WriteLine("╚═════╩════════════════════════════════════╩══════════╩════════╩════════╝");
            Console.ReadKey();
        }

        private void ActualizarDatosPaciente(Paciente paciente)
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║          ACTUALIZAR DATOS PERSONALES               ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.WriteLine("\n  1. Actualizar telefono");
                Console.WriteLine("  2. Actualizar email");
                Console.WriteLine("  3. Actualizar contacto de emergencia");
                Console.WriteLine("  4. Cambiar contraseña");
                Console.WriteLine("  0. Volver");
                Console.Write("\n  Opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        while (true)
                        {
                            Console.Write("\n  Nuevo telefono: ");
                            string nuevoTel = Console.ReadLine();
                            if (EsTelefonoValido(nuevoTel))
                            {
                                paciente.Telefono = nuevoTel.Trim();
                                Console.WriteLine("  Telefono actualizado");
                                sistema.GuardarTodosDatos();
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("  El telefono solo puede contener numeros y debe tener al menos 8 digitos");
                        }
                        break;
                    case "2":
                        while (true)
                        {
                            Console.Write("\n  Nuevo email: ");
                            string nuevoEmail = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(nuevoEmail) && nuevoEmail.Contains("@"))
                            {
                                paciente.Email = nuevoEmail.Trim();
                                Console.WriteLine("  Email actualizado");
                                sistema.GuardarTodosDatos();
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("  Email invalido");
                        }
                        break;
                    case "3":
                        while (true)
                        {
                            Console.Write("\n  Nuevo contacto de emergencia: ");
                            string nuevoContacto = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(nuevoContacto))
                            {
                                paciente.ContactoEmergencia = nuevoContacto.Trim();
                                Console.WriteLine("  Contacto actualizado");
                                sistema.GuardarTodosDatos();
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("  El contacto de emergencia no puede estar vacio");
                        }
                        break;
                    case "4":
                        while (true)
                        {
                            Console.Write("\n  Nueva contraseña: ");
                            string nuevaPass = LeerPassword();
                            if (!string.IsNullOrWhiteSpace(nuevaPass) && nuevaPass.Length >= 6)
                            {
                                paciente.Password = nuevaPass;
                                Console.WriteLine("\n  Contraseña actualizada");
                                sistema.GuardarTodosDatos();
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("\n  La contraseña debe tener al menos 6 caracteres");
                        }
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\n  Opcion no valida");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void MenuPersonalInicial()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║          AREA DE PERSONAL HOSPITALARIO             ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.WriteLine("\n  1. Iniciar Sesion");
                Console.WriteLine("  2. Registro de Personal (Solo Administradores)");
                Console.WriteLine("  0. Volver");
                Console.Write("\n  Opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        IniciarSesionPersonal();
                        break;
                    case "2":
                        RegistrarPersonalPorAdmin();
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\n  Opcion no valida");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void IniciarSesionPersonal()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║       INICIO DE SESION - PERSONAL HOSPITALARIO     ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");

                string id;
                while (true)
                {
                    Console.Write("\n  ID de usuario: ");
                    id = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(id))
                        break;
                    Console.WriteLine("  El ID no puede estar vacio");
                }

                Console.Write("  Contraseña: ");
                string password = LeerPassword();

                PersonalHospitalario personal = sistema.BuscarPersonal(id, password);

                if (personal != null)
                {
                    Console.WriteLine("\n  Acceso concedido");
                    Console.WriteLine($"  Bienvenido, {personal.Nombre}");
                    Console.ReadKey();

                    if (!personal.CambioPassword && password == "medicenter2025")
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
                            Console.WriteLine("║          CAMBIO DE CONTRASEÑA REQUERIDO            ║");
                            Console.WriteLine("╚════════════════════════════════════════════════════╝");
                            Console.WriteLine("\n  Por seguridad, debe cambiar su contraseña");
                            Console.Write("\n  Nueva contraseña: ");
                            string nuevaPass = LeerPassword();
                            Console.Write("  Confirmar contraseña: ");
                            string confirmar = LeerPassword();

                            if (!string.IsNullOrWhiteSpace(nuevaPass) &&
                                nuevaPass.Length >= 6 &&
                                nuevaPass == confirmar)
                            {
                                personal.Password = nuevaPass;
                                personal.CambioPassword = true;
                                sistema.GuardarTodosDatos();
                                Console.WriteLine("\n  Contraseña actualizada exitosamente");
                                Console.ReadKey();
                                break;
                            }

                            Console.WriteLine("\n  Error al cambiar contraseña");
                            Console.Write("  Desea intentar de nuevo? (s/n): ");
                            string r = Console.ReadLine().ToLower().Trim();
                            if (r != "s")
                                break;
                        }
                    }

                    if (personal.NivelAcceso == NivelAcceso.Administrador)
                    {
                        MenuAdministrador(personal);
                    }
                    else
                    {
                        MenuMedicoGeneral(personal);
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("\n  Credenciales incorrectas");
                    Console.Write("  Desea intentar de nuevo? (s/n): ");
                    string r = Console.ReadLine().ToLower().Trim();
                    if (r != "s")
                        break;
                }
            }
        }

        private void RegistrarPersonalPorAdmin()
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║      REGISTRO DE PERSONAL (ADMINISTRADOR)          ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            Console.Write("\n  ID de Administrador: ");
            string idAdmin = Console.ReadLine();

            Console.Write("  Contraseña: ");
            string passAdmin = LeerPassword();

            PersonalHospitalario admin = sistema.BuscarPersonal(idAdmin, passAdmin);

            if (admin == null || admin.NivelAcceso != NivelAcceso.Administrador)
            {
                Console.WriteLine("\n  Acceso denegado. Solo administradores pueden registrar personal");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n  Acceso administrativo verificado");
            Console.WriteLine("\n  Registro de Nuevo Personal:");

            PersonalHospitalario nuevoPersonal = new PersonalHospitalario();
            nuevoPersonal.Id = sistema.GenerarIdPersonal();
            nuevoPersonal.Password = "medicenter2025";

            while (true)
            {
                Console.Write("\n  Nombre completo: ");
                string nombre = Console.ReadLine();
                if (EsNombreValido(nombre))
                {
                    nuevoPersonal.Nombre = nombre.Trim();
                    break;
                }
                Console.WriteLine("  El nombre solo puede contener letras y espacios");
            }

            while (true)
            {
                Console.Write("  Email: ");
                string email = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
                {
                    nuevoPersonal.Email = email.Trim();
                    break;
                }
                Console.WriteLine("  Email invalido");
            }

            while (true)
            {
                Console.WriteLine("\n  Hospitales disponibles:");
                for (int i = 0; i < sistema.Hospitales.Count; i++)
                {
                    Console.WriteLine($"  {i + 1}. {sistema.Hospitales[i].Nombre} ({sistema.Hospitales[i].Id})");
                }
                Console.Write("\n  Seleccione hospital: ");
                string selTexto = Console.ReadLine();
                if (int.TryParse(selTexto, out int selHosp) &&
                    selHosp > 0 && selHosp <= sistema.Hospitales.Count)
                {
                    nuevoPersonal.IdHospital = sistema.Hospitales[selHosp - 1].Id;
                    break;
                }
                Console.WriteLine("  Hospital invalido");
            }

            while (true)
            {
                Console.WriteLine("\n  Nivel de Acceso:");
                Console.WriteLine("  1. Medico General");
                Console.WriteLine("  2. Administrador");
                Console.Write("\n  Opcion: ");
                string opNivel = Console.ReadLine();
                if (opNivel == "1")
                {
                    nuevoPersonal.NivelAcceso = NivelAcceso.MedicoGeneral;
                    Console.Write("\n  Especialidad: ");
                    nuevoPersonal.Especialidad = Console.ReadLine();
                    break;
                }
                if (opNivel == "2")
                {
                    nuevoPersonal.NivelAcceso = NivelAcceso.Administrador;
                    break;
                }
                Console.WriteLine("  Opcion no valida");
            }

            sistema.Personal.Add(nuevoPersonal);

            Hospital hospital = sistema.BuscarHospital(nuevoPersonal.IdHospital);
            if (hospital != null)
            {
                hospital.PersonalIds.Add(nuevoPersonal.Id);
            }

            sistema.GuardarTodosDatos();

            Console.WriteLine("\n  ════════════════════════════════════════════════════");
            Console.WriteLine("  Personal registrado exitosamente");
            Console.WriteLine($"  ID: {nuevoPersonal.Id}");
            Console.WriteLine("  Contraseña por defecto: medicenter2025");
            Console.WriteLine("  El usuario debera cambiarla al iniciar sesion");
            Console.WriteLine("  ════════════════════════════════════════════════════");
            Console.ReadKey();
        }

        private void MenuAdministrador(PersonalHospitalario admin)
        {
            bool salir = false;

            do
            {
                Console.Clear();
                Hospital hospital = sistema.BuscarHospital(admin.IdHospital);
                Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
                Console.WriteLine($"║  ADMINISTRADOR - {admin.Nombre,-28} ║");
                Console.WriteLine($"║  {hospital?.Nombre,-48} ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.WriteLine("\n  1. Gestionar Personal del Hospital");
                Console.WriteLine("  2. Ver Estadisticas del Hospital");
                Console.WriteLine("  3. Ver Registros Medicos");
                Console.WriteLine("  4. Ver Cola de Pacientes");
                Console.WriteLine("  5. Ver mi Informacion");
                Console.WriteLine("  6. Cambiar Contraseña");
                Console.WriteLine("  0. Cerrar Sesion");
                Console.Write("\n  Opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        GestionarPersonalHospital(admin);
                        break;
                    case "2":
                        VerEstadisticasHospital(admin.IdHospital);
                        break;
                    case "3":
                        VerRegistrosMedicosHospital(admin.IdHospital);
                        break;
                    case "4":
                        VerColaPacientesHospital(admin.IdHospital);
                        break;
                    case "5":
                        admin.MostrarInformacion();
                        Console.ReadKey();
                        break;
                    case "6":
                        CambiarPasswordPersonal(admin);
                        break;
                    case "0":
                        sistema.GuardarTodosDatos();
                        Console.WriteLine("\n  Sesion guardada. Hasta pronto");
                        Console.ReadKey();
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\n  Opcion no valida");
                        Console.ReadKey();
                        break;
                }

            } while (!salir);
        }

        private void MenuMedicoGeneral(PersonalHospitalario medico)
        {
            bool salir = false;

            do
            {
                Console.Clear();
                Hospital hospital = sistema.BuscarHospital(medico.IdHospital);
                Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
                Console.WriteLine($"║  MEDICO - {medico.Nombre,-35} ║");
                Console.WriteLine($"║  {hospital?.Nombre,-48} ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.WriteLine("\n  1. Atender Paciente (Cola)");
                Console.WriteLine("  2. Validar Diagnosticos");
                Console.WriteLine("  3. Ver Registros del Hospital");
                Console.WriteLine("  4. Mis Pacientes Asignados");
                Console.WriteLine("  5. Ver mi Informacion");
                Console.WriteLine("  6. Cambiar Contraseña");
                Console.WriteLine("  0. Cerrar Sesion");
                Console.Write("\n  Opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AtenderPaciente(medico);
                        break;
                    case "2":
                        ValidarDiagnosticosMedico(medico);
                        break;
                    case "3":
                        VerRegistrosMedicosHospital(medico.IdHospital);
                        break;
                    case "4":
                        VerMisPacientes(medico);
                        break;
                    case "5":
                        medico.MostrarInformacion();
                        Console.ReadKey();
                        break;
                    case "6":
                        CambiarPasswordPersonal(medico);
                        break;
                    case "0":
                        sistema.GuardarTodosDatos();
                        Console.WriteLine("\n  Sesion guardada. Hasta pronto");
                        Console.ReadKey();
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\n  Opcion no valida");
                        Console.ReadKey();
                        break;
                }

            } while (!salir);
        }

        private void GestionarPersonalHospital(PersonalHospitalario admin)
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║        GESTION DE PERSONAL DEL HOSPITAL            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            Hospital hospital = sistema.BuscarHospital(admin.IdHospital);
            if (hospital == null)
            {
                Console.WriteLine("\n  Error al cargar hospital");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\n  Hospital: {hospital.Nombre}");
            Console.WriteLine("  ────────────────────────────────────────────────────");

            List<PersonalHospitalario> personalHospital = sistema.Personal.FindAll(p => p.IdHospital == admin.IdHospital);

            if (personalHospital.Count == 0)
            {
                Console.WriteLine("\n  No hay personal registrado en este hospital");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n  Personal del Hospital:");
            for (int i = 0; i < personalHospital.Count; i++)
            {
                PersonalHospitalario p = personalHospital[i];
                Console.WriteLine($"\n  {i + 1}. [{p.Id}] {p.Nombre}");
                Console.WriteLine($"     Nivel: {p.NivelAcceso}");
                if (!string.IsNullOrEmpty(p.Especialidad))
                    Console.WriteLine($"     Especialidad: {p.Especialidad}");
                Console.WriteLine($"     Pacientes asignados: {p.PacientesAsignados.Count}");
            }

            Console.WriteLine("\n  Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void VerEstadisticasHospital(string idHospital)
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║        ESTADISTICAS DEL HOSPITAL                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            Hospital hospital = sistema.BuscarHospital(idHospital);
            if (hospital == null) return;

            List<RegistroMedico> registros = sistema.RegistrosPorHospital.ContainsKey(idHospital)
                ? sistema.RegistrosPorHospital[idHospital]
                : new List<RegistroMedico>();

            Queue<string> cola = sistema.ColasPorHospital.ContainsKey(idHospital)
                ? sistema.ColasPorHospital[idHospital]
                : new Queue<string>();

            int confirmados = registros.FindAll(r => r.Confirmado).Count;
            int pendientes = registros.Count - confirmados;

            Console.WriteLine($"\n  Hospital: {hospital.Nombre}");
            Console.WriteLine("  ════════════════════════════════════════════════════");
            Console.WriteLine($"  Total de Registros: {registros.Count}");
            Console.WriteLine($"  Diagnosticos Confirmados: {confirmados}");
            Console.WriteLine($"  Diagnosticos Pendientes: {pendientes}");
            Console.WriteLine($"  Pacientes en Cola: {cola.Count}");
            Console.WriteLine($"  Personal del Hospital: {hospital.PersonalIds.Count}");
            Console.WriteLine($"  Precision de Diagnostico: {hospital.PrecisionDiagnostico}%");
            Console.WriteLine($"  Tiempo Promedio: {hospital.TiempoPromedioMin} min");
            Console.WriteLine("  ════════════════════════════════════════════════════");

            Console.ReadKey();
        }

        private void VerRegistrosMedicosHospital(string idHospital)
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║        REGISTROS MEDICOS DEL HOSPITAL              ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            if (!sistema.RegistrosPorHospital.ContainsKey(idHospital))
            {
                Console.WriteLine("\n  No hay registros en este hospital");
                Console.ReadKey();
                return;
            }

            List<RegistroMedico> registros = sistema.RegistrosPorHospital[idHospital];

            if (registros.Count == 0)
            {
                Console.WriteLine("\n  No hay registros en este hospital");
                Console.ReadKey();
                return;
            }

            foreach (var registro in registros)
            {
                registro.MostrarRegistro();
            }

            Console.WriteLine("\n  Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void VerColaPacientesHospital(string idHospital)
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║           COLA DE PACIENTES                        ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            if (!sistema.ColasPorHospital.ContainsKey(idHospital) ||
                sistema.ColasPorHospital[idHospital].Count == 0)
            {
                Console.WriteLine("\n  No hay pacientes en cola");
                Console.ReadKey();
                return;
            }

            Queue<string> cola = sistema.ColasPorHospital[idHospital];
            string[] colaArray = cola.ToArray();

            Console.WriteLine($"\n  Total de pacientes en espera: {colaArray.Length}");
            Console.WriteLine("  ────────────────────────────────────────────────────");

            for (int i = 0; i < colaArray.Length; i++)
            {
                string[] partes = colaArray[i].Split('|');
                string idPaciente = partes[0];
                string idRegistro = partes[1];

                Paciente paciente = sistema.Pacientes.Find(p => p.Id == idPaciente);

                Console.WriteLine($"\n  {i + 1}. Paciente: {paciente?.Nombre ?? idPaciente}");
                Console.WriteLine($"     ID: {idPaciente} | Registro: {idRegistro}");
            }

            Console.WriteLine("\n  Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void AtenderPaciente(PersonalHospitalario medico)
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║           ATENDER PACIENTE                         ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            if (!sistema.ColasPorHospital.ContainsKey(medico.IdHospital) ||
                sistema.ColasPorHospital[medico.IdHospital].Count == 0)
            {
                Console.WriteLine("\n  No hay pacientes en cola");
                Console.ReadKey();
                return;
            }

            string clave = sistema.ColasPorHospital[medico.IdHospital].Dequeue();
            string[] partesClave = clave.Split('|');
            string idPaciente = partesClave[0];
            string idRegistro = partesClave[1];

            Paciente paciente = sistema.Pacientes.Find(p => p.Id == idPaciente);
            RegistroMedico registro = sistema.RegistrosPorHospital[medico.IdHospital]
                .Find(r => r.IdRegistro == idRegistro);

            if (paciente == null || registro == null)
            {
                Console.WriteLine("\n  Error al cargar datos del paciente");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\n  Paciente: {paciente.Nombre}");
            Console.WriteLine($"  ID: {paciente.Id} | Edad: {paciente.Edad}");
            registro.MostrarRegistro();

            Console.WriteLine("\n  ──────────────────────────────────────────────────");
            Console.WriteLine("  1. Confirmar diagnostico");
            Console.WriteLine("  2. Modificar diagnostico");
            Console.WriteLine("  3. Agregar tratamiento");
            Console.WriteLine("  4. Agregar observaciones");
            Console.WriteLine("  5. Devolver a cola");
            Console.Write("\n  Opcion: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    registro.Confirmado = true;
                    registro.IdMedico = medico.Id;
                    if (!medico.PacientesAsignados.Contains(paciente.Id))
                        medico.PacientesAsignados.Add(paciente.Id);
                    Console.WriteLine("\n  Diagnostico confirmado");
                    sistema.GuardarTodosDatos();
                    Console.ReadKey();
                    break;

                case "2":
                    while (true)
                    {
                        Console.Write("\n  Nuevo diagnostico: ");
                        string nuevoDiag = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nuevoDiag))
                        {
                            registro.Diagnostico = nuevoDiag;
                            registro.Confirmado = true;
                            registro.IdMedico = medico.Id;
                            if (!medico.PacientesAsignados.Contains(paciente.Id))
                                medico.PacientesAsignados.Add(paciente.Id);
                            Console.WriteLine("  Diagnostico actualizado");
                            sistema.GuardarTodosDatos();
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("  El diagnostico no puede estar vacio");
                    }
                    break;

                case "3":
                    while (true)
                    {
                        Console.Write("\n  Tratamiento recomendado: ");
                        string trat = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(trat))
                        {
                            registro.Tratamiento = trat;
                            Console.WriteLine("  Tratamiento agregado");
                            sistema.GuardarTodosDatos();
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("  El tratamiento no puede estar vacio");
                    }
                    break;

                case "4":
                    while (true)
                    {
                        Console.Write("\n  Observaciones medicas: ");
                        string obs = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(obs))
                        {
                            registro.ObservacionDoctor = obs;
                            Console.WriteLine("  Observaciones agregadas");
                            sistema.GuardarTodosDatos();
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("  Las observaciones no pueden estar vacias");
                    }
                    break;

                case "5":
                    sistema.ColasPorHospital[medico.IdHospital].Enqueue(clave);
                    Console.WriteLine("\n  Paciente devuelto a la cola");
                    sistema.GuardarTodosDatos();
                    Console.ReadKey();
                    break;

                default:
                    Console.WriteLine("\n  Opcion no valida");
                    Console.ReadKey();
                    break;
            }
        }

        private void ValidarDiagnosticosMedico(PersonalHospitalario medico)
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║         VALIDAR DIAGNOSTICOS PENDIENTES            ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            if (!sistema.RegistrosPorHospital.ContainsKey(medico.IdHospital))
            {
                Console.WriteLine("\n  No hay registros");
                Console.ReadKey();
                return;
            }

            List<RegistroMedico> pendientes = sistema.RegistrosPorHospital[medico.IdHospital]
                .FindAll(r => !r.Confirmado);

            if (pendientes.Count == 0)
            {
                Console.WriteLine("\n  Todos los diagnosticos estan confirmados");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\n  Diagnosticos pendientes: {pendientes.Count}");

            foreach (var registro in pendientes)
            {
                registro.MostrarRegistro();
            }

            Console.WriteLine("\n  Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void VerMisPacientes(PersonalHospitalario medico)
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║         MIS PACIENTES ASIGNADOS                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            if (medico.PacientesAsignados.Count == 0)
            {
                Console.WriteLine("\n  No tiene pacientes asignados");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\n  Total de pacientes: {medico.PacientesAsignados.Count}");
            Console.WriteLine("  ────────────────────────────────────────────────────");

            foreach (string idPaciente in medico.PacientesAsignados)
            {
                Paciente paciente = sistema.Pacientes.Find(p => p.Id == idPaciente);
                if (paciente != null)
                {
                    Console.WriteLine($"\n  [{paciente.Id}] {paciente.Nombre}");
                    Console.WriteLine($"  Edad: {paciente.Edad} | Registros: {paciente.Historial.Count}");
                }
            }

            Console.WriteLine("\n  Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void CambiarPasswordPersonal(PersonalHospitalario personal)
        {
            Console.Clear();
            Console.WriteLine("\n╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║           CAMBIAR CONTRASEÑA                       ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");

            Console.Write("\n  Contraseña actual: ");
            string actual = LeerPassword();

            if (actual != personal.Password)
            {
                Console.WriteLine("\n  Contraseña incorrecta");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Write("  Nueva contraseña: ");
                string nueva = LeerPassword();

                Console.Write("  Confirmar contraseña: ");
                string confirmar = LeerPassword();

                if (!string.IsNullOrWhiteSpace(nueva) && nueva.Length >= 6 && nueva == confirmar)
                {
                    personal.Password = nueva;
                    personal.CambioPassword = true;
                    sistema.GuardarTodosDatos();

                    Console.WriteLine("\n  Contraseña actualizada exitosamente");
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine("\n  Error al cambiar contraseña");
                Console.Write("  Desea intentar de nuevo? (s/n): ");
                string r = Console.ReadLine().ToLower().Trim();
                if (r != "s")
                    break;
            }
        }

        private bool EsNombreValido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            bool tieneLetra = false;

            foreach (char c in valor)
            {
                if (char.IsLetter(c))
                {
                    tieneLetra = true;
                }
                else if (c != ' ')
                {
                    return false;
                }
            }

            return tieneLetra;
        }

        private bool EsTelefonoValido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            valor = valor.Trim();
            if (valor.Length < 8)
                return false;

            foreach (char c in valor)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        private string Recortar(string texto, int max)
        {
            if (string.IsNullOrEmpty(texto))
                return "";
            if (texto.Length <= max)
                return texto;
            return texto.Substring(0, max);
        }

        private string LeerPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }
}