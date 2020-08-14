using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    // model of todoitem
    public class TodoItem
    {
        public string id { get; set; }
        public string date { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public float complete { get; set; }
        public string status { get; set; }
    }
}
