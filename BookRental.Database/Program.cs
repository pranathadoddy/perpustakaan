using DbUp;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace BookRental.Database
{
    class Program
    {
        static int Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("dbup.appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader = DeployChanges.To
                           .SqlDatabase(connectionString)
                           .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                           .WithTransaction()
                           .LogToConsole()
                           .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                Console.ReadLine();
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            Console.ReadLine();
            return 0;
        }
    }
}
