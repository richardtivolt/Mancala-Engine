using System;
using System.Collections.Generic;

public class EngineUltra : Engine
{
    public EngineUltra() : base() { }

    protected override (List<Move>, int) Minimax(Table table, int depth)
    {
        (int move, int eval) = MinimaxUltra(table.Data, depth, int.MinValue, int.MaxValue, table.CurrentPlayer);
        List<Move> bestPath = new List<Move>() { new Move(move, table.CurrentPlayer, depth) };

        return (bestPath, eval);
    }

    private (int, int) MinimaxUltra(int[] table, int depth, int alpha, int beta, int currentPlayer)
    {
        if (depth == 0 || IsGameOver(table))
        {
            return (-1, Evaluate(table));
        }

        int bestEval;
        int bestMove = -1;

        int[] validMoves = GetValidMoves(table, currentPlayer);
        if (currentPlayer == 0)
        {
            bestEval = int.MinValue;
            foreach (int move in validMoves)
            {
                int[] childTable = new int[table.Length];
                for (int i = 0; i < childTable.Length; i++)
                {
                    childTable[i] = table[i];
                }
                int newCurrentPlayer = currentPlayer;
                Move(childTable, move, ref newCurrentPlayer);

                int eval = MinimaxUltra(childTable, depth - 1, alpha, beta, newCurrentPlayer).Item2;

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
                int[] childTable = new int[table.Length];
                for (int i = 0; i < childTable.Length; i++)
                {
                    childTable[i] = table[i];
                }
                int newCurrentPlayer = currentPlayer;
                Move(childTable, move, ref newCurrentPlayer);

                int eval = MinimaxUltra(childTable, depth - 1, alpha, beta, newCurrentPlayer).Item2;

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

    private bool Move(int[] table, int index, ref int currentPlayer)
    {
        if (!IsValidMove(table, index, currentPlayer))
        {
            return false;
        }
        int pickIndex = index + currentPlayer * 7;
        int placeCount = table[pickIndex];

        int tileIndex = -1;
        table[pickIndex] = 0;
        for (int i = 0; i < placeCount; i++)
        {
            tileIndex = (pickIndex + i + 1) % 14;
            if (tileIndex == 6 + (1 - currentPlayer) * 7)
            {
                placeCount++;
                continue;
            }
            table[tileIndex]++;
        }

        int currentPlayerScoreTile = GetPlayerScoreTile(currentPlayer);
        if (tileIndex == currentPlayerScoreTile)
        {
            if (!HasPlayerValidMove(table, currentPlayer))
            {
                currentPlayer = 1 - currentPlayer;
            }
            return true;
        }

        if (IsField(tileIndex) && table[tileIndex] == 1)
        {
            int stealTile = 12 - tileIndex;
            table[currentPlayerScoreTile] += table[stealTile];
            table[stealTile] = 0;
        }

        if (HasPlayerValidMove(table, 1 - currentPlayer))
        {
            currentPlayer = 1 - currentPlayer;
        }
        return true;
    }

    private bool IsGameOver(int[] table)
    {
        return !HasPlayerValidMove(table, 0) && !HasPlayerValidMove(table, 1);
    }

    private bool HasPlayerValidMove(int[] table, int player)
    {
        for (int i = 0; i < 6; i++)
        {
            if (table[i + player * 7] > 0)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsValidMove(int[] table, int index, int currentPlayer)
    {
        if (index < 0 || index > 6)
        {
            return false;
        }
        if (table[index + currentPlayer * 7] == 0)
        {
            return false;
        }
        return true;
    }

    private int GetPlayerScoreTile(int player)
    {
        return 6 + player * 7;
    }

    private bool IsField(int index)
    {
        return index != 6 && index != 13;
    }

    private int Evaluate(int[] table)
    {
        _positionsEvaluated++;
        return table[6] - table[13];
    }

    private int[] GetValidMoves(int[] table, int currentPlayer)
    {
        int validMoveCount = 0;
        for (int i = 0; i < 6; i++)
        {
            if (IsValidMove(table, i, currentPlayer))
            {
                validMoveCount++;
            }
        }
        int[] validMoves = new int[validMoveCount];

        int index = 0;
        for (int i = 0; i < 6; i++)
        {
            if (IsValidMove(table, i, currentPlayer))
            {
                validMoves[index] = i;
                index++;
            }
        }
        return validMoves;
    }
}