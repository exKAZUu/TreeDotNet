using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace TreeDotNet {
	/// <summary>
	///   A class which represents node in the <see cref="Tree"/> instead of XElement.
	/// </summary>
	/// <typeparam name="TNode"> </typeparam>
	/// <typeparam name="T"> </typeparam>
	public class Node<TNode, T> : INode<T>
			where TNode : Node<TNode, T>, new() {
		public Node() {
			Previous = This;
			Next = This;
		}

		private TNode This {
			get { return (TNode)this; }
		}

		public static TNode Create(T value) {
			return new TNode { Value = value };
		}

		public TNode FirstChild { get; private set; }
		public TNode Parent { get; private set; }
		public TNode Previous { get; private set; }
		public TNode Next { get; private set; }
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
				var terminal = Parent.FirstChild;
				while (node != terminal) {
					yield return node;
					node = node.Next;
				}
			}
		}

		public IEnumerable<TNode> NextsWithSelf {
			get {
				var node = This;
				var terminal = Parent.FirstChild;
				do {
					yield return node;
					node = node.Next;
				} while (node != terminal);
			}
		}

		public IEnumerable<TNode> ReverseNexts {
			get {
				var node = Parent.LastChild;
				var terminal = This;
				while (node != terminal) {
					yield return node;
					node = node.Previous;
				}
			}
		}

		public IEnumerable<TNode> ReverseNextsWithSelf {
			get {
				var node = Parent.FirstChild;
				var terminal = This;
				do {
					node = node.Previous;
					yield return node;
				} while (node != terminal);
			}
		}

		public IEnumerable<TNode> Previouses {
			get {
				var node = Parent.FirstChild;
				var terminal = This;
				while (node != terminal) {
					yield return node;
					node = node.Next;
				}
			}
		}

		public IEnumerable<TNode> PreviousesWithSelf {
			get {
				var node = Parent.LastChild;
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
				var terminal = Parent.LastChild;
				while (node != terminal) {
					yield return node;
					node = node.Previous;
				}
			}
		}

		public IEnumerable<TNode> ReversePreviousesWithSelf {
			get {
				var node = This;
				var terminal = Parent.LastChild;
				do {
					yield return node;
					node = node.Previous;
				} while (node != terminal);
			}
		}

		public TNode AddFirst(TNode node) {
			Contract.Requires(node != null);
			Contract.Requires(node.Parent == null);
			return PrivateAddFirst(node);
		}

		public TNode AddFirst(T value) {
			return PrivateAddFirst(Create(value));
		}

		private TNode PrivateAddFirst(TNode node) {
			PrivateAddLast(node);
			FirstChild = node;
			return node;
		}

		private void AddPreviousPrivate(TNode node) {
			node.Next = This;
			node.Previous = Previous;
			Previous.Next = node;
			Previous = node;
		}

		public TNode AddLast(TNode node) {
			Contract.Requires(node != null);
			Contract.Requires(node.Parent == null);
			return PrivateAddLast(node);
		}

		public TNode AddLast(T value) {
			return PrivateAddLast(Create(value));
		}

		private TNode PrivateAddLast(TNode node) {
			var second = FirstChild;
			node.Parent = This;
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