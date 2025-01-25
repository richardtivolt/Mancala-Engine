public struct Move
{
    public int Index;
    public int Player;
    public int Depth;

    public Move(int index, int player, int depth)
    {
        Index = index;
        Player = player;
        Depth = depth;
    }

    public override string ToString()
    {
        return $"{Index}{(Player == 0 ? "+" : "-")}";
    }
}