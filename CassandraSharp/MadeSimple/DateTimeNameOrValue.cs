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

namespace CassandraSharp.MadeSimple
{
    using System;

    public class DateTimeNameOrValue : NameOrValueBase<DateTime>
    {
        public DateTimeNameOrValue(DateTime value)
            : base(value)
        {
        }

        public DateTimeNameOrValue(byte[] value)
            : base(value)
        {
        }

        public static INameOrValue FromNullable(DateTime? obj)
        {
            return obj.HasValue
                       ? new DateTimeNameOrValue(obj.Value)
                       : null;
        }

        public static INameOrValue FromBuffer(byte[] buffer)
        {
            return null != buffer
                       ? new DateTimeNameOrValue(buffer)
                       : null;
        }

        public override byte[] ToByteArray()
        {
            byte[] value = BitConverter.GetBytes(Value.Ticks);
            Array.Reverse(value);
            return value;
        }

        protected override DateTime FromByteArray(byte[] value)
        {
            byte[] buffer = new byte[value.Length];
            value.CopyTo(buffer, 0);
            Array.Reverse(buffer);
            long ticks = BitConverter.ToInt64(buffer, 0);
            return new DateTime(ticks);
        }
    }
}