using System.Collections.Generic;
using System.Linq;

namespace TreeDotNet {
    public static class NodeExtensions {
        public static IEnumerable<TNode> Ancestors<TNode, T>(IEnumerable<Node<TNode, T>> This)
                where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.Ancestors());
        }

        public static IEnumerable<TNode> AncestorsWithSelf<TNode, T>(
                IEnumerable<Node<TNode, T>> This) where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.AncestorsWithSelf());
        }

        public static IEnumerable<TNode> Children<TNode, T>(IEnumerable<Node<TNode, T>> This)
                where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.Children());
        }

        public static IEnumerable<TNode> ChildrenWithSelf<TNode, T>(
                IEnumerable<Node<TNode, T>> This) where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.ChildrenWithSelf());
        }

        public static IEnumerable<TNode> NextsFromSelf<TNode, T>(IEnumerable<Node<TNode, T>> This)
                where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.NextsFromSelf());
        }

        public static IEnumerable<TNode> NextsFromSelfWithSelf<TNode, T>(
                IEnumerable<Node<TNode, T>> This) where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.NextsFromSelfWithSelf());
        }

        public static IEnumerable<TNode> NextsFromLast<TNode, T>(IEnumerable<Node<TNode, T>> This)
                where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.NextsFromLast());
        }

        public static IEnumerable<TNode> NextsFromLastWithSelf<TNode, T>(
                IEnumerable<Node<TNode, T>> This) where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.NextsFromLastWithSelf());
        }

        public static IEnumerable<TNode> PrevsFromFirst<TNode, T>(IEnumerable<Node<TNode, T>> This)
                where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.PrevsFromFirst());
        }

        public static IEnumerable<TNode> PrevsFromFirstWithSelf<TNode, T>(
                IEnumerable<Node<TNode, T>> This) where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.PrevsFromFirstWithSelf());
        }

        public static IEnumerable<TNode> PrevsFromSelf<TNode, T>(IEnumerable<Node<TNode, T>> This)
                where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.PrevsFromSelf());
        }

        public static IEnumerable<TNode> PrevsFromSelfWithSelf<TNode, T>(
                IEnumerable<Node<TNode, T>> This) where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.PrevsFromSelfWithSelf());
        }

        public static IEnumerable<TNode> Descendants<TNode, T>(IEnumerable<Node<TNode, T>> This)
                where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.Descendants());
        }

        public static IEnumerable<TNode> DescendantsAndSelf<TNode, T>(
                IEnumerable<Node<TNode, T>> This) where TNode : Node<TNode, T> {
            return This.SelectMany(node => node.DescendantsAndSelf());
        }
    }
}