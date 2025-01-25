using System;
using System.Collections.Generic;

public class EngineFast : Engine
{
    public EngineFast() : base() { }

    protected override (List<Move>, int) Minimax(Table table, int depth)
    {
        (int move, int eval) = MinimaxFast(table, depth, int.MinValue, int.MaxValue);
        List<Move> bestPath = new List<Move>() { new Move(move, table.CurrentPlayer, depth) };

        return (bestPath, eval);
    }

    private (int, int) MinimaxFast(Table table, int depth, int alpha, int beta)
    {
        if (depth == 0 || table.IsGameOver())
        {
            return (-1, Evaluate(table));
        }

        int bestEval;
        int bestMove = -1;

        int[] validMoves = GetValidMoves(table);
        if (table.CurrentPlayer == 0)
        {
            bestEval = int.MinValue;
            foreach (int move in validMoves)
            {
                Table childTable = new Table(table);
                childTable.Move(move);

                int eval = MinimaxFast(childTable, depth - 1, alpha, beta).Item2;

                if (eval > bestEval)
                {
                    bestEval = eval;
                    bestMove = move;
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
            foreach (int move in validMoves)
            {
                Table childTable = new Table(table);
                childTable.Move(move);

                int eval = MinimaxFast(childTable, depth - 1, alpha, beta).Item2;

                if (eval < bestEval)
                {
                    bestEval = eval;
                    bestMove = move;
                }

                beta = Math.Min(beta, eval);
                if (beta <= alpha)
                {
                    break;
                }
            }
        }
        return (bestMove, bestEval);
    }
}