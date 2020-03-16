using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Train.Model
{
    public class ListSettingModel
    {
        public int? CurrentPage { get; set; }
        public string SearchFilter { get; set; }
        public string SearchValue { get; set; }
        public string SortOrder { get; set; }
    }
}
