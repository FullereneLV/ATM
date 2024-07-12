using System;

namespace HW3_ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Please enter email");
            var email = Console.ReadLine();

            Console.WriteLine("Please enter password");
            var password = Console.ReadLine();
            */
            Console.WriteLine("Please choose option: Log in, Sign in or Leave");
            var option = Console.ReadLine();
           
            if (option == "Log in" || option == "login" || option == "log")
            {
                Console.WriteLine("Please enter email");
                var email1 = Console.ReadLine();

                Console.WriteLine("Please enter password");
                var password1 = Console.ReadLine();
            }
            //else if (option == "Sign in" || option == "signin" || option == "sing")
            //{
            //    Console.WriteLine("Please enter email");
            //    var email2 = Console.ReadLine();

            //    Console.WriteLine("Please enter password");
            //    var password2 = Console.ReadLine();

            //    Console.WriteLine("Please enter the same password");
            //    var password21 = Console.ReadLine();
            //}
            else if (option == "leave" || option == "Leave")
            {
                Environment.Exit(1);
            }
        }
    }
}
