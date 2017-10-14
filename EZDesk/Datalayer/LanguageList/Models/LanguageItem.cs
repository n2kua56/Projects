using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.LanguageList.Models
{
    public class LanguageItem
    {
        public int ID { get; set; }
        public bool IsActive { get; set; }
        public string Language { get; set; }
        public int DisplayOrder { get; set; }
        public int DefaultDisplay { get; set; }
        public string ISO6391 { get; set; }
        public string ISO6392 { get; set; }

        public LanguageItem()
        {
        }
    }
}
