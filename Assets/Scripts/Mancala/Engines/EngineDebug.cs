using System;
using System.Collections.Generic;

public class EngineDebug : Engine
{
    public EngineDebug() : base() { }

    protected override (List<Move>, int) Minimax(Table table, int depth)
    {
        return MinimaxDebug(table, depth, int.MinValue, int.MaxValue);
    }

    private (List<Move>, int) MinimaxDebug(Table table, int depth, int alpha, int beta)
    {
        if (depth == 0 || table.IsGameOver())
        {
            return (new List<Move>(), Evaluate(table));
        }

        List<Move> bestPath = new List<Move>();
        int bestEval;

        if (table.CurrentPlayer == 0)
        {
            bestEval = int.MinValue;
            foreach (int move in GetValidMoves(table))
            {
                Table childTable = new Table(table);
                childTable.Move(move);

                (List<Move> childPath, int eval) = MinimaxDebug(childTable, depth - 1, alpha, beta);

                if (eval > bestEval)
                {
                    bestEval = eval;
                    bestPath = new List<Move> { new Move(move, table.CurrentPlayer, depth) };
                    bestPath.AddRange(childPath);
                }

                alpha = Math.Max(alpha, eval);
                if (beta <= alpha)
                {
                    break;
                }
            }
        }
        else
        {
            bestEval = int.MaxValue;
            foreach (int move in GetValidMoves(table))
            {
                Table childTable = new Table(table);
                childTable.Move(move);

                (List<Move> childPath, int eval) = MinimaxDebug(childTable, depth - 1, alpha, beta);

                if (eval < bestEval)
                {
                    bestEval = eval;
                    bestPath = new List<Move> { new Move(move, table.CurrentPlayer, depth) };
                    bestPath.AddRange(childPath);
                }

                beta = Math.Min(beta, eval);
                if (beta <= alpha)
                {
                    break;
                }
            }
        }
        return (bestPath, bestEval);
    }
}