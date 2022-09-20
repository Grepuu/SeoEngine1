using ResponseDataCollector.ServicesLibrary;
using ResponseDataCollector.ServicesLibrary.Models;
using System.Net;

namespace ConsoleApplication;

internal class Program
{
    static void Main(string[] args)
    {
        Start().GetAwaiter().GetResult();
    }

    static async Task Start()
    {
        Console.Title = "Response Data Collector";
        Console.Clear();
        Console.WriteLine("RESPONSE DATA COLLECTOR");
        Console.WriteLine("Modes: ");
        Console.WriteLine("[1] Get From Url");
        Console.WriteLine("[2] Get From IP Address");
        Console.WriteLine("[3] Get From E-mail");
        Console.WriteLine("[0] Exit");
        Console.Write("Enter: ");

        var _receive = Console.ReadLine();

        if (!string.IsNullOrEmpty(_receive))
        {
            Console.Clear();

            if (_receive == "1")
            {
                Console.Write("Enter Url: ");
                var _url = Console.ReadLine();

                if (!string.IsNullOrEmpty(_url))
                {
                    if (Uri.TryCreate(_url, UriKind.Absolute, out Uri? _uri))
                    {
                        ResponseDataCollectorService responseDataCollectorService = new(_uri);
                        DisplayResponseContentService.DisplayContent(responseDataCollectorService.Response);
                        SavingToFile(responseDataCollectorService.Response);
                    }
                    else
                    {
                        Console.WriteLine("Invalid url!");
                        Console.ReadKey();
                        await Start();
                    }
                }
            }
            else if (_receive == "2")
            {
                Console.Write("Enter Ip Address: ");
                var _ip = Console.ReadLine();

                if (!string.IsNullOrEmpty(_ip))
                {
                    if (IPAddress.TryParse(_ip, out IPAddress? _ipaddress))
                    {
                        ResponseDataCollectorService responseDataCollectorService = new(_ipaddress);
                        DisplayResponseContentService.DisplayContent(responseDataCollectorService.Response);
                        SavingToFile(responseDataCollectorService.Response);
                    }
                    else
                    {
                        Console.WriteLine("Invalid IP Address!");
                        Console.ReadKey();
                        await Start();
                    }
                }
            }
            else if (_receive == "3")
            {
                Console.Write("Enter E-mail: ");
                var _email = Console.ReadLine();

                if (!string.IsNullOrEmpty(_email))
                {
                    ResponseDataCollectorService responseDataCollectorService = new(_email);
                    DisplayResponseContentService.DisplayContent(responseDataCollectorService.Response);
                    SavingToFile(responseDataCollectorService.Response);
                }
            }
            else if (_receive == "0")
            {
                Environment.Exit(0);
            }
        }
        Console.WriteLine("");

        await Start();
    }

    private static void SavingToFile(ResponseDataModel model)
    {
        Console.WriteLine("Modes:");
        Console.WriteLine("[1] New Request");
        Console.WriteLine("[2] Send To File .txt");
        Console.Write("Enter: ");

        if (Console.ReadLine() == "2")
        {
            Console.Write("Enter file path: ");

            var _path = Console.ReadLine();

            if (File.Exists(_path))
            {
                Console.WriteLine("File is Existed!");
                Console.WriteLine("Modes: ");
                Console.WriteLine($"[1] Overwrite - {Path.GetFileName(_path)}");
                Console.WriteLine("[2] New Request");

                Console.Write("Enter: ");
                var _r = Console.ReadLine();

                if (_r == "3")
                {
                    return;
                }
            }

            CreateFile(model, _path);
        }
    }

    private static void CreateFile(ResponseDataModel model, string path)
    {
        try
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine($"Url: {model.Url.Host}");
                sw.WriteLine($"Response Time [ms]: {model.ResponseTime}");
                sw.WriteLine($"Status Code: {model.StatusCode}");
                sw.WriteLine($"DNS Zone: {model.DNSZone}");
                sw.WriteLine($"Scripts on Website:: {model.Content.ScriptsAmount}");
                sw.WriteLine($"CSS'es on Website: {model.Content.CSSAmount}");
                sw.WriteLine($"Paragraphs: {model.Content.Paragraphs}");
                sw.WriteLine($"Text (words): {model.Content.Words}");
                sw.WriteLine($"Images: {model.Content.Images}");
                sw.WriteLine($"Tags: " +
                             $"H1: {model.Content.Titles[0]} | " +
                             $"H2: {model.Content.Titles[1]} | " +
                             $"H3: {model.Content.Titles[2]} | " +
                             $"H4: {model.Content.Titles[3]} " +
                             $"| H5: {model.Content.Titles[4]}");
                sw.WriteLine($"Footer:  ({model.Content.Footers != 0}) {model.Content.Footers}");
                sw.WriteLine($"Section: ({model.Content.Headers != 0}) {model.Content.Sections}");
                sw.WriteLine($"Header:  ({model.Content.Sections != 0}) {model.Content.Headers}");
                sw.WriteLine($"Meta Tags: {model.Content.MetaTags}");
            }
        }
        catch (DirectoryNotFoundException e)
        {
            Console.WriteLine(e);
        }
    }
}