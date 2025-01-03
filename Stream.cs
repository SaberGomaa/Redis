using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text.Json.Serialization;

namespace Redis
{
    public class Stream
    {
        private IDatabase _database;
        public Stream(IDatabase database)
        {
            _database = database;
        }

        public void StreamAdd()
        {
            var entryId1 = _database.StreamAdd("saber:stream1",
                new NameValueEntry[]
                {
                    new NameValueEntry("name", "saber"),
                    new NameValueEntry("age", 25)
                });

            long startId = ((DateTimeOffset)DateTime.Now.AddHours(-1)).ToUnixTimeMilliseconds();
            long endId = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();

            var entries = _database.StreamRange("saber:stream1", startId, endId);
            foreach (var entry in entries)
            {
                Console.WriteLine(entry.Id);
                foreach (var item in entry.Values)
                    Console.WriteLine(item.Name + " : " + item.Value);
            }
        }

        public void streamMaxLen()
        {
            var entryId = _database.StreamAdd("saber:stream2",
              new NameValueEntry[]
              {
                    new NameValueEntry("name", "eman"),
                    new NameValueEntry("age", 30)
              }, maxLength: 2);
            var entryId1 = _database.StreamAdd("saber:stream2",
                new NameValueEntry[]
                {
                    new NameValueEntry("name", "maher"),
                    new NameValueEntry("age", 28)
                }, maxLength: 2);
            var entryId2 = _database.StreamAdd("saber:stream2",
                new NameValueEntry[]
                {
                    new NameValueEntry("name", "ahmed"),
                    new NameValueEntry("age", 8)
                }, maxLength: 2);
            var entryId3 = _database.StreamAdd("saber:stream2",
                new NameValueEntry[]
                {
                    new NameValueEntry("name", "haidy"),
                    new NameValueEntry("age", 6)
                }, maxLength: 2);

            long startId = ((DateTimeOffset)DateTime.Now.AddMinutes(-10)).ToUnixTimeMilliseconds();
            long endId = ((DateTimeOffset)DateTime.Now.AddMinutes(5)).ToUnixTimeMilliseconds();

            //var entries = _database.StreamRange("saber:stream2", startId, endId);
            //var entries = _database.StreamRange("saber:stream2");
            var entries = _database.StreamRange("saber:stream2", messageOrder: Order.Descending);
            foreach (var entry in entries)
                Console.WriteLine(JsonConvert.SerializeObject(entry.Values));

        }

        public void streamRead()
        {
            var entry1 = _database.StreamAdd("saber:stream3",
                new NameValueEntry[]
                {
                    new NameValueEntry("name", "saber"),
                    new NameValueEntry("age", 25)
                });
            var entry2 = _database.StreamAdd("saber:stream3",
                new NameValueEntry[]
                {
                    new NameValueEntry("name", "maher"),
                    new NameValueEntry("age", 28)
                });

            long startDate = ((DateTimeOffset)DateTime.Now.AddMinutes(-10)).ToUnixTimeMilliseconds();
            long endDate = ((DateTimeOffset)DateTime.Now.AddMinutes(5)).ToUnixTimeMilliseconds();

            var entries = _database.StreamRead("saber:stream3", "0", 2);

            foreach (var entry in entries)
                    Console.WriteLine(JsonConvert.SerializeObject(entry));

        }
    }
}