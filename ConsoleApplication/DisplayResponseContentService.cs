using ResponseDataCollector.ServicesLibrary.Models;

namespace ConsoleApplication;

public static class DisplayResponseContentService
{
    public static void DisplayContent(ResponseDataModel model)
    {
        Console.WriteLine();
        WriteWithColor("Url:", ConsoleColor.Yellow);
        WriteWithColor(model.Url.Host, ConsoleColor.Cyan, true);

        WriteWithColor("Response Time [ms]:", ConsoleColor.Yellow);
        WriteWithColor(model.ResponseTime.ToString(), ConsoleColor.Cyan, true);

        WriteWithColor("Status Code:", ConsoleColor.Yellow);
        if ((int)model.StatusCode >= 200 && (int)model.StatusCode < 300)
        {
            WriteWithColor($"({model.StatusCode})", ConsoleColor.Green, true);
        }
        else
        {
            WriteWithColor($"({model.StatusCode})", ConsoleColor.Red, true);
        }

        WriteWithColor("DNS Zone:", ConsoleColor.Yellow);
        WriteWithColor(model.DNSZone, ConsoleColor.Cyan, true);

        Console.WriteLine();

        WriteWithColor("Scripts on Website:", ConsoleColor.Yellow);
        WriteWithColor(model.Content.ScriptsAmount.ToString(), ConsoleColor.Cyan, true);

        WriteWithColor("CSS'es on Website:", ConsoleColor.Yellow);
        WriteWithColor(model.Content.CSSAmount.ToString(), ConsoleColor.Cyan, true);

        WriteWithColor("Paragraphs:", ConsoleColor.Yellow);
        WriteWithColor(model.Content.Paragraphs.ToString(), ConsoleColor.Cyan, true);

        WriteWithColor("Text (words):", ConsoleColor.Yellow);
        WriteWithColor(model.Content.Words.ToString(), ConsoleColor.Cyan, true);

        WriteWithColor("Images:", ConsoleColor.Yellow);
        WriteWithColor(model.Content.Images.ToString(), ConsoleColor.Cyan, true);

        WriteWithColor("H1:", ConsoleColor.Yellow);
        WriteWithColor(model.Content.Titles[0].ToString(), ConsoleColor.Cyan);
        WriteWithColor("H2:", ConsoleColor.Yellow);
        WriteWithColor(model.Content.Titles[1].ToString(), ConsoleColor.Cyan);
        WriteWithColor("H3:", ConsoleColor.Yellow);
        WriteWithColor(model.Content.Titles[2].ToString(), ConsoleColor.Cyan);
        WriteWithColor("H4:", ConsoleColor.Yellow);
        WriteWithColor(model.Content.Titles[3].ToString(), ConsoleColor.Cyan);
        WriteWithColor("H5:", ConsoleColor.Yellow);
        WriteWithColor(model.Content.Titles[4].ToString(), ConsoleColor.Cyan, true);

        WriteWithColor("Footer:", ConsoleColor.Yellow);
        WriteWithColor($" ({model.Content.Footers != 0})", ConsoleColor.Cyan);
        WriteWithColor(model.Content.Footers.ToString(), ConsoleColor.Cyan, true);

        WriteWithColor("Header:", ConsoleColor.Yellow);
        WriteWithColor($" ({model.Content.Headers != 0})", ConsoleColor.Cyan);
        WriteWithColor(model.Content.Headers.ToString(), ConsoleColor.Cyan, true);

        WriteWithColor("Section:", ConsoleColor.Yellow);
        WriteWithColor($"({model.Content.Sections != 0})", ConsoleColor.Cyan);
        WriteWithColor(model.Content.Sections.ToString(), ConsoleColor.Cyan, true);

        WriteWithColor("Meta Tags:", ConsoleColor.Yellow, true);
        WriteWithColor(model.Content.MetaTags.ToString(), ConsoleColor.Cyan, true);

        Console.WriteLine();
    }

    private static void WriteWithColor(string text, ConsoleColor color, bool allLine = false)
    {
        if (allLine)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(" " + text);
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = color;
            Console.Write(" " + text);
            Console.ResetColor();
        }
    }
}
