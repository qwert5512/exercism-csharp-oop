using System;
using System.Collections.Generic;
using System.Linq;

public static class SaddlePoints
{
    public static IEnumerable<(int, int)> Calculate(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        if (rows == 0 || cols == 0)
            yield break;

        var rowMax = new int[rows];
        var colMin = new int[cols];

        for (int r = 0; r < rows; r++)
        {
            int max = int.MinValue;
            for (int c = 0; c < cols; c++)
                if (matrix[r, c] > max) max = matrix[r, c];
            rowMax[r] = max;
        }

        for (int c = 0; c < cols; c++)
        {
            int min = int.MaxValue;
            for (int r = 0; r < rows; r++)
                if (matrix[r, c] < min) min = matrix[r, c];
            colMin[c] = min;
        }

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (matrix[r, c] == rowMax[r] && matrix[r, c] == colMin[c])
                    yield return (r + 1, c + 1);
            }
        }
    }
}