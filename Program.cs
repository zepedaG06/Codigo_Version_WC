using System;

namespace MEDICENTER
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Menu menuPrincipal = new Menu();
            menuPrincipal.MostrarMenuPrincipal();
        }
    }
}