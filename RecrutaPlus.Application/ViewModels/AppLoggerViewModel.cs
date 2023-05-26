using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecrutaPlus.Application.ViewModels
{
    public class AppLoggerViewModel
    {
        public int id { get; set; }
        public string message { get; set; }
        public string template { get; set; }
        public string level { get; set; }
        public DateTime timeStamp { get; set; }
        public string exception { get; set; }
        public string properties { get; set; }
    }
}
