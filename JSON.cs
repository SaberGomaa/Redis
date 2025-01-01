using StackExchange.Redis;
using NRedisStack;
using NRedisStack.RedisStackCommands;

namespace Redis
{
    public class JSON
    {
        private IJsonCommands json;
        public JSON(IDatabase db)
        {
            json = db.JSON();
        }
        public void JsonSet()
        {
            json.Set("jsonkey", "$", "\"jsonValue\"");
        }
        public void JsonGet()
        {
            Console.WriteLine(json.Get("json:key", "$"));
        }

        public void ElementType()
        {
            Console.WriteLine(json.Type("json:key").FirstOrDefault());
        }
        public void ElementLength()
        {
            Console.WriteLine(json.StrLen("json:key").FirstOrDefault());
        }
    }
}
