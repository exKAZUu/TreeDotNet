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
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class Node<TNode, T>
            where TNode : Node<TNode, T> {
        /// <summary>
        /// Initialzies a new instance of the Node class with a default value.
        /// </summary>
        protected Node() {
            CyclicPrev = This;
            CyclicNext = This;
        }

        /// <summary>
        /// Initialzies a new instance of the Node class with the specified value.
        /// </summary>
        protected Node(T value) {
            CyclicPrev = This;
            CyclicNext = This;
            Value = value;
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
            get { return Parent != null ? Parent.FirstChild.CyclicPrev : This; }
        }

        /// <summary>
        /// Gets the first child node.
        /// </summary>
        public TNode FirstChild { get; private set; }

        /// <summary>
        /// Gets the last child node.
        /// </summary>
        public TNode LastChild {
            get { return FirstChild == null ? null : FirstChild.CyclicPrev; }
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
        protected T Value { get; set; }

        /// <summary>
        /// Gets the number of children nodes.
        /// </summary>
        public int ChildrenCount {
            get { return Children().Count(); }
        }

        #region Traversal

        public IEnumerable<TNode> Ancestors() {
            return AncestorsAndSelf().Skip(1);
        }

        public IEnumerable<TNode> AncestorsAndSelf() {
            var node = This;
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

        public IEnumerable<TNode> ChildrenAndSelf() {
            return Enumerable.Repeat(This, 1).Concat(Children());
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
            return Enumerable.Repeat(This, 1).Concat(NextsFromSelf());
        }

        public IEnumerable<TNode> NextsFromLast() {
            var node = LastSibling;
            var terminal = This;
            while (node != terminal) {
                yield return node;
                node = node.CyclicPrev;
            }
        }

        public IEnumerable<TNode> NextsFromLastAndSelf() {
            return NextsFromLast().Concat(Enumerable.Repeat(This, 1));
        }

        public IEnumerable<TNode> PrevsFromFirst() {
            var node = FirstSibling;
            var terminal = This;
            while (node != terminal) {
                yield return node;
                node = node.CyclicNext;
            }
        }

        public IEnumerable<TNode> PrevsFromFirstAndSelf() {
            return PrevsFromFirst().Concat(Enumerable.Repeat(This, 1));
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
            return Enumerable.Repeat(This, 1).Concat(PrevsFromSelf());
        }

        public IEnumerable<TNode> Descendants() {
            var start = This;
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
            return Enumerable.Repeat(This, 1).Concat(Descendants());
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
            var node = This;
            do {
                foreach (var e in node.NextsFromSelf()) {
                    yield return e;
                }
                node = node.Parent;
            } while (node != null);
        }

        public IEnumerable<TNode> AncestorsAndSiblingsAfterSelfAndSelf() {
            return Enumerable.Repeat(This, 1).Concat(AncestorsAndSiblingsAfterSelf());
        }

        public IEnumerable<TNode> AncestorsAndSiblingsBeforeSelf() {
            return AncestorsAndSiblingsBeforeSelfAndSelf().Skip(1);
        }

        public IEnumerable<TNode> AncestorsAndSiblingsBeforeSelfAndSelf() {
            var node = This;
            do {
                foreach (var e in node.PrevsFromSelfAndSelf()) {
                    yield return e;
                }
                node = node.Parent;
            } while (node != null);
        }

        public IEnumerable<TNode> AncestorsOfSingle() {
            return AncestorsOfSingleAndSelf().Skip(1);
        }

        public IEnumerable<TNode> AncestorsOfSingleAndSelf() {
            var node = This;
            do {
                yield return node;
                node = node.Parent;
            } while (node != null && node == node.CyclicNext);
        }

        public IEnumerable<TNode> DescendantsOfSingle() {
            return DescendantsOfSingleAndSelf().Skip(1);
        }

        public IEnumerable<TNode> DescendantsOfSingleAndSelf() {
            var node = This;
            do {
                yield return node;
                node = node.FirstChild;
            } while (node != null && node == node.CyclicNext);
        }

        public IEnumerable<TNode> DescendantsOfFirstChild() {
            return DescendantsOfFirstChildAndSelf().Skip(1);
        }

        public IEnumerable<TNode> DescendantsOfFirstChildAndSelf() {
            var node = This;
            do {
                yield return node;
                node = node.FirstChild;
            } while (node != null);
        }

        public IEnumerable<TNode> Ancestors(int inclusiveDepth) {
            return AncestorsAndSelf(inclusiveDepth).Skip(1);
        }

        public IEnumerable<TNode> AncestorsAndSelf(int inclusiveDepth) {
            return AncestorsAndSelf().Take(inclusiveDepth + 1);
        }

        public IEnumerable<TNode> Descendants(int inclusiveDepth) {
            var start = This;
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
            return Enumerable.Repeat(This, 1).Concat(Descendants(inclusiveDepth));
        }

        public IEnumerable<TNode> Siblings(int inclusiveEachLength) {
            return PrevsFromSelf().Take(inclusiveEachLength).Reverse()
                    .Concat(NextsFromSelf().Take(inclusiveEachLength));
        }

        public IEnumerable<TNode> SiblingsAndSelf(int inclusiveEachLength) {
            return PrevsFromSelf().Take(inclusiveEachLength).Reverse()
                    .Concat(Enumerable.Repeat(This, 1))
                    .Concat(NextsFromSelf().Take(inclusiveEachLength));
        }

        #endregion

        public TNode AddPrevious(TNode node) {
            Contract.Requires(node != null);
            Contract.Requires(node.Parent == null);
            Contract.Requires(Parent != null);
            if (Parent.FirstChild == This) {
                Parent.FirstChild = node;
            }
            return AddPreviousIgnoringFirstChild(node);
        }

        public TNode AddNext(TNode node) {
            Contract.Requires(node != null);
            Contract.Requires(node.Parent == null);
            Contract.Requires(Parent != null);
            return This.CyclicNext.AddPreviousIgnoringFirstChild(node);
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
            node.Parent = This.Parent;
            node.CyclicNext = This;
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
                node.Parent = This;
                node.CyclicNext = node;
                node.CyclicPrev = node;
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
            builder.AppendLine(!Equals(node.Value, null) ? node.Value.ToString() : "");
            foreach (var child in node.Children()) {
                ToStringPrivate(child, depth + 1, builder);
            }
        }
    }
}