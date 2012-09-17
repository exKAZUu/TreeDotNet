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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace TreeDotNet {
	/// <summary>
	///   Instead of XElement.
	/// </summary>
	/// <typeparam name="T"> </typeparam>
	public class Node<T> : INode<T> {
		private Node() {}

		public Node(T value) {
			Value = value;
			Previous = this;
			Next = this;
		}

		public Node<T> FirstChild { get; private set; }
		public Node<T> Parent { get; private set; }
		public Node<T> Previous { get; private set; }
		public Node<T> Next { get; private set; }
		public T Value { get; set; }

		public Node<T> LastChild {
			get { return FirstChild == null ? null : FirstChild.Previous; }
		}

		public IEnumerable<Node<T>> Children {
			get {
				var node = FirstChild;
				if (node == null) {
					yield break;
				}
				var terminal = node;
				do {
					yield return node;
					node = node.Next;
				} while (node != terminal);
			}
		}

		public IEnumerable<Node<T>> Nexts {
			get {
				var node = Next;
				var terminal = Parent.FirstChild;
				while (node != terminal) {
					yield return node;
					node = node.Next;
				}
			}
		}

		public IEnumerable<Node<T>> NextsWithSelf {
			get {
				var node = this;
				var terminal = Parent.FirstChild;
				do {
					yield return node;
					node = node.Next;
				} while (node != terminal);
			}
		}

		public IEnumerable<Node<T>> ReverseNexts {
			get {
				var node = Parent.LastChild;
				var terminal = this;
				while (node != terminal) {
					yield return node;
					node = node.Previous;
				}
			}
		}

		public IEnumerable<Node<T>> ReverseNextsWithSelf {
			get {
				var node = Parent.FirstChild;
				var terminal = this;
				do {
					node = node.Previous;
					yield return node;
				} while (node != terminal);
			}
		}

		public IEnumerable<Node<T>> Previouses {
			get {
				var node = Parent.FirstChild;
				var terminal = this;
				while (node != terminal) {
					yield return node;
					node = node.Next;
				}
			}
		}

		public IEnumerable<Node<T>> PreviousesWithSelf {
			get {
				var node = Parent.LastChild;
				var terminal = this;
				do {
					node = node.Next;
					yield return node;
				} while (node != terminal);
			}
		}

		public IEnumerable<Node<T>> ReversePreviouses {
			get {
				var node = Previous;
				var terminal = Parent.LastChild;
				while (node != terminal) {
					yield return node;
					node = node.Previous;
				}
			}
		}

		public IEnumerable<Node<T>> ReversePreviousesWithSelf {
			get {
				var node = this;
				var terminal = Parent.LastChild;
				do {
					yield return node;
					node = node.Previous;
				} while (node != terminal);
			}
		}

		public Node<T> AddFirst(Node<T> node) {
			Contract.Requires<ArgumentException>(
					node.Parent == null, "The specified node already has a parent node.");
			return PrivateAddFirst(node);
		}

		public Node<T> AddFirst(T value) {
			return PrivateAddFirst(new Node<T> { Value = value });
		}

		private Node<T> PrivateAddFirst(Node<T> node) {
			PrivateAddLast(node);
			FirstChild = node;
			return node;
		}

		private void AddPreviousPrivate(Node<T> node) {
			node.Next = this;
			node.Previous = Previous;
			Previous.Next = node;
			Previous = node;
		}

		public Node<T> AddLast(Node<T> node) {
			Contract.Requires<ArgumentException>(
					node.Parent == null, "The specified node already has a parent node.");
			return PrivateAddLast(node);
		}

		public Node<T> AddLast(T value) {
			return PrivateAddLast(new Node<T> { Value = value });
		}

		private Node<T> PrivateAddLast(Node<T> node) {
			var second = FirstChild;
			node.Parent = this;
			if (second == null) {
				node.Next = node;
				node.Previous = node;
				FirstChild = node;
			} else {
				second.AddPreviousPrivate(node);
			}
			return node;
		}

		public override String ToString() {
			var builder = new StringBuilder();
			ToStringPrivate(this, 0, builder);
			return builder.ToString();
		}

		private static void ToStringPrivate(
				Node<T> node, int depth, StringBuilder builder) {
			if (node == null) {
				return;
			}
			for (int i = 0; i < depth; i++) {
				builder.Append("  ");
			}
			builder.AppendLine(node.Value.ToString());
			foreach (var child in node.Children) {
				ToStringPrivate(child, depth + 1, builder);
			}
		}
	}
}