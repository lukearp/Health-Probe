using Newtonsoft.Json;

namespace Models.Probes
{
    public class LocalProbe
    {
        public List<LocalPath> paths { get; set; }

        public LocalProbe (){
            
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class LocalPath
    {
        public string hostname { get; set; }
        public string ip {get;set;}
        public int port { get; set; }
        public string path {get;set;}
        public bool ssl {get;set;}

        public LocalPath () {
        }
    }
}