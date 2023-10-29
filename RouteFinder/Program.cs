using System;

namespace RouteFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            // ノード(地点)の情報を設定
            Node nodeA = new Node("A", 37.7749, -122.4194);
            Node nodeB = new Node("B", 37.7748, -122.4193);
            Node nodeC = new Node("C", 37.7747, -122.4192);
            Node nodeD = new Node("D", 37.7746, -122.4191);
            Node nodeE = new Node("E", 37.7745, -122.4190);
            Node nodeF = new Node("F", 37.7744, -122.4189);

            // グラフを作成
            Graph graph = new Graph();
            graph.AddNode(nodeA);
            graph.AddNode(nodeB);
            graph.AddNode(nodeC);
            graph.AddNode(nodeD);
            graph.AddNode(nodeE);
            graph.AddNode(nodeF);

            // エッジを追加
            // ここで各ノード(地点)との関係図を作成
            // どこからどこへ行けるか？(繋がっているか？)を定義
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("A", "D");
            graph.AddEdge("D", "E");
            graph.AddEdge("B", "E");
            graph.AddEdge("C", "F");
            graph.AddEdge("E", "F");

            // ノードとエッジの位置関係を出力
            graph.PrintGraph();
            Console.WriteLine("");

            // 最短経路を探索
            string startNodeName = "A";
            string endNodeName = "F";
            List<List<Node>> shortestPaths = graph.FindShortestPath(startNodeName, endNodeName);

            Console.WriteLine("最短経路:");
            foreach (var path in shortestPaths)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    Console.Write($"{path[i].Name} (緯度: {path[i].Latitude}, 経度: {path[i].Longitude})");
                    if (i < path.Count - 1)
                        Console.Write(" => ");
                }
                Console.WriteLine();
            }
        }
    }
}