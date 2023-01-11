using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Probes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
namespace Health_Probe.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private LocalProbe? probe;

    static HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds((int)JObject.Parse(System.IO.File.ReadAllText("appsettings.json"))["TimeOut"]) };

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        probe = JsonConvert.DeserializeObject<LocalProbe>(JObject.Parse(System.IO.File.ReadAllText("appsettings.json"))["LocalProbe"].ToString());
    }

    public ActionResult OnGet()
    {
        List<bool> responses = new List<bool>();
        string url;
        foreach (LocalPath path in probe.paths)
        {
            if (path.ssl == true)
            {
                url = "https://" + path.ip + ":" + path.port + path.path;
            }
            else
            {
                url = "http://" + path.ip + ":" + path.port + path.path;
            }
            try
            {
                client.DefaultRequestHeaders.Host = path.hostname;
                responses.Add(client.GetAsync(url).Result.IsSuccessStatusCode);
            }
            catch
            {
                responses.Add(false);
            }
        }
        if (responses.Contains(false))
        {
            string result = "";
            int count = 0;
            foreach(bool test in responses)
            {
                result += "Path " + count.ToString() + " Result:" + test + "\n";
                count++;
            }
            throw new Exception(result);
        }
        return new JsonResult("All paths healthy");
    }
}
