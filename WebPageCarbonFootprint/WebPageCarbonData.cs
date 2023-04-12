using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WebPageCarbonFootprint
{
    public class WebPageCarbonData
    {
        InputData userInput;
        WebsiteCarbonGetResponse getResponse;

        public WebPageCarbonData(InputData userInput)
        {
            this.userInput = userInput;
        }

        // Formats Info from Get Response into Readable Results 
        public async Task printCarbonData() 
        {
            this.getResponse = await performGetRequest();
            string formattedOutput = formatPrintOutput();
            Console.WriteLine(formattedOutput);
        }

        // Connect to Website Carbon API and send get request for information 
        public async Task<WebsiteCarbonGetResponse> performGetRequest()
        {
            // Create Client and Request using Inputted URL
            WebsiteCarbonGetResponse jsonObject;
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.websitecarbon.com/site?url=" + this.userInput.webPageURL);
                var response = await client.SendAsync(request);
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch(HttpRequestException ex)
                {
                    Console.WriteLine("Invalid Website for Testing");
                    throw new Exception("Please input a valid website for carbon data");
                }
                string jsonString = await response.Content.ReadAsStringAsync();
                jsonObject = JsonConvert.DeserializeObject<WebsiteCarbonGetResponse>(jsonString);
            }
            return jsonObject;
        }

        //Print in readable format
        public string formatPrintOutput()
        {
            string output = Environment.NewLine;
            output += "URL Tested: " + getResponse.url + Environment.NewLine;
            output += "Website is Considered Green: " + Convert.ToString(getResponse.green) + Environment.NewLine;
            output += "Number of bytes transferred during page load: " + getResponse.bytes + Environment.NewLine;
            output += "This website is cleaner than " + Convert.ToString(getResponse.cleanerThan * 100) + "% of sites tested" + Environment.NewLine;
            output += "Approximate number of bytes transferred by page load, adjusted to take first time vs returning visitor " +
                      "percentage into account: " + getResponse.statistics.adjustedBytes + Environment.NewLine;
            output += "Amount of energy transferred on each page load: " + getResponse.statistics.energy + " KWg" + Environment.NewLine;
            output += "Amount of CO2 transferred on each page load:" + Environment.NewLine;
            // If user is using renewable energy return those stats
            if (this.userInput.usingRenewableEnergy)
            {
                output += "    " + getResponse.statistics.co2.renewable.grams + " grams" + Environment.NewLine;
                output += "    " + getResponse.statistics.co2.renewable.litres + " litres" + Environment.NewLine;
            }
            // If user is using grid energy use those stats 
            else
            {
                output += "    " + getResponse.statistics.co2.grid.grams + " grams" + Environment.NewLine;
                output += "    " + getResponse.statistics.co2.grid.litres + " litres" + Environment.NewLine;
            }

            return output;
        }
    }
}
