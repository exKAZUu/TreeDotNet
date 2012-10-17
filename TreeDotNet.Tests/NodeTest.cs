#region License

// Copyright (C) 2011-2012 The Unicoen Project
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
using NUnit.Framework;

namespace TreeDotNet.Tests {
	[TestFixture]
	public class NodeTest {
		[Test]
		public void Create1Node() {
			Assert.That(Nodes.Create("a").ToString(), Is.EqualTo("a\r\n"));
		}

		[Test]
		public void Create2Nodes() {
			var node = Nodes.Create("a");
			node.AddFirst(Nodes.Create("b"));
			Assert.That(node.ToString(), Is.EqualTo("a\r\n  b\r\n"));
		}

		[Test]
		public void Create3Nodes() {
			var node = Nodes.Create("a");
			node.AddLast(Nodes.Create("b"));
			node.AddFirst(Nodes.Create("c"));
			Assert.That(node.ToString(), Is.EqualTo("a\r\n  c\r\n  b\r\n"));
		}

		[Test]
		public void Create4Nodes() {
			var node = Nodes.Create("a");
			node.AddLast(Nodes.Create("b"));
			node.AddFirst(Nodes.Create("c"));
			node.AddLast(Nodes.Create("d"));
			Assert.That(node.ToString(), Is.EqualTo("a\r\n  c\r\n  b\r\n  d\r\n"));
		}

		[Test]
		public void CreateTreeAndTraverse() {
			var node = Nodes.Create("a");
			var c3 = node.AddFirst(Nodes.Create("b"));
			var c4 = node.AddLast(Nodes.Create("c"));
			var c2 = node.AddFirst(Nodes.Create("d"));
			var c1 = node.AddFirst(Nodes.Create("e"));
			var d2 = c3.AddFirst(Nodes.Create("f"));
			var d1 = c3.AddFirst(Nodes.Create("g"));
			Assert.That(
					node.ToString(),
					Is.EqualTo("a\r\n  e\r\n  d\r\n  b\r\n    g\r\n    f\r\n  c\r\n"));

			Assert.That(c3.Children, Is.EqualTo(new[] { d1, d2 }));
			Assert.That(c3.Nexts, Is.EqualTo(new[] { c4 }));
			Assert.That(c3.ReverseNexts, Is.EqualTo(new[] { c4 }));
			Assert.That(c3.NextsWithSelf, Is.EqualTo(new[] { c3, c4 }));
			Assert.That(c3.ReverseNextsWithSelf, Is.EqualTo(new[] { c4, c3 }));
			Assert.That(c3.Previouses, Is.EqualTo(new[] { c1, c2 }));
			Assert.That(c3.PreviousesWithSelf, Is.EqualTo(new[] { c1, c2, c3 }));
			Assert.That(c3.ReversePreviouses, Is.EqualTo(new[] { c2, c1 }));
			Assert.That(c3.ReversePreviousesWithSelf, Is.EqualTo(new[] { c3, c2, c1 }));

			Assert.That(c1.Children, Is.EqualTo(new Tree<String>[0]));
			Assert.That(c1.Nexts, Is.EqualTo(new[] { c2, c3, c4 }));
			Assert.That(c1.ReverseNexts, Is.EqualTo(new[] { c4, c3, c2 }));
			Assert.That(c1.NextsWithSelf, Is.EqualTo(new[] { c1, c2, c3, c4 }));
			Assert.That(c1.ReverseNextsWithSelf, Is.EqualTo(new[] { c4, c3, c2, c1 }));
			Assert.That(c1.Previouses, Is.EqualTo(new Tree<String>[0]));
			Assert.That(c1.PreviousesWithSelf, Is.EqualTo(new[] { c1 }));
			Assert.That(c1.ReversePreviouses, Is.EqualTo(new Tree<String>[0]));
			Assert.That(c1.ReversePreviousesWithSelf, Is.EqualTo(new[] { c1 }));
		}
	}
}