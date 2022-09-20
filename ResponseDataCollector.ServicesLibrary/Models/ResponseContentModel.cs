using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ResponseDataCollector.ServicesLibrary.Models;

public class ResponseContentModel
{
    public string Content { get; set; }

    public int ScriptsAmount { get; set; }

    public int CSSAmount { get; set; }

    public int Paragraphs { get; set; }

    public int Words { get; set; }
    public int Images { get; set; }

    public int[] Titles { get; set; } = new int[6];

    public int Footers { get; set; }

    public int Headers { get; set; }

    public int Sections { get; set; }

    public JsonArray MetaTags { get; set; }
}
