using System;
using System.Collections.Generic;
using System.IO;
using RouteFinder;

Graph graph = new Graph();
// 設定ファイルからCSVファイルのパスを読み取る
string csvFilePath = "../../../testdata/nodes.csv";
// CSVファイルからノード(地点)情報を読み取り、ノードを追加
string[] csvLines = File.ReadAllLines(csvFilePath);
foreach (var csvLine in csvLines)
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
