
using FeedApplication.Properties.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FeedApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Length > 2)
                {
                    switch (args[2])
                    {
                        case "capterra":
                            GetDataFromYMLFile(args[3]);
                            break;
                        case "softwareadvice":
                            GetDataFromJSONFile(args[3]);
                            break;
                        default:
                            Console.WriteLine("Source name is incorrect");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Enter input in right format like '$ import capterra feed-products/capterra.yaml'");
                }
            }
            else
            {
                Console.WriteLine("Enter input in right format like '$ import capterra feed-products/capterra.yaml'");
            }
        }

        #region GetDataFromJSONFile
        /// <summary>
        /// Get json data from file and populate the data on the screen
        /// </summary>
        /// <param name="filePath">File path for json file</param>
        /// <returns>Returns true if data is shown on screen, otherwise false</returns>
        private static bool GetDataFromJSONFile(string filePath)
        {
            bool isDataShown = false;
            try
            {
                //Create path of the file
                string path = @$"{AppDomain.CurrentDomain.BaseDirectory}\{filePath}".Replace('/', '\\');
                using (StreamReader reader = File.OpenText(@path))
                {
                    string json = reader.ReadToEnd();
                    var data = JsonConvert.DeserializeObject<JSONModelData>(json);
                    //Check if there are any products or not
                    if (data != null && data.products != null && data.products.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in data.products)
                        {
                            if (sb.Length > 0)
                            {
                                sb.Append('\n');
                            }
                            sb.Append("importing:");

                            //Creating text for category
                            if (item.categories != null && item.categories.Count > 0)
                            {
                                StringBuilder categories = new StringBuilder();
                                categories.Append(" Categories:");
                                item.categories.ForEach(x => categories.Append($" {x},"));
                                sb.Append(categories.ToString().TrimEnd(','));
                            }

                            //Creating text for title
                            if (!string.IsNullOrEmpty(item.title))
                            {
                                sb.Append($"; Title: {item.title}");
                            }

                            //Creating text for twitter
                            if (!string.IsNullOrEmpty(item.twitter))
                            {
                                sb.Append($"; Twitter: {item.twitter}");
                            }
                        }
                        Console.WriteLine(sb);
                        isDataShown = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while reading JSON file. Error- {ex.Message}");
            }
            return isDataShown;
        }
        #endregion

        #region GetDataFromYMLFile
        /// <summary>
        /// Get yml data from file and populate the data on the screen
        /// </summary>
        /// <param name="filePath">File path for yml file</param>
        /// <returns>Returns true if data is shown on screen, otherwise false</returns>
        private static bool GetDataFromYMLFile(string filePath)
        {
            bool isDataShown = false;
            try
            {
                //Create path of the file
                string path = @$"{AppDomain.CurrentDomain.BaseDirectory}\{filePath}".Replace('/', '\\');
                using (var reader = new StreamReader(path))
                {
                    var deserializer = new Deserializer();
                    var data = deserializer.Deserialize<List<YMLModelData>>(reader);
                    if (data != null && data.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in data)
                        {
                            if (sb.Length > 0)
                            {
                                sb.Append('\n');
                            }
                            sb.Append("importing:");
                            //Creating text for name
                            if (!string.IsNullOrEmpty(item.name))
                            {
                                sb.Append($" Name: \"{item.name}\"");
                            }
                            //Creating text for tags
                            if (!string.IsNullOrEmpty(item.tags))
                            {
                                sb.Append($"; Categories: {item.tags}");
                            }                         
                            //Creating text for twitter
                            if (!string.IsNullOrEmpty(item.twitter))
                            {
                                sb.Append($"; Twitter: @{item.twitter}");
                            }
                        }
                        Console.WriteLine(sb);
                        isDataShown = true;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured while reading yml file. Error- {ex.Message}");
            }
            return isDataShown;
        }
        #endregion
    }
}
