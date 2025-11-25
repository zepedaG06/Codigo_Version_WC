using System;
using System.Numerics;

namespace MEDICENTER
{
    public class Menu
    {
        public Sistema sistema;

        public Menu()
        {
            sistema = new Sistema();
        }

        public void MostrarMenuPrincipal()
        {
            bool salir = false;

            do
            {
                Console.WriteLine("\n=========================================");
                Console.WriteLine("    BIENVENIDO A MEDICENTER v1.0");
                Console.WriteLine("    Sistema de Gestion Medica");
                Console.WriteLine("=========================================");
                Console.WriteLine("1. Iniciar sesion");
                Console.WriteLine("2. Registrar usuario (Paciente o Doctor)");
                Console.WriteLine("0. Salir");
                Console.WriteLine("=========================================");
                Console.Write("Seleccione una opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        IniciarSesion();
                        break;
                    case "2":
                        MenuRegistro();
                        break;
                    case "0":
                        Console.WriteLine("\nGracias por usar MEDICENTER!");
                        Console.WriteLine("Hasta pronto.");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\nOpcion no valida. Intente de nuevo.");
                        break;
                }

            } while (!salir);
        }

        public void MenuRegistro()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("        REGISTRO DE USUARIO");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar Paciente");
            Console.WriteLine("2. Registrar Doctor");
            Console.WriteLine("0. Volver");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opcion: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    sistema.RegistrarPaciente();
                    break;
                case "2":
                    sistema.RegistrarDoctor();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("\nOpcion no valida.");
                    break;
            }
        }

        public void IniciarSesion()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("          INICIAR SESION");
            Console.WriteLine("========================================");
            Console.WriteLine("Credenciales de prueba:");
            Console.WriteLine("  Paciente: P001 / pass123");
            Console.WriteLine("  Doctor: D001 / doc123");
            Console.WriteLine("========================================");

            Console.Write("ID de usuario: ");
            string id = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (id.StartsWith("P"))
            {
                Paciente paciente = sistema.BuscarPaciente(id, password);

                if (paciente != null)
                {
                    Console.WriteLine("\nBienvenido, " + paciente.Nombre);
                    MenuPaciente(paciente);
                }
                else
                {
                    Console.WriteLine("\nCredenciales incorrectas.");
                }
            }
            else if (id.StartsWith("D"))
            {
                Doctor doctor = sistema.BuscarDoctor(id, password);

                if (doctor != null)
                {
                    Console.WriteLine("\nBienvenido, Dr. " + doctor.Nombre);
                    MenuDoctor(doctor);
                }
                else
                {
                    Console.WriteLine("\nCredenciales incorrectas.");
                }
            }
            else
            {
                Console.WriteLine("\nID no valido.");
            }
        }

        public void MenuPaciente(Paciente paciente)
        {
            bool salir = false;

            do
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("          MENU PACIENTE");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Ingresar sintomas");
                Console.WriteLine("2. Obtener diagnostico automatico");
                Console.WriteLine("3. Consultar historial medico");
                Console.WriteLine("4. Comparar clinicas");
                Console.WriteLine("5. Solicitar consulta con doctor");
                Console.WriteLine("6. Actualizar datos personales");
                Console.WriteLine("7. Ver informacion personal");
                Console.WriteLine("0. Cerrar sesion");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        sistema.IngresarSintomas(paciente);
                        break;
                    case "2":
                        sistema.ObtenerDiagnosticoInteractivo(paciente);
                        break;
                    case "3":
                        sistema.MostrarHistorial(paciente);
                        break;
                    case "4":
                        sistema.CompararClinicas();
                        break;
                    case "5":
                        sistema.SolicitarConsultaDoctor(paciente);
                        break;
                    case "6":
                        sistema.ActualizarDatosPaciente(paciente);
                        break;
                    case "7":
                        paciente.MostrarInformacion();
                        break;
                    case "0":
                        Console.WriteLine("\nCerrando sesion...");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\nOpcion no valida.");
                        break;
                }

            } while (!salir);
        }

        public void MenuDoctor(Doctor doctor)
        {
            bool salir = false;

            do
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("           MENU DOCTOR");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Revisar cola de pacientes");
                Console.WriteLine("2. Validar diagnosticos");
                Console.WriteLine("3. Gestionar registros medicos");
                Console.WriteLine("4. Ver estadisticas");
                Console.WriteLine("5. Agregar nodo al arbol (beta)");
                Console.WriteLine("6. Entrenar modelo IA (beta)");
                Console.WriteLine("7. Ver informacion personal");
                Console.WriteLine("0. Cerrar sesion");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        sistema.RevisarColaPacientes();
                        break;
                    case "2":
                        sistema.ValidarDiagnosticos(doctor);
                        break;
                    case "3":
                        sistema.GestionarRegistrosMedicos();
                        break;
                    case "4":
                        sistema.VerEstadisticas();
                        break;
                    case "5":
                        sistema.FuncionBeta("Agregar nodo al arbol de diagnostico");
                        break;
                    case "6":
                        sistema.FuncionBeta("Entrenar modelo IA");
                        break;
                    case "7":
                        doctor.MostrarInformacion();
                        break;
                    case "0":
                        Console.WriteLine("\nCerrando sesion...");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\nOpcion no valida.");
                        break;
                }

            } while (!salir);
        }
    }
}