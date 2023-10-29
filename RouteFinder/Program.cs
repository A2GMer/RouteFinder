using System;
using System.Collections.Generic;
using System.IO;

namespace RouteFinder
{
    class Program
    {
        // 始点ノード名
        public static string startNodeName = "A";
        // 終点ノード名
        public static string endNodeName = "F";
        // ノード情報定義CSVファイルパス
        public static string nodesFilePath = "../../../testdata/nodes.csv";
        // エッジ情報定義CSVファイルパス
        public static string edgesFilePath = "../../../testdata/edges.csv";

        static void Main(string[] args)
        {
            Graph graph = new Graph();
            // CSVファイルからノード(地点)情報を読み取り、ノードを追加
            GetNodes(graph);

            // CSVファイルからエッジ(繋がり)情報を読み取り、エッジを追加
            GetEdges(graph);

            // ノードとエッジの位置関係を出力
            graph.PrintGraph();

            Console.WriteLine("");

            // 最短経路を探索
            List<List<Node>> shortestPaths = graph.FindShortestPath(startNodeName, endNodeName);
            NewMethod(shortestPaths);
        }

        private static void NewMethod(List<List<Node>> shortestPaths)
        {
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

        private static void GetEdges(Graph graph)
        {
            string[] edgeCsvLines = File.ReadAllLines(edgesFilePath)
                .Skip(1).ToArray();
            foreach (var csvLine in edgeCsvLines)
            {
                var values = csvLine.Split(',').ToArray();
                if (values.Count() == 2)
                {
                    string startNode = values[0];
                    string endNode = values[1];
                    // エッジを追加
                    // ここで各ノード(地点)との関係図を作成
                    // どこからどこへ行けるか？(繋がっているか？)を定義
                    graph.AddEdge(startNode, endNode);
                }
            }
        }

        private static void GetNodes(Graph graph)
        {
            string[] nodesCsvLines = File.ReadAllLines(nodesFilePath)
                .Skip(1).ToArray();
            foreach (var csvLine in nodesCsvLines)
            {
                var values = csvLine.Split(',').ToArray();
                if (values.Count() == 3)
                {
                    string name = values[0];
                    double latitude = double.Parse(values[1]);
                    double longitude = double.Parse(values[2]);
                    Node node = new Node(name, latitude, longitude);
                    // グラフを作成
                    graph.AddNode(node);
                }
            }
        }
    }
}