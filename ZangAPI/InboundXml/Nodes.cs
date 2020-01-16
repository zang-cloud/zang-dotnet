using System.Xml.Linq;

namespace AvayaCPaaS.InboundXml
{
    /// <summary>
    /// The node for the Inbound XML builder.
    /// </summary>
    /// <typeparam name="TCurrentNode">The type of the current node.</typeparam>
    /// <seealso cref="AvayaCPaaS.InboundXml.INode{TCurrentNode}" />
    public class InboundXmlNode<TCurrentNode> : INode<TCurrentNode> where TCurrentNode : XElement
    {
        /// <summary>
        /// Gets the current node.
        /// </summary>
        public TCurrentNode CurrentNode { get; protected set; }

        /// <summary>
        /// Gets the document.
        /// </summary>
        public XDocument Document
        {
            get
            {
                // returns the current document if there is anything in current node
                return this.CurrentNode?.Document;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InboundXmlNode{TCurrentNode}"/> class.
        /// </summary>
        /// <param name="currentNode">The current node.</param>
        public InboundXmlNode(TCurrentNode currentNode)
        {
            // sets the current node
            this.CurrentNode = currentNode;
        }
    }

    /// <summary>
    /// The node for the Inbound XML builder that can have inner nodes.
    /// </summary>
    /// <typeparam name="TCurrentNode">The current node.</typeparam>
    /// <typeparam name="TLastChild">The last child of current node.</typeparam>
    public class InboundXmlNodeCanInner<TCurrentNode, TLastChild>
            : InboundXmlNode<TCurrentNode>, INodeCanInner<TCurrentNode, TLastChild>
            where TCurrentNode : XElement
            where TLastChild : XElement
    {
        /// <summary>
        /// Gets the last child.
        /// </summary>
        /// <value>
        /// The last child.
        /// </value>
        public TLastChild LastChild { get; protected set; }

        public InboundXmlNodeCanInner(TCurrentNode currentNode, TLastChild lastChild)
            : base(currentNode)
        {
            // sets the last child
            this.LastChild = lastChild;
        }

        /// <summary>
        /// Starts the inner node.
        /// </summary>
        /// <returns></returns>
        public INodeInner<TLastChild, TCurrentNode> StartInner()
        {
            // returns the inner node.
            return new InboundXmlNodeInner<TLastChild, TCurrentNode>(this.LastChild, this.CurrentNode);
        }
    }

    /// <summary>
    /// The inner node for the Inbound XML builder.
    /// </summary>
    /// <typeparam name="TCurrentNode">The type of the current node.</typeparam>
    /// <typeparam name="TParentNode">The type of the parent node.</typeparam>
    /// <seealso cref="AvayaCPaaS.InboundXml.InboundXmlNode{TCurrentNode}" />
    /// <seealso cref="AvayaCPaaS.InboundXml.INodeInner{TCurrentNode, TParentNode}" />
    public class InboundXmlNodeInner<TCurrentNode, TParentNode>
        : InboundXmlNode<TCurrentNode>, INodeInner<TCurrentNode, TParentNode>
        where TParentNode : XElement
        where TCurrentNode : XElement
    {
        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public TParentNode Parent { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InboundXmlNodeInner{TCurrentNode, TParentNode}"/> class.
        /// </summary>
        /// <param name="currentNode">The current node.</param>
        /// <param name="parentNode">The parent node.</param>
        public InboundXmlNodeInner(TCurrentNode currentNode, TParentNode parentNode)
            : base(currentNode)
        {
            // sets the parent node
            this.Parent = parentNode;
        }

        /// <summary>
        /// Ends the inner part of the node current node.
        /// </summary>
        /// <returns>
        /// Parent node and it's last child (current node).
        /// </returns>
        public INodeCanInner<TParentNode, TCurrentNode> EndInner()
        {
            // returns the parent node
            return new InboundXmlNodeCanInner<TParentNode, TCurrentNode>(this.Parent, this.CurrentNode);
        }
    }
}
