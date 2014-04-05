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

        public static IEnumerable<TNode> AncestorsAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.AncestorsAndSelf());
        }

        public static IEnumerable<TNode> Children<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.Children());
        }

        public static IEnumerable<TNode> ChildrenAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.ChildrenAndSelf());
        }

        public static IEnumerable<TNode> NextsFromSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.NextsFromSelf());
        }

        public static IEnumerable<TNode> NextsFromSelfAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.NextsFromSelfAndSelf());
        }

        public static IEnumerable<TNode> NextsFromLast<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.NextsFromLast());
        }

        public static IEnumerable<TNode> NextsFromLastAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.NextsFromLastAndSelf());
        }

        public static IEnumerable<TNode> PrevsFromFirst<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.PrevsFromFirst());
        }

        public static IEnumerable<TNode> PrevsFromFirstAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.PrevsFromFirstAndSelf());
        }

        public static IEnumerable<TNode> PrevsFromSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.PrevsFromSelf());
        }

        public static IEnumerable<TNode> PrevsFromSelfAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.PrevsFromSelfAndSelf());
        }

        public static IEnumerable<TNode> Descendants<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.Descendants());
        }

        public static IEnumerable<TNode> DescendantsAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self)
                where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.DescendantsAndSelf());
        }

        public static IEnumerable<TNode> Siblings<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.Siblings());
        }

        public static IEnumerable<TNode> SiblingsAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.SiblingsAndSelf());
        }

        public static IEnumerable<TNode> AncestorsAndSiblingsAfterSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsAndSiblingsAfterSelf());
        }

        public static IEnumerable<TNode> AncestorsAndSiblingsAfterSelfAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsAndSiblingsAfterSelfAndSelf());
        }

        public static IEnumerable<TNode> AncestorsAndSiblingsBeforeSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsAndSiblingsBeforeSelf());
        }

        public static IEnumerable<TNode> AncestorsAndSiblingsBeforeSelfAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsAndSiblingsBeforeSelfAndSelf());
        }

        public static IEnumerable<TNode> AncestorsOfSingle<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.AncestorsOfSingle());
        }

        public static IEnumerable<TNode> AncestorsOfSingleAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.AncestorsOfSingleAndSelf());
        }

        public static IEnumerable<TNode> DescendantsOfSingle<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.DescendantsOfSingle());
        }

        public static IEnumerable<TNode> DescendantsOfSingleAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.DescendantsOfSingleAndSelf());
        }

        public static IEnumerable<TNode> DescendantsOfFirstChild<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.DescendantsOfFirstChild());
        }

        public static IEnumerable<TNode> DescendantsOfFirstChildAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.DescendantsOfFirstChildAndSelf());
        }

        public static IEnumerable<TNode> Ancestors<TNode, T>(
                this IEnumerable<Node<TNode, T>> self, int inclusiveDepth) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.Ancestors(inclusiveDepth));
        }

        public static IEnumerable<TNode> AncestorsAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self, int inclusiveDepth) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.AncestorsAndSelf(inclusiveDepth));
        }

        public static IEnumerable<TNode> Descendants<TNode, T>(
                this IEnumerable<Node<TNode, T>> self, int inclusiveDepth) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.Descendants(inclusiveDepth));
        }

        public static IEnumerable<TNode> DescendantsAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self, int inclusiveDepth) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.DescendantsAndSelf(inclusiveDepth));
        }

        public static IEnumerable<TNode> Siblings<TNode, T>(
                this IEnumerable<Node<TNode, T>> self, int inclusiveEachLength) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.Siblings(inclusiveEachLength));
        }

        public static IEnumerable<TNode> SiblingsAndSelf<TNode, T>(
                this IEnumerable<Node<TNode, T>> self, int inclusiveEachLength) where TNode : Node<TNode, T> {
            return self.SelectMany(node => node.SiblingsAndSelf(inclusiveEachLength));
        }

        #endregion

        #region NamedNode

        public static IEnumerable<TNode> Ancestors<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.Ancestors(name));
        }

        public static IEnumerable<TNode> AncestorsAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsAndSelf(name));
        }

        public static IEnumerable<TNode> Children<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.Children(name));
        }

        public static IEnumerable<TNode> ChildrenAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.ChildrenAndSelf(name));
        }

        public static IEnumerable<TNode> NextsFromSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.NextsFromSelf(name));
        }

        public static IEnumerable<TNode> NextsFromSelfAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.NextsFromSelfAndSelf(name));
        }

        public static IEnumerable<TNode> NextsFromLast<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.NextsFromLast(name));
        }

        public static IEnumerable<TNode> NextsFromLastAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.NextsFromLastAndSelf(name));
        }

        public static IEnumerable<TNode> PrevsFromFirst<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.PrevsFromFirst(name));
        }

        public static IEnumerable<TNode> PrevsFromFirstAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.PrevsFromFirstAndSelf(name));
        }

        public static IEnumerable<TNode> PrevsFromSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.PrevsFromSelf(name));
        }

        public static IEnumerable<TNode> PrevsFromSelfAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.PrevsFromSelfAndSelf(name));
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

        public static IEnumerable<TNode> Siblings<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.Siblings(name));
        }

        public static IEnumerable<TNode> SiblingsAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.SiblingsAndSelf(name));
        }

        public static IEnumerable<TNode> AncestorsAndSiblingsAfterSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsAndSiblingsAfterSelf(name));
        }

        public static IEnumerable<TNode> AncestorsAndSiblingsAfterSelfAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsAndSiblingsAfterSelfAndSelf(name));
        }

        public static IEnumerable<TNode> AncestorsAndSiblingsBeforeSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsAndSiblingsBeforeSelf(name));
        }

        public static IEnumerable<TNode> AncestorsAndSiblingsBeforeSelfAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsAndSiblingsBeforeSelfAndSelf(name));
        }

        public static IEnumerable<TNode> AncestorsOfSingle<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsOfSingle(name));
        }

        public static IEnumerable<TNode> AncestorsOfSingleAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsOfSingleAndSelf(name));
        }

        public static IEnumerable<TNode> DescendantsOfSingle<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.DescendantsOfSingle(name));
        }

        public static IEnumerable<TNode> DescendantsOfSingleAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.DescendantsOfSingleAndSelf(name));
        }

        public static IEnumerable<TNode> DescendantsOfFirstChild<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.DescendantsOfFirstChild(name));
        }

        public static IEnumerable<TNode> DescendantsOfFirstChildAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.DescendantsOfFirstChildAndSelf(name));
        }

        public static IEnumerable<TNode> Ancestors<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name, int inclusiveDepth)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.Ancestors(name, inclusiveDepth));
        }

        public static IEnumerable<TNode> AncestorsAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name, int inclusiveDepth)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.AncestorsAndSelf(name, inclusiveDepth));
        }

        public static IEnumerable<TNode> Descendants<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name, int inclusiveDepth)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.Descendants(name, inclusiveDepth));
        }

        public static IEnumerable<TNode> DescendantsAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name, int inclusiveDepth)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.DescendantsAndSelf(name, inclusiveDepth));
        }

        public static IEnumerable<TNode> Siblings<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name, int inclusiveEachLength)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.Siblings(name, inclusiveEachLength));
        }

        public static IEnumerable<TNode> SiblingsAndSelf<TNode, T>(
                this IEnumerable<NamedNode<TNode, T>> self, string name, int inclusiveEachLength)
                where TNode : NamedNode<TNode, T> {
            return self.SelectMany(node => node.SiblingsAndSelf(name, inclusiveEachLength));
        }

        #endregion
    }
}