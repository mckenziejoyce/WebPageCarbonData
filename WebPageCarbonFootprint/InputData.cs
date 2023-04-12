using System;
namespace WebPageCarbonFootprint
{
    public class InputData
    {
        // Saves web page to test, if renewable energy is being used, if grid power is being used
        public string webPageURL;
        public Boolean usingRenewableEnergy;
        public Boolean usingGridEnergy;

        public InputData()
        {
            // Get User Input for website to be Tested 
            Console.WriteLine("Enter the website you want carbon emissions data on");
            this.webPageURL = Console.ReadLine();

            // Get User Input for Type of Energy Being Used to access Page (Renewable/Grid)
            Boolean validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Are you using renewable energy (not grid) to access this website Y/N");
                string energyTypeInput = Console.ReadLine().ToUpper();
                this.usingRenewableEnergy = (energyTypeInput == "Y" || energyTypeInput == "YES");
                this.usingGridEnergy = (energyTypeInput == "N" || energyTypeInput == "NO");
                validInput = this.usingRenewableEnergy || this.usingGridEnergy;
                if (!validInput)
                {
                    Console.WriteLine("Invalid Input, Valid Options Include: Yes, No, Y, or N");
                }
            }
        }

        // Alternative Constructor if URL and Energy Used are already known - currently used for testing
        public InputData(string URL, Boolean usingRenewableEnergy)
        {
            this.webPageURL = URL;
            if (usingRenewableEnergy)
            {
                this.usingRenewableEnergy = true;
                this.usingGridEnergy = false;
            }
            else
            {
                this.usingGridEnergy = true;
                this.usingRenewableEnergy = false;
            }
        }
        
    }
}
