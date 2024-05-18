namespace DataStructuresAndAlgorithms.BreadthFirstSearch
{
    public static class BfsAlgorithm
    {
        private static int[] dRow = { -1, 1, 0, 0 };
        private static int[] dColumn = { 0, 0, 1, -1 };

        public static void Testing()
        {
            int[,] grid = { { 1, 2, 3, 4 },
                   { 5, 6, 7, 8 },
                   { 9, 10, 11, 12 },
                   { 13, 14, 15, 16 } };

            Print(grid, 0, 0);
        }

        public static void Print(int[,] grid, int row, int col)
        {
            var vis = new bool[grid.GetLength(0), grid.GetLength(1)];
            var queue = new Queue<Pair>();
            
            queue.Enqueue(new Pair(row, col));
            vis[row, col] = true;

            while (queue.Count > 0)
            {
                // Process the pair
                var pair = queue.Dequeue();
                var x = pair.first;
                var y = pair.second;
                Console.Write($"{grid[x, y]}, ");

                GoThoughtAdjacents(queue, vis, x, y);
            }
        }

        public static void GoThoughtAdjacents(Queue<Pair> queue, bool[,] vis, int x, int y)
        {
            for (var i = 0; i < 4; i++)
            {
                var adjx = x + dRow[i];
                var adjy = y + dColumn[i];

                if (isValid(vis, adjx, adjy))
                {
                    queue.Enqueue(new Pair(adjx, adjy));
                    vis[adjx, adjy] = true;
                }
            }
        }

        public static bool isValid(bool[,] vis, int row, int col)
        {
            if (row < 0 || col < 0 || row >= vis.GetLength(0) || col >= vis.GetLength(1))
                return false;

            return !vis[row, col];
        }
    }
}
