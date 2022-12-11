using System.Text.RegularExpressions;

namespace AdventOfCode;

// https://github.com/encse/adventofcode/blob/master/Lib/Ocr.cs

/*
Copyright (c) 2019 David Nemeth Cs.

The license applies to all source code and configuration incluced in
the repository. It doesn't apply to advent of code problem statements 
and input files. For the license conditions of those please contact the 
original author at https://adventofcode.com.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

static class OcrExtension
{
    public static OcrString Ocr(this string st)
    {
        return new OcrString(st);
    }
}

public record OcrString(string St)
{
    public override string ToString()
    {
        var lines = St.Split("\n")
            .SkipWhile(string.IsNullOrWhiteSpace)
            .TakeWhile(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        while (lines.All(line => line.StartsWith(" ")))
            lines = GetRect(lines, 1, 0, lines[0].Length - 1, lines.Length).Split("\n");

        while (lines.All(line => line.EndsWith(" ")))
            lines = GetRect(lines, 0, 0, lines[0].Length - 1, lines.Length).Split("\n");

        var width = lines[0].Length;
        var height = lines.Length;

        var smallAlphabet = StripMargin(@"
        | A    B    C    E    F    G    H    I    J    K    L    O    P    R    S    U    Y    Z    
        |  ##  ###   ##  #### ####  ##  #  #  ###   ## #  # #     ##  ###  ###   ### #  # #   ##### 
        | #  # #  # #  # #    #    #  # #  #   #     # # #  #    #  # #  # #  # #    #  # #   #   # 
        | #  # ###  #    ###  ###  #    ####   #     # ##   #    #  # #  # #  # #    #  #  # #   #  
        | #### #  # #    #    #    # ## #  #   #     # # #  #    #  # ###  ###   ##  #  #   #   #   
        | #  # #  # #  # #    #    #  # #  #   #  #  # # #  #    #  # #    # #     # #  #   #  #    
        | #  # ###   ##  #### #     ### #  #  ###  ##  #  # ####  ##  #    #  # ###   ##    #  #### 
        ");

        var largeAlphabet = StripMargin(@"
        | A       B       C       E       F       G       H       J       K       L       N       P       R       X       Z
        |   ##    #####    ####   ######  ######   ####   #    #     ###  #    #  #       #    #  #####   #####   #    #  ######  
        |  #  #   #    #  #    #  #       #       #    #  #    #      #   #   #   #       ##   #  #    #  #    #  #    #       #  
        | #    #  #    #  #       #       #       #       #    #      #   #  #    #       ##   #  #    #  #    #   #  #        #  
        | #    #  #    #  #       #       #       #       #    #      #   # #     #       # #  #  #    #  #    #   #  #       #   
        | #    #  #####   #       #####   #####   #       ######      #   ##      #       # #  #  #####   #####     ##       #    
        | ######  #    #  #       #       #       #  ###  #    #      #   ##      #       #  # #  #       #  #      ##      #     
        | #    #  #    #  #       #       #       #    #  #    #      #   # #     #       #  # #  #       #   #    #  #    #      
        | #    #  #    #  #       #       #       #    #  #    #  #   #   #  #    #       #   ##  #       #   #    #  #   #       
        | #    #  #    #  #    #  #       #       #   ##  #    #  #   #   #   #   #       #   ##  #       #    #  #    #  #       
        | #    #  #####    ####   ######  #        ### #  #    #   ###    #    #  ######  #    #  #       #    #  #    #  ######  
        ");

        var charMap =
            lines.Length == smallAlphabet.Length - 1 ? smallAlphabet :
            lines.Length == largeAlphabet.Length - 1 ? largeAlphabet :
            throw new Exception("Could not find alphabet");

        var charWidth = charMap == smallAlphabet ? 5 : 8;
        var charHeight = charMap == smallAlphabet ? 6 : 10;
        var res = "";
        for (var i = 0; i < width; i += charWidth)
        {
            res += Detect(lines, i, charWidth, charHeight, charMap);
        }

        return res;
    }

    public static string[] StripMargin(string st) => (
        from line in Regex.Split(st, "\r?\n")
        where Regex.IsMatch(line, @"^\s*\| ")
        select Regex.Replace(line, @"^\s* \| ", "")
    ).ToArray();

    public string Detect(string[] text, int icolLetter, int charWidth, int charHeight, string[] charMap)
    {
        var textRect = GetRect(text, icolLetter, 0, charWidth, charHeight);

        for (var icol = 0; icol < charMap[0].Length; icol += charWidth)
        {
            var ch = charMap[0][icol].ToString();
            var charPattern = GetRect(charMap, icol, 1, charWidth, charHeight);
            var found = Enumerable.Range(0, charPattern.Length).All(i =>
            {
                var textWhiteSpace = " .".Contains(textRect[i]);
                var charWhiteSpace = " .".Contains(charPattern[i]);
                return textWhiteSpace == charWhiteSpace;
            });

            if (found)
            {
                return ch;
            }
        }

        throw new Exception($"Unrecognized letter: \n{textRect}\n");
    }

    static string GetRect(string[] st, int icol0, int irow0, int ccol, int crow)
    {
        var res = "";
        for (var irow = irow0; irow < irow0 + crow; irow++)
        {
            for (var icol = icol0; icol < icol0 + ccol; icol++)
            {
                var ch = irow < st.Length && icol < st[irow].Length ? st[irow][icol] : ' ';
                res += ch;
            }

            if (irow + 1 != irow0 + crow)
            {
                res += "\n";
            }
        }

        return res;
    }
}