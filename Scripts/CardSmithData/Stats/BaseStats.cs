using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BaseStats : Stats, ICopy<Stats>
{
    public Stats Copy()
    {
        Stats toReturn = new Stats();

        foreach (string key in IntegerStats.Keys)
        {
            toReturn.IntegerStats[key] = IntegerStats[key];
        }

        foreach (string key in StringStats.Keys)
        {
            toReturn.StringStats[key] = StringStats[key];
        }

        foreach (string key in BooleanStats.Keys)
        {
            toReturn.BooleanStats[key] = BooleanStats[key];
        }

        return toReturn;
    }
}
