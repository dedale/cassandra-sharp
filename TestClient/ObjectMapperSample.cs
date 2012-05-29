﻿// cassandra-sharp - a .NET client for Apache Cassandra
// Copyright (c) 2011-2012 Pierre Chalamet
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace TestClient
{
    using System;
    using System.Collections.Generic;
    using Apache.Cassandra;
    using CassandraSharp;
    using CassandraSharp.ObjectMapper;

    [Schema("Twissandra", Name = "users", Comment = "Users family")]
    public class Users
    {
        [Column(Name = "bio")]
        public string Bio;

        [Column(Name = "createdAt")]
        public DateTime CreatedAt;

        [Column(Name = "displayName")]
        public string DisplayName;

        [CompositeKey(Name = "location", Index = 1)]
        public string Location;

        [Key(Name = "name")]
        public string Name;

        [Column(Name = "password")]
        public string Password;

        [Column(Name = "webUrl")]
        public string Web;
    }

    [Schema("Twissandra", Comment = "Tweets family")]
    public class Tweets
    {
        [Key]
        public Guid InReplyToTweet;

        [Column]
        public string InReplyToUser;

        [Column]
        public string Location;

        [Column]
        public string Text;

        [Column]
        public DateTime Time;

        [Column]
        public Guid TweedId;

        [Column]
        public string User;
    }

    public class ObjectMapperSample
    {
        private readonly string _configName;

        public ObjectMapperSample(string configName)
        {
            _configName = configName;
        }

        public void Run()
        {
            using (ICluster cluster = ClusterManager.GetCluster(_configName))
                Run(cluster);
        }

        private void CreateKeyspace(Cassandra.Client client)
        {
            //client.set_keyspace("system");
            try
            {
                client.system_drop_keyspace("Twissandra");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.InnerException);
            }

            KsDef ksDef = new KsDef
                              {
                                  Name = "Twissandra",
                                  Strategy_class = "SimpleStrategy",
                                  Cf_defs = new List<CfDef>(),
                                  Strategy_options = new Dictionary<string, string>()
                              };
            ksDef.Strategy_options["replication_factor"] = "1";

            client.system_add_keyspace(ksDef);

            //client.set_keyspace("twissandra");
        }

        protected void Run(ICluster cluster)
        {
            cluster.Execute(x => CreateKeyspace(x.CassandraClient));

            cluster.CreateTable<Users>();
            cluster.CreateTable<Tweets>();

            cluster.Insert<Users>(new {Name = "User1", DisplayName = "RealName1", Location = "SF"});
            cluster.Insert<Users>(new {Name = "User2", DisplayName = "RealName12", Location = "NY"});
            cluster.Insert<Users>(new {Name = "User3", DisplayName = "RealName13", Location = "SF"});
            cluster.Insert<Users>(new {Name = "User4", DisplayName = "RealName14", Location = "HK"});

            IEnumerable<Users> users = cluster.Select<Users>(new {Location = "SF"});
            foreach (Users user in users)
            {
                Console.WriteLine("{0}: {1}", user.Name, user.Location);
            }

            cluster.DropTable<Tweets>();
            cluster.DropTable<Users>();
        }
    }
}