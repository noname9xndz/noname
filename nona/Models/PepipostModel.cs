using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nona.Models
{
    public class PepipostModel
    {
        public PepipostModel()
        {
            personalizations = new List<Personalization>();
            from = new From();
        }
        public List<Personalization> personalizations { get; set; }
        public From from { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
    }

    public class From
    {
        public string fromEmail { get; set; }
        public string fromName { get; set; }
    }

    public class Personalization
    {
        public string recipient { get; set; }
    }


}
