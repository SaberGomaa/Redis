using StackExchange.Redis;

namespace Redis
{
    public class DSRedis
    {
        private IDatabase _database;
        public DSRedis(IDatabase database)
        {
            _database = database;
        }

        public void ListPushPop()
        {
            _database.KeyDelete("saber:list");
            _database.ListLeftPush("saber:list", "value1");
            _database.ListLeftPush("saber:list", "value2");
            _database.ListLeftPush("saber:list", "value3");

            for (int i = 0; i < 5; i++)
                _database.ListRightPush("saber:list", i + 1);

            Console.WriteLine("item of index 5 = " + _database.ListGetByIndex("saber:list", 5));

            Console.WriteLine(_database.ListRightPop("saber:list"));
            Console.WriteLine(_database.ListLength("saber:list"));

            RedisValue[] list = _database.ListRange("saber:list", 0, -1);

            foreach (var item in list)
                Console.WriteLine(item);
        }

        public void listMove()
        {
            _database.KeyDelete("saber:list1");
            _database.KeyDelete("saber:list2");
            _database.ListLeftPush("saber:list1", "value1");
            _database.ListLeftPush("saber:list1", "value2");
            _database.ListLeftPush("saber:list1", "value3");
            _database.ListLeftPush("saber:list2", "value4");
            _database.ListLeftPush("saber:list2", "value5");
            _database.ListLeftPush("saber:list2", "value6");
            _database.ListRightPopLeftPush("saber:list1", "saber:list2");
            RedisValue[] list1 = _database.ListRange("saber:list1", 0, -1);
            RedisValue[] list2 = _database.ListRange("saber:list2", 0, -1);

            Console.WriteLine("------------ List 1 ------------------- ");


            foreach (var item in list1)
                Console.WriteLine(item);

            Console.WriteLine("------------ List 2 ------------------- ");

            foreach (var item in list2)
                Console.WriteLine(item);
        }

    }
}
