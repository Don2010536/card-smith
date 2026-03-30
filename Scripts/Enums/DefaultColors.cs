using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum DefaultColors
{
    Red,
    Green,
    Yellow,
    Purple,
    White
}

public static class Colors
{
    public static Dictionary<DefaultColors, string> ColorMap = new Dictionary<DefaultColors, string>()
    {
        { DefaultColors.Red, "#da3e44" },
        { DefaultColors.Green, "#45a366" },
        { DefaultColors.Yellow, "#ffc04e" },
        { DefaultColors.Purple, "#7289da" },
        { DefaultColors.White, "#FFFFFF" },
    };
}