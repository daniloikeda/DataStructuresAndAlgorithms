using DataStructuresAndAlgorithms.BreadthFirstSearch;

namespace DataStructuresAndAlgorithms.DepthFirstSearch
{
    public static class DfsAlgorithm
    {
        private static int[] dRow = { 0, 1, 0, -1 };
        private static int[] dColumn = { -1, 0, 1, 0 };

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
            // Visited array
            var vis = new bool[grid.GetLength(0), grid.GetLength(1)];
            var stack = new Stack<Pair>();
            stack.Push(new Pair(row, col));            

            // While this stack is not empty
            // Pop the element to processed
            // Process the element by stacking its valid adjacents elements and marking them as visited
            while(stack.Count > 0)
            {
                var node = stack.Pop();
                var x = node.first;
                var y = node.second;

                if (!isValid(vis, x, y))
                {
                    continue;
                }

                Console.WriteLine($"{grid[x, y]} ");

                for(int i = 0; i < 4; i++)
                {
                    var adjx = x + dRow[i];
                    var adjy = y + dColumn[i];

                    if (isValid(vis, adjx, adjy))
                    {
                        stack.Push(new Pair(adjx, adjy));
                    }
                }

                vis[x, y] = true;
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
