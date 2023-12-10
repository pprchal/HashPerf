class Item
{
    readonly string Pattern;
    public Item(string pattern)
    {
        Pattern = pattern;
    }

    bool IsMatch(string line)
    {
        return line == Pattern;
    }
}