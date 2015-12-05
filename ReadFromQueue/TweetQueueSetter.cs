using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertIntoQueue
{
    class TweetQueueSetter
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string AccessKey { get; set; }
        public string SearchTerms { get; set; }
        public string ControlAction { get; set; }
        public DateTime Time { get; set; }

    }
}
