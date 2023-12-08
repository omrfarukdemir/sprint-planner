namespace SprintPlanner.Infrastructure.Extensions;

public static class ListExtensions
{
    public static double[][] ConvertToDoubleArray(this List<List<int>> intMatris)
    {
        var rowCount = intMatris.Count;
        var colCount = intMatris[0].Count;

        var doubleMatris = new double[rowCount][];

        for (var i = 0; i < rowCount; i++)
        {
            doubleMatris[i] = new double[colCount];

            for (var j = 0; j < colCount; j++)
            {
                doubleMatris[i][j] = (double)intMatris[i][j];
            }
        }

        return doubleMatris;
    }
}