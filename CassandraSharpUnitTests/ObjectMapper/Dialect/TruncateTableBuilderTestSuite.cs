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

namespace CassandraSharpUnitTests.ObjectMapper.Dialect
{
    using System;
    using CassandraSharp.ObjectMapper.Dialect;
    using NUnit.Framework;

    public class TruncateTableBuilderTestSuite<T> where T : IDialect, new()
    {
        private static ITruncateTableBuilder CreateTruncateTableBuilder()
        {
            T dialect = new T();
            ITruncateTableBuilder builder = dialect.GetTruncateTableBuilder();
            builder.Table = "TestTable";
            return builder;
        }

        [Test]
        public void TestAllParams()
        {
            const string expectedCql = "truncate TestTable";

            ITruncateTableBuilder builder = CreateTruncateTableBuilder();
            string cql = builder.Build();
            Assert.AreEqual(expectedCql, cql);
        }

        [Test]
        public void TestValidateTable()
        {
            ITruncateTableBuilder builder = CreateTruncateTableBuilder();
            builder.Table = null;
            Assert.Throws<ArgumentNullException>(() => builder.Build());
        }
    }
}