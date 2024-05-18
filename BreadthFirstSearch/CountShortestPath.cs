namespace DataStructuresAndAlgorithms.BreadthFirstSearch
{
    /// <summary>
    /// Find the shortest path from 'S' to 'E' given that it can only walk through '.' and '#' is a blocked passage
    /// </summary>
    public static class CountShortestPath
    {
        private static int[] dRow = { -1, 1, 0, 0 };
        private static int[] dColumn = { 0, 0, 1, -1 };

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

        public static Pair FindStartingPoint(char[,] grid)
        {
            for(var i = 0; i < grid.GetLength(0); i++)
            {
                for(var j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 'S')
                    {
                        return new Pair(i, j);
                    }
                }
            }

            throw new Exception("Starting Point could not be found");
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

            var vis = new bool[grid.GetLength(0), grid.GetLength(1)];
            var queue = new Queue<Pair>();

            var visited_layers = 0;
            var processed_nodes = 1;
            var number_nodes_next_layer  = 0;

            // Visit and enqueue first node
            queue.Enqueue(new Pair(row, col));
            vis[row, col] = true;

            while (queue.Count > 0)
            {
                // Process last node in queue
                var pair = queue.Dequeue();
                var x = pair.first;
                var y = pair.second;

                if (grid[x, y] == 'E')
                {
                    return visited_layers;
                }

                // Visit and enqueue all adjacent nodes of the processed node
                // and check if any of these node is the End Point
                number_nodes_next_layer += GoThoughtAdjacents(queue, grid, vis, x, y);

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

        public static int GoThoughtAdjacents(Queue<Pair> queue, char[,] grid, bool[,] vis, int x, int y)
        {
            var number_valid_nodes = 0;

            for (var i = 0; i < 4; i++)
            {
                var adjx = x + dRow[i];
                var adjy = y + dColumn[i];

                if (isValid(grid, vis, adjx, adjy))
                {
                    queue.Enqueue(new Pair(adjx, adjy));
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
    }
}
