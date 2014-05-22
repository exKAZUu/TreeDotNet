#region License

// Copyright (C) 2011-2014 Kazunori Sakamoto
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TreeDotNet.Tests {
    [TestFixture]
    public class PerformanceTest {
        [Test]
        public void RepeatAndConcat() {
            var a = new StringNode("a"); // 1
            var b = a.AddFirst(new StringNode("b")); // 2
            var c = a.AddLast(new StringNode("c")); // 2
            var d = a.AddFirst(new StringNode("d")); // 2
            var e = a.AddFirst(new StringNode("e")); // 2
            var f = b.AddFirst(new StringNode("f")); // 3
            var g = b.AddFirst(new StringNode("g")); // 3
            var h = g.AddLast("h"); // 4
            var i = f.AddLast("i"); // 4
            var j = h.AddNext("j"); // 4
            var k = h.AddPrevious("k"); // 4
            var l = i.AddPrevious("l"); // 4
            var m = i.AddNext("m"); // 4

            var tickCount = Environment.TickCount;
            for (int n = 0; n < 1000 * 1000 * 10; n++) {
                a.DescendantsAndSelf().ToList();
            }
            Console.WriteLine("DescendantsAndSelf w/  LINQ: " + (Environment.TickCount - tickCount));

            tickCount = Environment.TickCount;
            for (int n = 0; n < 1000 * 1000 * 10; n++) {
                a.DescendantsAndSelfWithoutLinq().ToList();
            }
            Console.WriteLine("DescendantsAndSelf w/o LINQ: "
                              + (Environment.TickCount - tickCount));
        }
    }

    internal static class Extension {
        public static IEnumerable<TNode> DescendantsAndSelfWithoutLinq<TNode, T>(
                this Node<TNode, T> self)
                where TNode : Node<TNode, T> {
            var start = (TNode)self;
            yield return start;
            var cursor = start;
            if (cursor.FirstChild != null) {
                cursor = cursor.FirstChild;
                yield return cursor;
                while (true) {
                    while (cursor.FirstChild != null) {
                        cursor = cursor.FirstChild;
                        yield return cursor;
                    }
                    while (cursor.Next == null) {
                        cursor = cursor.Parent;
                        if (cursor == start) {
                            yield break;
                        }
                    }
                    cursor = cursor.CyclicNext;
                    yield return cursor;
                }
            }
        }
    }
}