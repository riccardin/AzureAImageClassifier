using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace ImageClassificationApp
{
    class Program
    {
        private static readonly string subscriptionKey = "YOUR_SUBSCRIPTION_KEY";
        private static readonly string endpoint = "YOUR_ENDPOINT";

        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide image file paths as arguments.");
                return;
            }

            var client = Authenticate(endpoint, subscriptionKey);

            foreach (var imagePath in args)
            {
                if (File.Exists(imagePath))
                {
                    var analysisResult = await AnalyzeImage(client, imagePath);
                    var categoryProbabilities = MapToCategories(analysisResult);
                    DisplayResults(categoryProbabilities);
                }
                else
                {
                    Console.WriteLine($"File not found: {imagePath}");
                }
            }
        }

        private static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key))
            {
                Endpoint = endpoint
            };
            return client;
        }

        private static async Task<ImageAnalysis> AnalyzeImage(ComputerVisionClient client, string imagePath)
        {
            using (var imageStream = File.OpenRead(imagePath))
            {
                var features = new List<VisualFeatureTypes?> { VisualFeatureTypes.Categories, VisualFeatureTypes.Description, VisualFeatureTypes.Tags };
                var analysis = await client.AnalyzeImageInStreamAsync(imageStream, features);
                return analysis;
            }
        }

        private static Dictionary<string, double> MapToCategories(ImageAnalysis analysis)
        {
            var categoryProbabilities = new Dictionary<string, double>
            {
                { "People", 0.0 },
                { "Technology", 0.0 },
                { "Post-it", 0.0 },
                { "Faces", 0.0 },
                { "Feelings", 0.0 }
            };

            foreach (var category in analysis.Categories)
            {
                if (category.Name.Contains("people"))
                {
                    categoryProbabilities["People"] = Math.Max(categoryProbabilities["People"], category.Score);
                }
                if (category.Name.Contains("technology"))
                {
                    categoryProbabilities["Technology"] = Math.Max(categoryProbabilities["Technology"], category.Score);
                }
                if (category.Name.Contains("post-it"))
                {
                    categoryProbabilities["Post-it"] = Math.Max(categoryProbabilities["Post-it"], category.Score);
                }
                if (category.Name.Contains("face"))
                {
                    categoryProbabilities["Faces"] = Math.Max(categoryProbabilities["Faces"], category.Score);
                }
                if (category.Name.Contains("feeling"))
                {
                    categoryProbabilities["Feelings"] = Math.Max(categoryProbabilities["Feelings"], category.Score);
                }
            }

            return categoryProbabilities;
        }

        private static void DisplayResults(Dictionary<string, double> categoryProbabilities)
        {
            Console.WriteLine("Classification Results:");
            foreach (var category in categoryProbabilities)
            {
                Console.WriteLine($"{category.Key}: {category.Value:P2}");
            }
        }
    }
}
