using System;

namespace RouteFinder
{
    class Node
    {
        // ノードの名前
        public string Name { get; set; }
        // ノードの緯度
        public double Latitude { get; set; }
        // ノードの経度
        public double Longitude { get; set; }
        // その他の路線情報を追加

        public Node(string name, double latitude, double longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}

