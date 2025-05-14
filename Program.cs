using System;
using LoveCampus.infrastructure.Mysql;

class Program {
    static void Main(string[] args) {
        string connectionString = "server=localhost;user=roor; password=123456;database=love";
        
        while (true) {
            Console.Clear();
            Console.WriteLine("❤️  Bienvenido a LoveCampus ❤️")
            Console.WriteLine("❤️  1. Usuarios");
            Console.WriteLine("❤️  2. Pais");
            Console.WriteLine("❤️  3. Region");
            Console.WriteLine("❤️  4. Ciudad");
            Console.WriteLine("❤️  5. Salir");
            Console.Write("❤️  Ingrese una opción: ");
            var opcion = Console.ReadLine();
        }
    }
}