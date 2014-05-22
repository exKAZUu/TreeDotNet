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

using System.Collections.Generic;
using System.Linq;

namespace TreeDotNet {
    /// <summary>
    /// Represents a node with a name.
    /// </summary>
    /// <typeparam name="TNode">The type of this class.</typeparam>
    /// <typeparam name="TValue">The type of elements in the list.</typeparam>
    public class NamedNode<TNode, TValue> : Node<TNode, TValue>
            where TNode : NamedNode<TNode, TValue> {
        protected NamedNode() {}

        protected NamedNode(TValue node) : base(node) {}

        public string Name { get; protected set; }

        #region Traversal

        public TNode Child(string name) {
            return Children().FirstOrDefault(node => node.Name == name);
        }

        public IEnumerable<TNode> Ancestors(string name) {
            return Ancestors().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> AncestorsAndSelf(string name) {
            return AncestorsAndSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> Children(string name) {
            return Children().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> NextsFromSelf(string name) {
            return NextsFromSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> NextsFromSelfAndSelf(string name) {
            return NextsFromSelfAndSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> NextsFromLast(string name) {
            return NextsFromLast().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> NextsFromLastAndSelf(string name) {
            return NextsFromLastAndSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> PrevsFromFirst(string name) {
            return PrevsFromFirst().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> PrevsFromFirstAndSelf(string name) {
            return PrevsFromFirstAndSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> PrevsFromSelf(string name) {
            return PrevsFromSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> PrevsFromSelfAndSelf(string name) {
            return PrevsFromSelfAndSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> Descendants(string name) {
            return Descendants().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> DescendantsAndSelf(string name) {
            return DescendantsAndSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> Siblings(string name) {
            return Siblings().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> SiblingsAndSelf(string name) {
            return SiblingsAndSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> AncestorsAndSiblingsAfterSelf(string name) {
            return AncestorsAndSiblingsAfterSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> AncestorsAndSiblingsAfterSelfAndSelf(string name) {
            return AncestorsAndSiblingsAfterSelfAndSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> AncestorsAndSiblingsBeforeSelf(string name) {
            return AncestorsAndSiblingsBeforeSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> AncestorsAndSiblingsBeforeSelfAndSelf(string name) {
            return AncestorsAndSiblingsBeforeSelfAndSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> AncestorsWithSingleChild(string name) {
            return AncestorsWithSingleChild().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> AncestorsWithSingleChildAndSelf(string name) {
            return AncestorsWithSingleChildAndSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> DescendantsOfSingle(string name) {
            return DescendantsOfSingle().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> DescendantsOfSingleAndSelf(string name) {
            return DescendantsOfSingleAndSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> DescendantsOfFirstChild(string name) {
            return DescendantsOfFirstChild().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> DescendantsOfFirstChildAndSelf(string name) {
            return DescendantsOfFirstChildAndSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> Ancestors(string name, int inclusiveDepth) {
            return Ancestors(inclusiveDepth).Where(node => node.Name == name);
        }

        public IEnumerable<TNode> AncestorsAndSelf(string name, int inclusiveDepth) {
            return AncestorsAndSelf(inclusiveDepth).Where(node => node.Name == name);
        }

        public IEnumerable<TNode> Descendants(string name, int inclusiveDepth) {
            return Descendants(inclusiveDepth).Where(node => node.Name == name);
        }

        public IEnumerable<TNode> DescendantsAndSelf(string name, int inclusiveDepth) {
            return DescendantsAndSelf(inclusiveDepth).Where(node => node.Name == name);
        }

        public IEnumerable<TNode> Siblings(string name, int inclusiveEachLength) {
            return Siblings(inclusiveEachLength).Where(node => node.Name == name);
        }

        public IEnumerable<TNode> SiblingsAndSelf(string name, int inclusiveEachLength) {
            return SiblingsAndSelf(inclusiveEachLength).Where(node => node.Name == name);
        }

        #endregion
    }
}