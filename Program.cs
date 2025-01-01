using Redis;
using StackExchange.Redis;

// connect to redis
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase database = redis.GetDatabase();

//setGetStringRedis(database);
//setGetHashRedis(database);

//JSON json = new JSON(database);
//json.JsonSet();
//json.jsonGet();


DSRedis dsRedis = new DSRedis(database);
dsRedis.listMove();
//dsRedis.ListPushPop();


void setGetStringRedis(IDatabase db)
{
    db.StringSet("saber:key", "saber:value");
    Console.WriteLine(db.StringGet("saber:key"));
}

void setGetHashRedis(IDatabase db)
{
    db.HashSet("saber:hash", new HashEntry[]
    {
        new HashEntry("key1", "value1"),
        new HashEntry("key2", "value2")
    });


    HashEntry[] hashEntries = db.HashGetAll("saber:hash");
    foreach (var entry in hashEntries)
    {
        Console.WriteLine($"{entry.Name}: {entry.Value}");
    }
}
