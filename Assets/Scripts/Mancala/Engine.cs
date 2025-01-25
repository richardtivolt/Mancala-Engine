using System.Collections.Generic;
using System.Diagnostics;

public abstract class Engine
{
    protected int _positionsEvaluated;

    protected Engine()
    {
        _positionsEvaluated = 0;
    }

    public SearchData CalculateMove(Table table, int depth)
    {
        _positionsEvaluated = 0;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        (List<Move> bestPath, int eval) = Minimax(table, depth);

        stopwatch.Stop();
        return new SearchData(bestPath, eval, _positionsEvaluated, stopwatch.ElapsedMilliseconds);
    }

    protected abstract (List<Move>, int) Minimax(Table table, int depth);

    protected int Evaluate(Table table)
    {
        _positionsEvaluated++;
        return table.Data[6] - table.Data[13];
    }

    protected int[] GetValidMoves(Table table)
    {
        int validMoveCount = 0;
        for (int i = 0; i < 6; i++)
        {
            if (table.IsValidMove(i))
            {
                validMoveCount++;
            }
        }
        int[] validMoves = new int[validMoveCount];

        int index = 0;
        for (int i = 0; i < 6; i++)
        {
            if (table.IsValidMove(i))
            {
                validMoves[index] = i;
                index++;
            }
        }
        return validMoves;
    }
}