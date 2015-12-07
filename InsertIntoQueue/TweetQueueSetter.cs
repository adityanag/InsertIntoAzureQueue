using System;


namespace InsertIntoQueue
{
    class TweetQueueSetter
    {
        public string TableName { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string AccessKey { get; set; }
        public string SearchTerm { get; set; }
        public string ControlAction { get; set; }
        public DateTime Time { get; set; }

    }
}
