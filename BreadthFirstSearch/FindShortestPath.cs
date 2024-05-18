namespace DataStructuresAndAlgorithms.BreadthFirstSearch
{
    /// <summary>
    /// Find the shortest path from 'S' to 'E' given that it can only walk through '.' and '#' is a blocked passage
    /// </summary>
    public static class FindShortestPath
    {
        private static int[] dRow = { -1, 1, 0, 0 };
        private static int[] dColumn = { 0, 0, 1, -1 };
        private static Queue<Pair> Queue;
        private static List<Tuple<Pair, Pair>> PairPath;

        public static void Testing()
        {
            char[,] grid = {
                { 'S', '.', '.', '#', '.', '.', '.' },
                { '.', '#', '.', '.', '.', '#', '.' },
                { '.', '#', '.', '.', '.', '.', '.' },
                { '.', '.', '#', '#', '.', '.', '.' },
                { '#', '.', '#', 'E', '.', '.', '.' },
            };

            Console.WriteLine("Number of steps: " + CalculateShortestPath(grid));
        }

        /// <summary>
        /// In this case BFS will be used to count how many layers (visited_layers) is necessary to reach the End Point.
        /// The number of layers visited (except the first one) is the number of steps
        /// </summary>
        public static int CalculateShortestPath(char[,] grid)
        {
            // Get starting point
            var sPoint = FindStartingPoint(grid);
            var row = sPoint.first;
            var col = sPoint.second;

            Queue = new Queue<Pair>();
            PairPath = new List<Tuple<Pair, Pair>>();

            var vis = new bool[grid.GetLength(0), grid.GetLength(1)];

            var visited_layers = 0;
            var processed_nodes = 1;
            var number_nodes_next_layer = 0;

            // Visit and enqueue first node
            Queue.Enqueue(new Pair(row, col));
            vis[row, col] = true;

            while (Queue.Count > 0)
            {
                // Process last node in queue
                var pair = Queue.Dequeue();
                var x = pair.first;
                var y = pair.second;

                if (grid[x, y] == 'E')
                {
                    PrintThePath(grid, x, y);
                    return visited_layers;
                }

                // Visit and enqueue all adjacent nodes of the processed node
                // and check if any of these node is the End Point
                number_nodes_next_layer += GoThoughtAdjacents(grid, vis, x, y);

                // After processing each node of the current layer
                // then goes to the next layer and increase the number of visited layer
                processed_nodes--;
                if (processed_nodes == 0)
                {
                    processed_nodes = number_nodes_next_layer;
                    number_nodes_next_layer = 0;
                    visited_layers++;
                }
            }

            return visited_layers;
        }

        public static Pair FindStartingPoint(char[,] grid)
        {
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                for (var j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 'S')
                    {
                        return new Pair(i, j);
                    }
                }
            }

            throw new Exception("Starting Point could not be found");
        }

        public static int GoThoughtAdjacents(char[,] grid, bool[,] vis, int x, int y)
        {
            var number_valid_nodes = 0;

            for (var i = 0; i < 4; i++)
            {
                var adjx = x + dRow[i];
                var adjy = y + dColumn[i];

                if (isValid(grid, vis, adjx, adjy))
                {
                    var adjPair = new Pair(adjx, adjy);

                    Queue.Enqueue(adjPair);
                    PairPath.Add(new Tuple<Pair, Pair>(adjPair, new Pair(x, y)));
                    vis[adjx, adjy] = true;
                    number_valid_nodes++;
                }
            }

            return number_valid_nodes;
        }

        public static bool isValid(char[,] grid, bool[,] vis, int row, int col)
        {
            if (row < 0 || col < 0 || row >= vis.GetLength(0) || col >= vis.GetLength(1))
                return false;

            return !vis[row, col] && grid[row, col] != '#';
        }

        public static void PrintThePath(char[,] grid, int endX, int endY)
        {
            var currNode = PairPath.FirstOrDefault(_ => _.Item1.first == endX && _.Item1.second == endY);

            while (currNode != null)
            {
                Console.WriteLine($"({currNode?.Item1.first}, {currNode?.Item1.second}) - {grid[currNode.Item1.first, currNode.Item1.second]}");
                var nextNode = PairPath.FirstOrDefault(_ => _.Item1.first == currNode.Item2.first && _.Item1.second == currNode.Item2.second);

                if (nextNode == null)
                {
                    Console.WriteLine($"({currNode?.Item2.first}, {currNode?.Item2.second}) - {grid[currNode.Item2.first, currNode.Item2.second]}");
                }

                currNode = nextNode;
            }
        }
    }
}
