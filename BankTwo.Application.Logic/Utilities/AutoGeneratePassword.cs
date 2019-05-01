using System;
using ViewModels;

namespace BankTwo.Application.Logic.Utilities
{
    public class AutoGeneratePassword
    {
        public static string AutoGenerateUserPassword(RegisterViewModel registerViewModel)
        {

            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            char[] symbols = { '-', '%', '$', '*', '!' };
            char[] alphabets = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
            string stringVar = registerViewModel.OtherNames + registerViewModel.FirstName + registerViewModel.LastName;
            string password = "";
            Random rand = new Random();
            for (int i = 0; i < stringVar.Length; i++)
            {
                int x = rand.Next(0, alphabets.Length - 1);
                
                if (i == 0)
                {
                    password += alphabets[x].ToString().ToUpper();
                }
                password += alphabets[x];
            }
            password = password.Substring(0, 12);
            password = password + symbols[rand.Next(0, symbols.Length - 1)] +
                numbers[rand.Next(0, numbers.Length - 1)] +
                numbers[rand.Next(0, numbers.Length - 1)];
            
            return password;
        }
        public static string AutoGenerateUserPassword()
        {
            string password = Guid.NewGuid().ToString().Replace("-","").Substring(0,12);
            return password;
        }
    }

}
