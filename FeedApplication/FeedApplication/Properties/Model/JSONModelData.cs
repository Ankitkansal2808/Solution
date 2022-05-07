using System.Collections.Generic;

namespace FeedApplication.Properties.Model
{
    class JSONModelData
    {
        public List<products> products { get; set; }

    }
    class products
    {
        public List<string> categories { get; set; }
        public string twitter { get; set; }
        public string title { get; set; }
    }
}
