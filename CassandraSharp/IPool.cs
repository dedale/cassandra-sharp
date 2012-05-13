﻿// Licensed under the Apache License, Version 2.0 (the "License");
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

namespace CassandraSharp
{
    using System;

    /// <summary>
    ///     Pool caches object according to its policy A pool implementation must be thread safe
    /// </summary>
    /// <typeparam name="T"> Type of entries </typeparam>
    internal interface IPool<T> : IDisposable where T : IDisposable
    {
        bool Acquire(out T entry);

        void Release(T entry);
    }
}