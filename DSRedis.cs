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

        public void setAddRemove()
        {
            _database.KeyDelete("saber:set");
            _database.SetAdd("saber:set", "value1");
            _database.SetAdd("saber:set", "value2");
            _database.SetAdd("saber:set", "value3");
            _database.SetAdd("saber:set", "value4");
            _database.SetAdd("saber:set", "value5");
            _database.SetAdd("saber:set", "value6");

            RedisValue[] set = _database.SetMembers("saber:set");
            foreach (var item in set)
                Console.WriteLine(item);
            _database.SetRemove("saber:set", "value1");

            Console.WriteLine(_database.SetContains("saber:set", "value1"));

            RedisValue[] set1 = _database.SetMembers("saber:set");
            foreach (var item in set1)
                Console.WriteLine(item);

        }

        public void SortedSet()
        {
            _database.KeyDelete("saber:sortedset");
            _database.SortedSetAdd("saber:sortedset", "value1", 1);
            _database.SortedSetAdd("saber:sortedset", "value2", 2);
            _database.SortedSetAdd("saber:sortedset", "value3", 3);
            _database.SortedSetAdd("saber:sortedset", "value4", 4);
            _database.SortedSetAdd("saber:sortedset", "value5", 5);
            _database.SortedSetAdd("saber:sortedset", "value6", 6);

            RedisValue[] sortedSet = _database.SortedSetRangeByRank("saber:sortedset", 0, -1);
            foreach (var item in sortedSet)
                Console.WriteLine(item);
        }

        public void Hash()
        {
            _database.KeyDelete("saber:hash");
            _database.HashSet("saber:hash", new HashEntry[]
            {
                new HashEntry("key1", "value1"),
                new HashEntry("key2", "value2"),
                new HashEntry("key3", "value3"),
                new HashEntry("key4", "value4"),
                new HashEntry("key5", "value5"),
                new HashEntry("key6", "value6")
            });
            HashEntry[] hashEntries = _database.HashGetAll("saber:hash");
            foreach (var entry in hashEntries)
                Console.WriteLine($"{entry.Name}: {entry.Value}");
        }

        public void setUnion()
        {
            _database.KeyDelete("saber:set1");
            _database.KeyDelete("saber:set2");
            _database.KeyDelete("saber:set3");
            _database.SetAdd("saber:set1", "value1");
            _database.SetAdd("saber:set1", "value2");
            _database.SetAdd("saber:set1", "value3");
            _database.SetAdd("saber:set1", "value4");
            _database.SetAdd("saber:set1", "value5");
            _database.SetAdd("saber:set1", "value6");
            _database.SetAdd("saber:set2", "value4");
            _database.SetAdd("saber:set2", "value5");
            _database.SetAdd("saber:set2", "value6");
            _database.SetAdd("saber:set2", "value7");
            _database.SetAdd("saber:set2", "value8");
            _database.SetAdd("saber:set2", "value9");
            RedisValue[] set1 = _database.SetMembers("saber:set1");
            RedisValue[] set2 = _database.SetMembers("saber:set2");
            Console.WriteLine("------------ Set 1 ------------------- ");
            foreach (var item in set1)
                Console.WriteLine(item);
            Console.WriteLine("------------ Set 2 ------------------- ");
            foreach (var item in set2)
                Console.WriteLine(item);
            _database.SetCombineAndStore(SetOperation.Union, "saber:set3", "saber:set1", "saber:set2");
            RedisValue[] set3 = _database.SetMembers("saber:set3");
            Console.WriteLine("------------ Set 3 ------------------- ");
            foreach (var item in set3)
                Console.WriteLine(item);
        }

        public void ZADDSet()
        {
            _database.KeyDelete("saber:zaddset");
            _database.SortedSetAdd("saber:zaddset", "value1", 1);
            _database.SortedSetAdd("saber:zaddset", "value2", 2);
            _database.SortedSetAdd("saber:zaddset", "value3", 3);
            _database.SortedSetAdd("saber:zaddset", "value4", 4);
            _database.SortedSetAdd("saber:zaddset", "value5", 5);
            _database.SortedSetAdd("saber:zaddset", "value6", 6);

            _database.SortedSetAdd("saber:zaddset", new SortedSetEntry[]
            {
                new SortedSetEntry("value7", 7),
                new SortedSetEntry("value8", 8),
                new SortedSetEntry("value9", 9),
                new SortedSetEntry("value10", 10),
                new SortedSetEntry("value11", 11),
                new SortedSetEntry("value12", 12)
            });

            RedisValue[] zaddset = _database.SortedSetRangeByRank("saber:zaddset", 0, -1, Order.Descending);
            foreach (var item in zaddset)
                Console.WriteLine(item);

            Console.WriteLine(_database.SortedSetRank("saber:zaddset","value8"));
        }
    }
}