using System.Text.RegularExpressions;
using TextCopy;
Regex formatter = new("}|(\"[^\"]+\"|\\S+) += +(\"[^\"]+\"|\\S+)|(\\S+|\"[^\"]+\")", RegexOptions.Compiled);
ClipboardService.SetText(FormatCode(ClipboardService.GetText()));
string FormatCode(string? str)
{
    if (str == null)
        return "";
    MatchCollection matches = formatter.Matches(str);

    string[] vs = matches.Select(m => m.Value).ToArray();
    int indent = 0;
    for (int i = 0; i < vs.Length; i++)
    {
        if (vs[i].EndsWith('{'))
        {
            vs[i] = vs[i].Prepend(indent, '\t');
            indent++;
        }
        else if (vs[i].EndsWith('}'))
        {
            indent--;
            if (indent < 0)
                throw new Exception();
            vs[i] = vs[i].Prepend(indent, '\t');
        }
        else
        {
            vs[i] = vs[i].Prepend(indent, '\t');
        }
    }

    return string.Join(Environment.NewLine, vs);
}
public static class str2
{
    public static string Prepend(this string sa, int amount, char c)
    {
        string s = "".PadLeft(amount, c);
        return s + sa;
    }
}