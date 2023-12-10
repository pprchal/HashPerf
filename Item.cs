class Item
{
    readonly string Pattern;
    public Item(string pattern)
    {
        Pattern = pattern;
    }

    public bool IsMatch(string line)
    {
        return line == Pattern;
    }
}