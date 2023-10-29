using System;

namespace RouteFinder
{
    class Graph
    {
        private Dictionary<string, Node> nodes;
        private List<Edge> edges;

        public Graph()
        {
            nodes = new Dictionary<string, Node>();
            edges = new List<Edge>();
        }

        public void AddNode(Node node)
        {
            // ノードを辞書に追加（名前をキーとして）
            nodes[node.Name] = node;
        }

        public void AddEdge(string startNodeName, string endNodeName)
        {
            if (nodes.ContainsKey(startNodeName) && nodes.ContainsKey(endNodeName))
            {
                Node startNode = nodes[startNodeName];
                Node endNode = nodes[endNodeName];
                // エッジをリストに追加
                edges.Add(new Edge(startNode, endNode));
            }
        }

        public List<List<Node>> FindShortestPath(string startNodeName, string endNodeName)
        {
            // 開始ノードと終了ノードを取得
            Node startNode = nodes[startNodeName];
            Node endNode = nodes[endNodeName];

            // 各ノードへの最短距離を保持する辞書
            Dictionary<Node, double> distance = new Dictionary<Node, double>();
            // 最短経路における前のノードを保持する辞書
            Dictionary<Node, Node> previous = new Dictionary<Node, Node>();
            // 未訪問のノードを保持するセット
            HashSet<Node> unvisitedNodes = new HashSet<Node>();

            // 各ノードの初期距離を無限大、前のノードをnullとして初期化
            foreach (var node in nodes.Values)
            {
                distance[node] = double.MaxValue;
                previous[node] = null;
                unvisitedNodes.Add(node);
            }

            // 開始ノードまでの距離を0に設定
            distance[startNode] = 0;

            // 未訪問のノードが残っている間繰り返し
            while (unvisitedNodes.Count > 0)
            {
                Node currentNode = null;
                // 未訪問のノードのうち、最短距離のノードを選択
                foreach (var node in unvisitedNodes)
                {
                    if (currentNode == null || distance[node] < distance[currentNode])
                        currentNode = node;
                }

                // 選択したノードを未訪問ノードから削除
                unvisitedNodes.Remove(currentNode);

                // ゴールノードに到達した場合、探索を終了
                if (currentNode == endNode)
                    break;

                // 選択したノードに隣接するノードを探索
                foreach (var neighborEdge in GetNeighborEdges(currentNode))
                {
                    Node neighborNode = neighborEdge.End;
                    // 選択ノードを経由した場合の距離を計算
                    double tentativeDistance = distance[currentNode] + neighborEdge.Cost;
                    // より短い距離が見つかった場合、更新
                    if (tentativeDistance < distance[neighborNode])
                    {
                        distance[neighborNode] = tentativeDistance;
                        previous[neighborNode] = currentNode;
                    }
                }
            }

            // 最短経路を格納するリスト
            List<List<Node>> shortestPaths = new List<List<Node>>();

            // ゴールノードから開始ノードまで遡り、最短経路を構築
            Node current = endNode;
            while (current != null)
            {
                List<Node> pathSegment = new List<Node>();
                Node previousNode = previous[current];
                pathSegment.Add(current);

                if (previousNode != null)
                    pathSegment.Add(previousNode);

                if (pathSegment.Count() == 2)
                {
                    pathSegment.Reverse();
                    shortestPaths.Add(pathSegment);
                }
                current = previousNode;
            }

            // 最短経路リストを逆順にして返す
            shortestPaths.Reverse();
            return shortestPaths;
        }


        private List<Edge> GetNeighborEdges(Node node)
        {
            List<Edge> neighborEdges = new List<Edge>();
            foreach (var edge in edges)
            {
                if (edge.Start == node)
                    neighborEdges.Add(edge);
            }
            return neighborEdges;
        }

        public void PrintGraph()
        {
            Console.WriteLine("ノードとエッジの位置関係:");
            foreach (var edge in edges)
            {
                Console.WriteLine(edge);
            }
        }

    }
}

