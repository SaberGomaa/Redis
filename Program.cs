using StackExchange.Redis;

// connect to redis
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase database = redis.GetDatabase();

setGetStringRedis(database);

void setGetStringRedis(IDatabase db)
{
    db.StringSet("saber:key", "saber:value");
    Console.WriteLine(db.StringGet("saber:key"));
}
