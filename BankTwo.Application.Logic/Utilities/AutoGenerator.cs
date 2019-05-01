using System;

namespace BankTwo.Application.Logic.Utilities
{
    public static class AutoGenerator
    {
        private static int[] numbers = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static int GenerateAccountCode(int mainAccountId,int glCategoryId)
        {
            string codeString = mainAccountId.ToString() + glCategoryId.ToString();
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                codeString += numbers[rand.Next(0, 9)];
            }

            return int.Parse(codeString);           
        }
        public static int GenerateCustomerId()
        {
            string codeString = "";
            Random rand = new Random();
            for (int i = 0; i < 6; i++)
            {
                codeString += numbers[rand.Next(0, 9)];
            }

            return int.Parse(codeString);
        }
        public static int GenerateCustomerAccountIdentificationNo(int customersId,int accountType)
        {
            //Customer account code is a 9 digit Number
            string Identification = "";
            var loop = 8 - Math.Floor(Math.Log10(customersId) + 1);
            Random rand = new Random();
            for (int i = 0; i < loop - 1; i++)
            {
                Identification += rand.Next(0,9);
            }
            string idString = customersId.ToString() + Identification + accountType.ToString();
            int identificationNumber = int.Parse(idString);
            return identificationNumber;
        }
    }
}
