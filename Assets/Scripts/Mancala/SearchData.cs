using System.Collections.Generic;
using System.Text;

public struct SearchData
{
    public List<Move> BestPath;
    public int Evaluation;
    public int PositionsEvaluated;
    public long TimeSpent;

    public SearchData(List<Move> bestPath, int evaluation, int positionsEvaluated, long timeSpent)
    {
        BestPath = bestPath;
        Evaluation = evaluation;
        PositionsEvaluated = positionsEvaluated;
        TimeSpent = timeSpent;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Best Move");
        sb.Append(BestPath.Count > 1 ? "s:" : ":");
        for (int i = 0; i < BestPath.Count; i++)
        {
            sb.Append(" ");
            sb.Append(BestPath[i].ToString());
        }
        sb.Append(" [");
        sb.Append(Evaluation);
        sb.Append("]");
        sb.Append("\nPositions Evaluated: ");
        sb.Append(PositionsEvaluated);
        sb.Append("\nTime Spent: ");
        sb.Append(TimeSpent / 1000f);
        sb.Append("s");
        return sb.ToString();
    }
}