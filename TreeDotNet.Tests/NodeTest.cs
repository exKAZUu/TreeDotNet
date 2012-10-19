#region License

// Copyright (C) 2011-2012 Kazunori Sakamoto
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
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TreeDotNet.Tests {
	public static class StringExtensionForTest {
		public static string NormalizeNewLine(this string text) {
			return text.Replace("\n", Environment.NewLine);
		}
	}

	[TestFixture]
	public class NodeTest {
		[Test]
		public void OperateRoot() {
			var root = Nodes.Create("a");
			root.Previouses.Should().HaveCount(0);
			root.Nexts.Should().HaveCount(0);
			root.PreviousesWithSelf.Should().Equal(Enumerable.Repeat(root, 1));
			root.NextsWithSelf.Should().Equal(Enumerable.Repeat(root, 1));
			root.ReversePreviouses.Should().HaveCount(0);
			root.ReverseNexts.Should().HaveCount(0);
			root.ReversePreviousesWithSelf.Should().Equal(Enumerable.Repeat(root, 1));
			root.ReverseNextsWithSelf.Should().Equal(Enumerable.Repeat(root, 1));
		}

		[Test]
		public void Create1Node() {
			var node = Nodes.Create("a");
			node.ToString().Should().Be("a\n".NormalizeNewLine());
		}

		[Test]
		public void Create2Nodes() {
			var node = Nodes.Create("a");
			node.AddFirst(Nodes.Create("b"));
			node.ToString().Should().Be("a\n  b\n".NormalizeNewLine());
		}

		[Test]
		public void Create3Nodes() {
			var node = Nodes.Create("a");
			node.AddLast(Nodes.Create("b"));
			node.AddFirst(Nodes.Create("c"));
			node.ToString().Should().Be("a\n  c\n  b\n".NormalizeNewLine());
		}

		[Test]
		public void Create4Nodes() {
			var node = Nodes.Create("a");
			node.AddLast(Nodes.Create("b"));
			node.AddFirst(Nodes.Create("c"));
			node.AddLast(Nodes.Create("d"));
			node.ToString().Should().Be("a\n  c\n  b\n  d\n".NormalizeNewLine());
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
			var e1 = d1.AddLast("h");
			var e2 = d2.AddLast("i");
			var f1 = e1.AddNext("j");
			var f2 = e1.AddPrevious("k");
			var g1 = e2.AddPrevious("l");
			var g2 = e2.AddNext("m");
			node.ToString()
			    .Should()
			    .Be(
					    "a\n  e\n  d\n  b\n    g\n      k\n      h\n      j\n    f\n      l\n      i\n      m\n  c\n"
							    .NormalizeNewLine());

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