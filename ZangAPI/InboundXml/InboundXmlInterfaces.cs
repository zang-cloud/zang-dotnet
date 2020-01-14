using System.Xml.Linq;

namespace AvayaCPaaS.InboundXml
{
    /// <summary>
    /// Interface for the Inbound XML builder node.
    /// </summary>
    /// <typeparam name="TCurrentNode">The current node.</typeparam>
    public interface INode<TCurrentNode> where TCurrentNode : XElement
    {
        /// <summary>
        /// Gets the document.
        /// </summary>
        /// <value>
        /// The document.
        /// </value>
        XDocument Document { get; }

        /// <summary>
        /// Gets the current node.
        /// </summary>
        /// <value>
        /// The current node.
        /// </value>
        TCurrentNode CurrentNode { get; }
    }

    /// <summary>
    /// Interface for the Inbound XML builder node that has parent.
    /// </summary>
    /// <typeparam name="TParentNode">The parent node.</typeparam>
    public interface INodeHasParent<TParentNode> where TParentNode : XElement
    {
        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        TParentNode Parent { get; }
    }

    /// <summary>
    /// The interface for the Inbound XML builder node that can have children.
    /// </summary>
    /// <typeparam name="TCurrentNode">The current node.</typeparam>
    /// <typeparam name="TLastChild">The last child of the current node.</typeparam>
    /// <seealso cref="AvayaCPaaS.InboundXml.INode{TCurrentNode}" />
    public interface INodeCanInner<TCurrentNode, TLastChild> : INode<TCurrentNode> where TCurrentNode : XElement where TLastChild : XElement
    {
        /// <summary>
        /// Gets the last child.
        /// </summary>
        /// <value>
        /// The last child.
        /// </value>
        TLastChild LastChild { get; }

        /// <summary>
        /// Starts the inner part of the last child.
        /// </summary>
        /// <returns>Inner node for current node and it's last child.</returns>
        INodeInner<TLastChild, TCurrentNode> StartInner();
    }

    /// <summary>
    /// The inner node for the Inbound XML builder.
    /// </summary>
    /// <typeparam name="TParentNode">The type of the parent node.</typeparam>
    /// <typeparam name="TCurrentNode">The type of the current node.</typeparam>
    /// <seealso cref="AvayaCPaaS.InboundXml.INode{TCurrentNode}" />
    public interface INodeInner<TCurrentNode, TParentNode> : INode<TCurrentNode>, INodeHasParent<TParentNode> where TParentNode : XElement where TCurrentNode : XElement
    {
        /// <summary>
        /// Ends the inner part of the node current node.
        /// </summary>
        /// <returns>Parent node and it's last child (current node).</returns>
        INodeCanInner<TParentNode, TCurrentNode> EndInner();
    }
}
