using CCSC.Glue;
using Silk.NET.Maths;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace CCSC.Nodestuff
{
    internal static class Positioner
    {
        private static readonly List<int> maxYperX = [];
        private static readonly List<bool> liftedY = [];
        private static readonly List<Node> visited = [];

        internal static void SetupStartPositions(Dictionary<string, NodeStore> nodes)
        {
            foreach (var store in nodes.Keys)
            {
                if (nodes[store].Nodes.Count <= 0)
                {
                    continue;
                }

                Main.SelectedCharacter = store;

                NodeStore nodeStore = nodes[store];
                nodeStore.Positions.Clear();

                //no idea why we have to clear it or where the wrong ones come from....
                SetStartPositionsForNodesInList(100, 1, nodeStore, [.. nodeStore.Nodes]);

                Main.CenterAndSelectNode(nodes[store].Nodes.First(), 0.8f);
            }
        }

        internal static void SetStartPositionsForNodesInList(int intX, int intY, NodeStore nodeStore, List<Node> nodeList, bool inListOnly = false)
        {
            int ParentEdgeMaxStartValue = 0;
            maxYperX.Clear();
            maxYperX.ExtendToIndex(intX, intY);

            for (int i = 0; i < maxYperX.Count; i++)
            {
                maxYperX[i] = intY;
            }

            nodeList.Sort(new NodeComparers(nodeStore));
            bool restarted = false;
            int skipCount = 0;
        Restart:
            visited.Clear();
            foreach (var key in nodeList)
            {
                Family family = nodeStore[key];
                if (!restarted && (family.Parents.Count > ParentEdgeMaxStartValue || visited.Contains(key)))
                {
                    skipCount++;
                    continue;
                }

                if (family.Childs.Count > 0)
                {
                    intX = maxYperX.Count + 1;
                }
                else
                {
                    intX += 1 + ParentEdgeMaxStartValue;
                }
                maxYperX.ExtendToIndex(intX, intY);

                //selectedcharacter is set correctly here
                intX = SetStartPosForConnected(intX, nodeStore, key, inListOnly);
            }
            if (!restarted && skipCount == nodeList.Count)
            {
                restarted = true;
                goto Restart;
            }
        }

        internal static int SetStartPosForConnected(int intX, NodeStore nodeStore, Node start, bool inListOnly = false)
        {
            maxYperX.ExtendToIndex(intX, (int)(start.Position.Y / Main.scaleY));
            liftedY.ExtendToIndex(intX, false);

            Queue<Node> toExplore = [];
            Queue<int> layerX = [];
            toExplore.Enqueue(start);
            layerX.Enqueue(intX);
            int lastIntX = 0;

            //Debug.WriteLine($"starting on {key.ID} at {intX}|{1}");

            while (toExplore.Count > 0)
            {
                var node = toExplore.Dequeue();
                intX = layerX.Dequeue();

                if (visited.Contains(node) || (inListOnly && !Main.selected.Contains(node)))
                {
                    continue;
                }
                else
                {
                    visited.Add(node);
                }

                var childs = nodeStore.Childs(node);
                childs.Sort(new NodeChildComparer(nodeStore));
                var parents = nodeStore.Parents(node);
                parents.Sort(new NodeComparers(nodeStore));

                int newParentsX = intX - (parents.Count / 3) - 1;
                newParentsX = Math.Max(0, newParentsX);
                int newChildX = intX + (childs.Count / 3) + 1;
                maxYperX.ExtendToIndex(newChildX, maxYperX[intX]);
                liftedY.ExtendToIndex(newChildX, false);

                int rest = (int)float.Round(node.Size.Height / Main.NodeSizeY);
                rest = rest <= 0 ? 1 : rest;

                var newPos = new PointF(intX * Main.scaleX, maxYperX[intX] * Main.scaleY);
                //if we are more right along the screen this is a child
                if (lastIntX < intX)
                {
                    ShoveNodesToRight(node, newPos);
                }
                else
                {
                    ShoveNodesToLeft(node, newPos);
                }

                node.Position = newPos;

                maxYperX[intX] += rest;

                if (childs.Count > 0)
                {
                    //only do once per X
                    if (!liftedY[newChildX])
                    {
                        liftedY[newChildX] = true;
                        maxYperX[newChildX] = maxYperX[intX] - rest;
                    }
                    foreach (var item in childs)
                    {
                        if (visited.Contains(item) || (inListOnly && !Main.selected.Contains(item)))
                        {
                            continue;
                        }

                        layerX.Enqueue(newChildX);
                        toExplore.Enqueue(item);
                    }
                }

                if (parents.Count > 0)
                {
                    if (maxYperX[newParentsX] < maxYperX[intX] - rest)
                    {
                        maxYperX[newParentsX] = maxYperX[intX] - rest;
                    }

                    foreach (var item in parents)
                    {
                        if (visited.Contains(item) || (inListOnly && !Main.selected.Contains(item)))
                        {
                            continue;
                        }

                        layerX.Enqueue(newParentsX);
                        toExplore.Enqueue(item);
                    }
                }
                lastIntX = intX;
            }

            return intX;
        }

        internal static void ShoveNodesToLeft(Node nodeToPlace, PointF newPos)
        {
            var maybeNodePos = newPos;
            Node maybeThere;
            Node maybeNewSpot;

            while ((maybeNewSpot = Main.GetNodeAtPoint(maybeNodePos + Main.NodeCenter)) != Node.NullNode && maybeNewSpot != nodeToPlace)
            {
                maybeNodePos = newPos;
                while ((maybeThere = Main.GetNodeAtPoint(maybeNodePos + Main.NodeCenter)) != Node.NullNode && maybeThere != nodeToPlace)
                {
                    maybeNodePos -= new SizeF(Main.scaleX, 0);
                    if (Main.GetNodeAtPoint(maybeNodePos + Main.NodeCenter) == Node.NullNode)
                    {
                        maybeThere.Position = maybeNodePos;
                        break;
                    }
                }
            }
        }

        internal static void ShoveNodesToRight(Node nodeToPlace, PointF newPos)
        {
            var maybeNodePos = newPos;
            Node maybeThere;
            Node maybeNewSpot;

            while ((maybeNewSpot = Main.GetNodeAtPoint(maybeNodePos + Main.NodeCenter)) != Node.NullNode && maybeNewSpot != nodeToPlace)
            {
                maybeNodePos = newPos;
                while ((maybeThere = Main.GetNodeAtPoint(maybeNodePos + Main.NodeCenter)) != Node.NullNode && maybeThere != nodeToPlace)
                {
                    maybeNodePos += new SizeF(Main.scaleX, 0);
                    if (Main.GetNodeAtPoint(maybeNodePos + Main.NodeCenter) == Node.NullNode)
                    {
                        maybeThere.Position = maybeNodePos;
                        break;
                    }
                }
            }
        }

        internal static void SortConnected(Node selectedNode, NodeStore store)
        {
            //clickednode is set when this is called
            visited.Clear();
            var intX = (int)(selectedNode.Position.X / Main.scaleX);
            var intY = (int)(selectedNode.Position.Y / Main.scaleY);
            maxYperX.ExtendToIndex(intX, intY);

            for (int i = 0; i < maxYperX.Count; i++)
            {
                maxYperX[i] = intY;
            }
            //selectedcharacter is set correctly here
            SetStartPosForConnected(intX, store, selectedNode);
        }
    }
}
