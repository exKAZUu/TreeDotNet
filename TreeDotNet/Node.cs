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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace TreeDotNet {
	/// <summary>
	/// Represents a node instead of XElement.
	/// </summary>
	/// <typeparam name="TNode">The type of this class.</typeparam>
	/// <typeparam name="TValue">The type of elements in the list.</typeparam>
	public class Node<TNode, TValue>
			where TNode : Node<TNode, TValue> {
		/// <summary>
		/// Initializes a new instance of the Node class with a default value.
		/// </summary>
		protected Node() {
			CyclicPrev = ThisNode;
			CyclicNext = ThisNode;
		}

		/// <summary>
		/// Initializes a new instance of the Node class with the specified value.
		/// </summary>
		protected Node(TValue value) {
			CyclicPrev = ThisNode;
			CyclicNext = ThisNode;
			Value = value;
		}

		/// <summary>
		/// The casted this instance for the simplicity.
		/// </summary>
		private TNode ThisNode {
			get { return (TNode)this; }
		}

		/// <summary>
		/// Gets the first sibling node or the current node.
		/// </summary>
		public TNode FirstSibling {
			get { return Parent != null ? Parent.FirstChild : ThisNode; }
		}

		/// <summary>
		/// Gets the last sibling node or the current node.
		/// </summary>
		public TNode LastSibling {
			get { return Parent != null ? Parent.FirstChild.CyclicPrev : ThisNode; }
		}

		/// <summary>
		/// Gets the first child node.
		/// </summary>
		public TNode FirstChild { get; private set; }

		/// <summary>
		/// Gets the last child node.
		/// </summary>
		public TNode LastChild {
			get { return FirstChild != null ? FirstChild.CyclicPrev : null; }
		}

		/// <summary>
		/// Gets the parent node.
		/// </summary>
		public TNode Parent { get; private set; }

		/// <summary>
		/// Gets the previous node.
		/// </summary>
		public TNode CyclicPrev { get; private set; }

		/// <summary>
		/// Gets the next node.
		/// </summary>
		public TNode CyclicNext { get; private set; }

		/// <summary>
		/// Gets the previous node or null.
		/// </summary>
		public TNode Prev {
			get { return CyclicPrev != LastSibling ? CyclicPrev : null; }
		}

		/// <summary>
		/// Gets the next node or null.
		/// </summary>
		public TNode Next {
			get { return CyclicNext != FirstSibling ? CyclicNext : null; }
		}

		/// <summary>
		/// Gets and sets the value.
		/// </summary>
		protected TValue Value { get; set; }

		/// <summary>
		/// Gets the number of children nodes.
		/// </summary>
		public int ChildrenCount {
			get { return Children().Count(); }
		}

		/// <summary>
		/// Gets the length from the deepest child node.
		/// </summary>
		public int LengthFromDeepestChild {
			get { return GetLengthFromDeepestChild(); }
		}

		private int GetLengthFromDeepestChild() {
			var maxLength = 0;
			foreach (var child in Children()) {
				var length = child.GetLengthFromDeepestChild() + 1;
				if (maxLength < length) {
					maxLength = length;
				}
			}
			return maxLength;
		}

		#region Traversal

		public TNode ChildAtOrNull(int index) {
			return Children().ElementAtOrDefault(index);
		}

		public IEnumerable<TNode> Ancestors() {
			return AncestorsAndSelf().Skip(1);
		}

		public IEnumerable<TNode> AncestorsAndSelf() {
			var node = ThisNode;
			do {
				yield return node;
				node = node.Parent;
			} while (node != null);
		}

		public IEnumerable<TNode> Children() {
			var node = FirstChild;
			if (node == null) {
				yield break;
			}
			var terminal = node;
			do {
				yield return node;
				node = node.CyclicNext;
			} while (node != terminal);
		}

		public IEnumerable<TNode> ReverseChildren() {
			var node = LastChild;
			if (node == null) {
				yield break;
			}
			var terminal = node;
			do {
				yield return node;
				node = node.CyclicPrev;
			} while (node != terminal);
		}

		public IEnumerable<TNode> NextsFromSelf() {
			var node = CyclicNext;
			var terminal = FirstSibling;
			while (node != terminal) {
				yield return node;
				node = node.CyclicNext;
			}
		}

		public IEnumerable<TNode> NextsFromSelfAndSelf() {
			return Enumerable.Repeat(ThisNode, 1).Concat(NextsFromSelf());
		}

		public IEnumerable<TNode> NextsFromLast() {
			var node = LastSibling;
			var terminal = ThisNode;
			while (node != terminal) {
				yield return node;
				node = node.CyclicPrev;
			}
		}

		public IEnumerable<TNode> NextsFromLastAndSelf() {
			return NextsFromLast().Concat(Enumerable.Repeat(ThisNode, 1));
		}

		public IEnumerable<TNode> PrevsFromFirst() {
			var node = FirstSibling;
			var terminal = ThisNode;
			while (node != terminal) {
				yield return node;
				node = node.CyclicNext;
			}
		}

		public IEnumerable<TNode> PrevsFromFirstAndSelf() {
			return PrevsFromFirst().Concat(Enumerable.Repeat(ThisNode, 1));
		}

		public IEnumerable<TNode> PrevsFromSelf() {
			var node = CyclicPrev;
			var terminal = LastSibling;
			while (node != terminal) {
				yield return node;
				node = node.CyclicPrev;
			}
		}

		public IEnumerable<TNode> PrevsFromSelfAndSelf() {
			return Enumerable.Repeat(ThisNode, 1).Concat(PrevsFromSelf());
		}

		public IEnumerable<TNode> Descendants() {
			var start = ThisNode;
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

		public IEnumerable<TNode> DescendantsAndSelf() {
			return Enumerable.Repeat(ThisNode, 1).Concat(Descendants());
		}

		public IEnumerable<TNode> Siblings() {
			var first = FirstSibling;
			var node = first;
			while (node != this) {
				yield return node;
				node = node.CyclicNext;
			}
			node = node.CyclicNext;
			while (node != first) {
				yield return node;
				node = node.CyclicNext;
			}
		}

		public IEnumerable<TNode> SiblingsAndSelf() {
			var first = FirstSibling;
			var node = first;
			do {
				yield return node;
				node = node.CyclicNext;
			} while (node != first);
		}

		public IEnumerable<TNode> AncestorsAndSiblingsAfterSelf() {
			var node = ThisNode;
			do {
				foreach (var e in node.NextsFromSelf()) {
					yield return e;
				}
				node = node.Parent;
			} while (node != null);
		}

		public IEnumerable<TNode> AncestorsAndSiblingsAfterSelfAndSelf() {
			return Enumerable.Repeat(ThisNode, 1).Concat(AncestorsAndSiblingsAfterSelf());
		}

		public IEnumerable<TNode> AncestorsAndSiblingsBeforeSelf() {
			return AncestorsAndSiblingsBeforeSelfAndSelf().Skip(1);
		}

		public IEnumerable<TNode> AncestorsAndSiblingsBeforeSelfAndSelf() {
			var node = ThisNode;
			do {
				foreach (var e in node.PrevsFromSelfAndSelf()) {
					yield return e;
				}
				node = node.Parent;
			} while (node != null);
		}

		public IEnumerable<TNode> AncestorsWithSingleChild() {
			var node = ThisNode;
			if (node == node.CyclicNext) {
				do {
					node = node.Parent;
					yield return node;
				} while (node != null && node == node.CyclicNext);
			}
		}

		public IEnumerable<TNode> AncestorsWithSingleChildAndSelf() {
			var node = ThisNode;
			yield return node;
			if (node == node.CyclicNext) {
				do {
					node = node.Parent;
					yield return node;
				} while (node != null && node == node.CyclicNext);
			}
		}

		public IEnumerable<TNode> DescendantsOfSingle() {
			return DescendantsOfSingleAndSelf().Skip(1);
		}

		public IEnumerable<TNode> DescendantsOfSingleAndSelf() {
			var node = ThisNode;
			do {
				yield return node;
				node = node.FirstChild;
			} while (node != null && node == node.CyclicNext);
		}

		public IEnumerable<TNode> DescendantsOfFirstChild() {
			return DescendantsOfFirstChildAndSelf().Skip(1);
		}

		public IEnumerable<TNode> DescendantsOfFirstChildAndSelf() {
			var node = ThisNode;
			do {
				yield return node;
				node = node.FirstChild;
			} while (node != null);
		}

		public IEnumerable<TNode> Ancestors(int inclusiveDepth) {
			return Ancestors().Take(inclusiveDepth);
		}

		public IEnumerable<TNode> AncestorsAndSelf(int inclusiveDepth) {
			return AncestorsAndSelf().Take(inclusiveDepth + 1);
		}

		public IEnumerable<TNode> Descendants(int inclusiveDepth) {
			var start = ThisNode;
			var cursor = start;
			if (cursor.FirstChild != null && inclusiveDepth > 0) {
				cursor = cursor.FirstChild;
				inclusiveDepth--;
				yield return cursor;
				while (true) {
					while (cursor.FirstChild != null && inclusiveDepth > 0) {
						cursor = cursor.FirstChild;
						inclusiveDepth--;
						yield return cursor;
					}
					while (cursor.Next == null) {
						cursor = cursor.Parent;
						inclusiveDepth++;
						if (cursor == start) {
							yield break;
						}
					}
					cursor = cursor.CyclicNext;
					yield return cursor;
				}
			}
		}

		public IEnumerable<TNode> DescendantsAndSelf(int inclusiveDepth) {
			return Enumerable.Repeat(ThisNode, 1).Concat(Descendants(inclusiveDepth));
		}

		public IEnumerable<TNode> Siblings(int inclusiveEachLength) {
			return PrevsFromSelf().Take(inclusiveEachLength).Reverse()
					.Concat(NextsFromSelf().Take(inclusiveEachLength));
		}

		public IEnumerable<TNode> SiblingsAndSelf(int inclusiveEachLength) {
			return PrevsFromSelf().Take(inclusiveEachLength).Reverse()
					.Concat(Enumerable.Repeat(ThisNode, 1))
					.Concat(NextsFromSelf().Take(inclusiveEachLength));
		}

		#endregion

		public TNode AddPrevious(TNode node) {
			Contract.Requires(node != null);
			Contract.Requires(node.Parent == null);
			Contract.Requires(Parent != null);
			if (Parent.FirstChild == this) {
				Parent.FirstChild = node;
			}
			return AddPreviousIgnoringFirstChild(node);
		}

		public TNode AddNext(TNode node) {
			Contract.Requires(node != null);
			Contract.Requires(node.Parent == null);
			Contract.Requires(Parent != null);
			return CyclicNext.AddPreviousIgnoringFirstChild(node);
		}

		public TNode AddFirst(TNode node) {
			Contract.Requires(node != null);
			Contract.Requires(node.Parent == null);
			return AddFirstPrivate(node);
		}

		private TNode AddFirstPrivate(TNode node) {
			AddLastPrivate(node);
			FirstChild = node;
			return node;
		}

		private TNode AddPreviousIgnoringFirstChild(TNode node) {
			node.Parent = Parent;
			node.CyclicNext = ThisNode;
			node.CyclicPrev = CyclicPrev;
			CyclicPrev.CyclicNext = node;
			CyclicPrev = node;
			return node;
		}

		public TNode AddLast(TNode node) {
			Contract.Requires(node != null);
			Contract.Requires(node.Parent == null);
			return AddLastPrivate(node);
		}

		private TNode AddLastPrivate(TNode node) {
			var second = FirstChild;
			if (second == null) {
				node.Parent = ThisNode;
				node.CyclicNext = node;
				node.CyclicPrev = node;
				FirstChild = node;
			} else {
				second.AddPreviousIgnoringFirstChild(node);
			}
			return node;
		}

		/// <summary>
		/// Replace this node with the specified node.
		/// </summary>
		public void Replace(TNode newNode) {
			if (Parent == null) {
				throw new InvalidOperationException("A root node cannot be replaced.");
			}
			newNode.Parent = Parent;
			newNode.CyclicNext = CyclicNext;
			newNode.CyclicPrev = CyclicPrev;
			CyclicPrev.CyclicNext = newNode;
			CyclicNext.CyclicPrev = newNode;
			if (Parent.FirstChild == this) {
				Parent.FirstChild = newNode;
			}
			CyclicNext = null;
			CyclicPrev = null;
			Parent = null;
		}

		/// <summary>
		/// Remove this node.
		/// </summary>
		public void Remove() {
			if (Parent == null) {
				throw new InvalidOperationException("A root node cannot be removed.");
			}
			var next = CyclicNext;
			if (next != this) {
				CyclicPrev.CyclicNext = next;
				next.CyclicPrev = CyclicPrev;
				if (Parent.FirstChild == this) {
					Parent.FirstChild = next;
				}
			} else {
				Parent.FirstChild = null;
			}
			CyclicNext = null;
			CyclicPrev = null;
			Parent = null;
		}

		/// <summary>
		/// Returns the action that add this node to restore the original tree removing this node.
		/// </summary>
		/// <returns>The action that add this node to restore the original tree</returns>
		public Action RemoveRecoverably() {
			if (Parent == null) {
				throw new InvalidOperationException("A root node cannot be removed.");
			}
			var next = CyclicNext;
			if (next != this) {
				CyclicPrev.CyclicNext = next;
				next.CyclicPrev = CyclicPrev;
				if (Parent.FirstChild == this) {
					Parent.FirstChild = next;
					return () => {
						next.Parent.FirstChild = ThisNode;
						CyclicPrev.CyclicNext = ThisNode;
						next.CyclicPrev = ThisNode;
					};
				}
				return () => {
					CyclicPrev.CyclicNext = ThisNode;
					next.CyclicPrev = ThisNode;
				};
			}
			var parent = Parent;
			parent.FirstChild = null;
			return () => { parent.FirstChild = ThisNode; };
		}

		public override String ToString() {
			var builder = new StringBuilder();
			ToStringPrivate(ThisNode, 0, builder);
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
			builder.AppendLine(!Equals(node.Value, null) ? node.Value.ToString() : "");
			foreach (var child in node.Children()) {
				ToStringPrivate(child, depth + 1, builder);
			}
		}
	}
}