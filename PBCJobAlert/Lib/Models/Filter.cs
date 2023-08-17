using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCJobAlert.Lib.Models
{
    public class Filter
    {
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public bool ShowFilter { get; set; } = true;
        public string FilterMode { get; set; } = "Simple";
    }
}
