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
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class NamedNode<TNode, T> : Node<TNode, T>
            where TNode : NamedNode<TNode, T> {
        public string Name { get; protected set; }

        #region Traversal

        public IEnumerable<TNode> Ancestors(string name) {
            return Ancestors().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> AncestorsWithSelf(string name) {
            return AncestorsWithSelf().Where(node => node.Name == name);
        }

        public TNode Child(string name) {
            return Children().FirstOrDefault(node => node.Name == name);
        }

        public IEnumerable<TNode> Children(string name) {
            return Children().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> ChildrenWithSelf(string name) {
            return ChildrenWithSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> NextsFromSelf(string name) {
            return NextsFromSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> NextsFromSelfWithSelf(string name) {
            return NextsFromSelfWithSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> NextsFromLast(string name) {
            return NextsFromLast().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> NextsFromLastWithSelf(string name) {
            return NextsFromLastWithSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> PrevsFromFirst(string name) {
            return PrevsFromFirst().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> PrevsFromFirstWithSelf(string name) {
            return PrevsFromFirstWithSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> PrevsFromSelf(string name) {
            return PrevsFromSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> PrevsFromSelfWithSelf(string name) {
            return PrevsFromSelfWithSelf().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> Descendants(string name) {
            return Descendants().Where(node => node.Name == name);
        }

        public IEnumerable<TNode> DescendantsAndSelf(string name) {
            return DescendantsAndSelf().Where(node => node.Name == name);
        }

        #endregion
    }
}