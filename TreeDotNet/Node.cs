#region License

// Copyright (C) 2011-2013 Kazunori Sakamoto
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
	/// Represents a node instead of XElement.
	/// </summary>
	/// <typeparam name="TNode">The type of this class.</typeparam>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	public class Node<TNode, T> : INode<T>
			where TNode : Node<TNode, T>, new() {
		/// <summary>
		/// Initialzies a new instance of the Node class with a default value.
		/// The Create static method should be used instead of the constructor.
		/// </summary>
		protected Node() {
			Previous = This;
			Next = This;
		}

		/// <summary>
		/// Initialzies a new instance of the Node class with a default value.
		/// </summary>
		/// <returns></returns>
		public static TNode Create() {
			return new TNode();
		}

		/// <summary>
		/// Initialzies a new instance of the Node class with the specified value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static TNode Create(T value) {
			return new TNode { Value = value };
		}

		/// <summary>
		/// The casted this instance for the simplicity.
		/// </summary>
		private TNode This {
			get { return (TNode)this; }
		}

		/// <summary>
		/// Gets the first sibling node or the current node.
		/// </summary>
		public TNode FirstSibling {
			get { return Parent != null ? Parent.FirstChild : This; }
		}

		/// <summary>
		/// Gets the last sibling node or the current node.
		/// </summary>
		public TNode LastSibling {
			get { return Parent != null ? Parent.FirstChild.Previous : This; }
		}

		/// <summary>
		/// Gets the first child node.
		/// </summary>
		public TNode FirstChild { get; private set; }

		/// <summary>
		/// Gets the parent node.
		/// </summary>
		public TNode Parent { get; private set; }

		/// <summary>
		/// Gets the previous node.
		/// </summary>
		public TNode Previous { get; private set; }

		/// <summary>
		/// Gets the next node.
		/// </summary>
		public TNode Next { get; private set; }

		/// <summary>
		/// Gets the value.
		/// </summary>
		public T Value { get; set; }

		public TNode LastChild {
			get { return FirstChild == null ? null : FirstChild.Previous; }
		}

		public IEnumerable<TNode> Children {
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

		public IEnumerable<TNode> Nexts {
			get {
				var node = Next;
				var terminal = FirstSibling;
				while (node != terminal) {
					yield return node;
					node = node.Next;
				}
			}
		}

		public IEnumerable<TNode> NextsWithSelf {
			get {
				var node = This;
				var terminal = FirstSibling;
				do {
					yield return node;
					node = node.Next;
				} while (node != terminal);
			}
		}

		public IEnumerable<TNode> ReverseNexts {
			get {
				var node = LastSibling;
				var terminal = This;
				while (node != terminal) {
					yield return node;
					node = node.Previous;
				}
			}
		}

		public IEnumerable<TNode> ReverseNextsWithSelf {
			get {
				var node = FirstSibling;
				var terminal = This;
				do {
					node = node.Previous;
					yield return node;
				} while (node != terminal);
			}
		}

		public IEnumerable<TNode> Previouses {
			get {
				var node = FirstSibling;
				var terminal = This;
				while (node != terminal) {
					yield return node;
					node = node.Next;
				}
			}
		}

		public IEnumerable<TNode> PreviousesWithSelf {
			get {
				var node = LastSibling;
				var terminal = This;
				do {
					node = node.Next;
					yield return node;
				} while (node != terminal);
			}
		}

		public IEnumerable<TNode> ReversePreviouses {
			get {
				var node = Previous;
				var terminal = LastSibling;
				while (node != terminal) {
					yield return node;
					node = node.Previous;
				}
			}
		}

		public IEnumerable<TNode> ReversePreviousesWithSelf {
			get {
				var node = This;
				var terminal = LastSibling;
				do {
					yield return node;
					node = node.Previous;
				} while (node != terminal);
			}
		}

		public TNode AddPrevious(TNode node) {
			Contract.Requires(node != null);
			Contract.Requires(node.Parent == null);
			Contract.Requires(Parent != null);
			if (Parent.FirstChild == This) {
				Parent.FirstChild = node;
			}
			return AddPreviousIgnoringFirstChild(node);
		}

		public TNode AddPrevious(T value) {
			Contract.Requires(Parent != null);
			var node = Create(value);
			if (Parent.FirstChild == This) {
				Parent.FirstChild = node;
			}
			return AddPreviousIgnoringFirstChild(node);
		}

		public TNode AddNext(TNode node) {
			Contract.Requires(node != null);
			Contract.Requires(node.Parent == null);
			Contract.Requires(Parent != null);
			return This.Next.AddPreviousIgnoringFirstChild(node);
		}

		public TNode AddNext(T value) {
			Contract.Requires(Parent != null);
			return This.Next.AddPreviousIgnoringFirstChild(Create(value));
		}

		public TNode AddFirst(TNode node) {
			Contract.Requires(node != null);
			Contract.Requires(node.Parent == null);
			return AddFirstPrivate(node);
		}

		public TNode AddFirst(T value) {
			return AddFirstPrivate(Create(value));
		}

		private TNode AddFirstPrivate(TNode node) {
			AddLastPrivate(node);
			FirstChild = node;
			return node;
		}

		private TNode AddPreviousIgnoringFirstChild(TNode node) {
			node.Parent = This.Parent;
			node.Next = This;
			node.Previous = Previous;
			Previous.Next = node;
			Previous = node;
			return node;
		}

		public TNode AddLast(TNode node) {
			Contract.Requires(node != null);
			Contract.Requires(node.Parent == null);
			return AddLastPrivate(node);
		}

		public TNode AddLast(T value) {
			return AddLastPrivate(Create(value));
		}

		private TNode AddLastPrivate(TNode node) {
			var second = FirstChild;
			if (second == null) {
				node.Parent = This;
				node.Next = node;
				node.Previous = node;
				FirstChild = node;
			} else {
				second.AddPreviousIgnoringFirstChild(node);
			}
			return node;
		}

		public override String ToString() {
			var builder = new StringBuilder();
			ToStringPrivate(This, 0, builder);
			return builder.ToString();
		}

		private static void ToStringPrivate(
				TNode node, int depth, StringBuilder builder) {
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