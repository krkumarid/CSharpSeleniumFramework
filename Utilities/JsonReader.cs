using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.Utilities
{
    public class JsonReader
    {

        /*
         * ExtractData() is used to read a single String value from the Json file based on token( Key)
         * Parameter : token( String)
         * return String (value) 
        */
        public string ExtractData(String tocken)
        {
            var myJsonFile = File.ReadAllText("Utilities\\TestData.json");
            var jsonObject = JToken.Parse(myJsonFile);
            return jsonObject.SelectToken(tocken).Value<string>();
        }

        /*
        * ExtractDataArray() is used to read a Array of  Strings  from the Json file based on token( Key)
        * Parameter : token( String)
        * return String[] (value) 
       */
        public string[] ExtractDataArray(String tocken)
        {
            var myJsonFile = File.ReadAllText("Utilities\\TestData.json");
            var jsonObject = JToken.Parse(myJsonFile);
            IList<String> productList = jsonObject.SelectToken(tocken).Values<string>().ToList();
            return productList.ToArray();
        }
    }
    
}
