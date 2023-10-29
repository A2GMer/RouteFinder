using System;

namespace RouteFinder
{
    class Edge
    {
        // エッジの始点ノード
        public Node Start { get; set; }
        // エッジの終点ノード
        public Node End { get; set; }
        // エッジのコスト（直線距離）
        public double Cost { get; set; }

        public Edge(Node start, Node end)
        {
            Start = start;
            End = end;
            // 直線距離をコストとして計算
            Cost = CalculateDistance(start, end);
        }

        /// <summary>
        /// 直線距離を計算
        /// </summary>
        /// <param name="node1">始点ノード</param>
        /// <param name="node2">終点ノード</param>
        /// <returns></returns>
        private double CalculateDistance(Node node1, Node node2)
        {
            // 2つの緯度経度から直線距離を計算するロジックを追加
            // ここは別の方法でもいいからなんとかして計算する。
            // 例: ヒュベニの距離公式を使用する
            double radius = 6371; // 地球の平均半径（単位はキロメートル）
            double lat1 = Math.PI * node1.Latitude / 180.0;
            double lat2 = Math.PI * node2.Latitude / 180.0;
            double lon1 = Math.PI * node1.Longitude / 180.0;
            double lon2 = Math.PI * node2.Longitude / 180.0;

            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;

            double a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return radius * c;
        }

        public override string ToString()
        {
            return $"{Start} -- {End}";
        }
    }
}

