using System.Collections.Generic;
using System.Linq;

namespace TreeDotNet {
    public static class NodeExtensions {
        #region Node

        public static IEnumerable<TNode> Ancestors<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.Ancestors());
        }

        public static IEnumerable<TNode> AncestorsWithSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.AncestorsWithSelf());
        }

        public static IEnumerable<TNode> Children<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.Children());
        }

        public static IEnumerable<TNode> ChildrenWithSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.ChildrenWithSelf());
        }

        public static IEnumerable<TNode> NextsFromSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.NextsFromSelf());
        }

        public static IEnumerable<TNode> NextsFromSelfWithSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.NextsFromSelfWithSelf());
        }

        public static IEnumerable<TNode> NextsFromLast<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.NextsFromLast());
        }

        public static IEnumerable<TNode> NextsFromLastWithSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.NextsFromLastWithSelf());
        }

        public static IEnumerable<TNode> PrevsFromFirst<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.PrevsFromFirst());
        }

        public static IEnumerable<TNode> PrevsFromFirstWithSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.PrevsFromFirstWithSelf());
        }

        public static IEnumerable<TNode> PrevsFromSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.PrevsFromSelf());
        }

        public static IEnumerable<TNode> PrevsFromSelfWithSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.PrevsFromSelfWithSelf());
        }

        public static IEnumerable<TNode> Descendants<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.Descendants());
        }

        public static IEnumerable<TNode> DescendantsAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.DescendantsAndSelf());
        }

        #endregion

        #region NamedNode

        public static IEnumerable<TNode> Ancestors<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.Ancestors(name));
        }

        public static IEnumerable<TNode> AncestorsWithSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsWithSelf(name));
        }

        public static IEnumerable<TNode> Children<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.Children(name));
        }

        public static IEnumerable<TNode> ChildrenWithSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.ChildrenWithSelf(name));
        }

        public static IEnumerable<TNode> NextsFromSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.NextsFromSelf(name));
        }

        public static IEnumerable<TNode> NextsFromSelfWithSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.NextsFromSelfWithSelf(name));
        }

        public static IEnumerable<TNode> NextsFromLast<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.NextsFromLast(name));
        }

        public static IEnumerable<TNode> NextsFromLastWithSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.NextsFromLastWithSelf(name));
        }

        public static IEnumerable<TNode> PrevsFromFirst<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.PrevsFromFirst(name));
        }

        public static IEnumerable<TNode> PrevsFromFirstWithSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.PrevsFromFirstWithSelf(name));
        }

        public static IEnumerable<TNode> PrevsFromSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.PrevsFromSelf(name));
        }

        public static IEnumerable<TNode> PrevsFromSelfWithSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.PrevsFromSelfWithSelf(name));
        }

        public static IEnumerable<TNode> Descendants<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.Descendants(name));
        }

        public static IEnumerable<TNode> DescendantsAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.DescendantsAndSelf(name));
        }

        #endregion
    }
}