using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nona.Models
{
    public class PepipostModel
    {
        public From from { get; set; }
        public string subject { get; set; }
        public Content[] content { get; set; }
        public Personalization[] personalizations { get; set; }
    }

    public class From
    {
        public string email { get; set; }
        public string name { get; set; }
    }

    public class Content
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Personalization
    {
        public To[] to { get; set; }
    }

    public class To
    {
        public string email { get; set; }
        public string name { get; set; }
    }


}
