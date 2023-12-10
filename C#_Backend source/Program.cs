using Autofac;
using Backend.Interfaces;
using Backend.Model;
using Backend.Services;
using System;

namespace Backend
{
    class Program
    {
        private static IContainer container;

        static void Main()
        {
            container = ConfigureContainer();

            while (true)
            {
                Console.WriteLine("Main Menu");
                Console.WriteLine("---------");
                Console.WriteLine();
                Console.WriteLine("1. View Movie Stars List");
                Console.WriteLine("2. Calculate Net Salary");
                Console.WriteLine("3. Exit");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    // Option 1 is for reading ad displaying the movie stars
                    if (choice == 1)
                    {
                        var application = container.Resolve<ApplicationService>();

                        application.Run();
                    }
                    // Option 2 is for calculating the salary
                    else if (choice == 2)
                    {
                        CalculateNetSalary();
                    }
                    // Option 3 is to exit the program
                    else if (choice == 3)
                    {
                        break;
                    }
                    // This is for invalid input between 1, 2 or 3
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter 1 or 2 or 3.");
                    }
                }
                // This is if the entered number is not valid
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                }
            }
        }

        static void CalculateNetSalary()
        {
            Console.WriteLine("Enter gross salary in Imaginaria Dolars (IDR):");

            if (double.TryParse(Console.ReadLine(), out double grossSalary))
            {
                var salaryCalculator = container.Resolve<ISalaryCalculator>();
                double netSalary = salaryCalculator.CalculateNetSalary(grossSalary);

                Console.WriteLine($"Net Salary: {netSalary} IDR");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                Console.WriteLine();
            }
        }

        //You should configure DI container (Autofac) or other DI Framework
        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            //Here you should register Interfaces with their referent classes

            // 1. I register ApplicationService for the first task to read the input.txt file
            builder.RegisterType<ApplicationService>().AsSelf();
            // 2. I register the ISalaryCalculator for the second Task for the salary
            builder.RegisterType<SalaryCalculator>().As<ISalaryCalculator>();
            // 3. I register the ILogger for the errors
            builder.RegisterType<Logger>().As<ILogger>();

            return builder.Build();
        }
    }
}