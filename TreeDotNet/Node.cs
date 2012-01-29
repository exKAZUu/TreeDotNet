#region License

// Copyright (C) 2011-2012 The UNICOEN Project
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

namespace TreeDotNet {
	/// <summary>
	/// Instead of XElement.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Node<T> : INode<T> {
		private Node() {}

		public Node(T value) {
			Value = value;
			Previous = this;
			Next = this;
		}

		public Node<T> Child { get; private set; }
		public Node<T> Parent { get; private set; }
		public Node<T> Previous { get; private set; }
		public Node<T> Next { get; private set; }
		public T Value { get; set; }

		public IEnumerable<Node<T>> Children {
			get {
				var node = Child;
				while (node != null) {
					yield return node;
					node = node.Next;
				}
			}
		}

		public IEnumerable<Node<T>> Nexts {
			get {
				var node = Next;
				while (node != null) {
					yield return node;
					node = node.Next;
				}
			}
		}

		public IEnumerable<Node<T>> ReverseNexts {
			get {
				var terminal = this;
				var node = Parent.Child.Previous;
				while (node != terminal) {
					yield return node;
					node = node.Previous;
				}
			}
		}

		public IEnumerable<Node<T>> Previouses {
			get {
				var terminal = this;
				var node = Parent.Child;
				while (node != terminal) {
					yield return node;
					node = node.Next;
				}
			}
		}

		public IEnumerable<Node<T>> ReversePreviouses {
			get {
				var node = Previous;
				while (node != null) {
					yield return node;
					node = node.Previous;
				}
			}
		}

		public void AddFirst(Node<T> node) {
			Contract.Requires<ArgumentException>(
					node.Parent != null, "The specified node already has a parent node.");
			PrivateAddFirst(node);
		}

		public void AddFirst(T value) {
			PrivateAddFirst(new Node<T> { Value = value });
		}

		private void PrivateAddFirst(Node<T> node) {
			var second = Child;
			Child = node;
			node.Next = second;
			node.Previous = second.Previous;
			second.Previous.Next = node;
			second.Previous = node;
			node.Parent = this;
		}

		public void AddLast(Node<T> node) {
			Contract.Requires<ArgumentException>(
					node.Parent != null, "The specified node already has a parent node.");
			PrivateAddLast(node);
		}

		public void AddLast(T value) {
			PrivateAddLast(new Node<T> { Value = value });
		}

		private void PrivateAddLast(Node<T> node) {
			var head = Child.Previous;
			Child = node;
			node.Next = head;
			node.Parent = this;
		}
	}
}