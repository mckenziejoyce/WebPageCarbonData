using System.Threading.Tasks;

namespace WebPageCarbonFootprint
{
    class MainClass
    {
        static async Task Main(string[] args)
        {
            // Get User Input for which website to test and type of enery being used
            InputData inputData = new InputData();

            // Use API to get Carbon Data about Webpage
            WebPageCarbonData pageData = new WebPageCarbonData(inputData);

            // Print Carbon Emissions Data about Webpage
            await pageData.printCarbonData();
        }

    }
}
