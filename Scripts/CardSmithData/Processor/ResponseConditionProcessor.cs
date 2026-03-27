using CardSmithData.Dialog.Responses;
using System;
using System.Collections.Generic;

namespace CardSmithData.Processor
{
    public static class ResponseConditionProcessor
    {
        public static bool ProcessConditions(IResponse response, ICharacter characterToCheck)
        {
            if (response.LogicalOperator == LogicalOperators.And)
            {
                return ProcessAnd(response.Conditions, characterToCheck);
            }
            else
            {
                return ProcessOr(response.Conditions, characterToCheck);
            }
        }

        public static bool ProcessAnd(IEnumerable<IResponseCondition> conditions, ICharacter characterToCheck)
        {
            foreach (IResponseCondition condition in conditions)
            {
                if (!ProcessStatement(condition, characterToCheck))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ProcessOr(IEnumerable<IResponseCondition> conditions, ICharacter characterToCheck)
        {
            foreach (IResponseCondition condition in conditions)
            {
                if (ProcessStatement(condition, characterToCheck))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ProcessStatement(IResponseCondition condition, ICharacter characterToCheck)
        {
            string statToCompare = GetStat(condition, characterToCheck);

            switch (condition.StatType)
            {
                case StatTypes.Int:
                    return CompareInt(condition.ComparissonType, Convert.ToInt32(statToCompare), Convert.ToInt32(condition.ComparrisonValue));
                default:
                    return CompareString(statToCompare, condition.ComparrisonValue);
            }
        }

        private static string GetStat(IResponseCondition condition, ICharacter characterToCheck)
        {
            if (condition.CustomStatUsed)
            {
                switch (condition.StatType)
                {
                    case StatTypes.Int:
                        return characterToCheck.CustomStats.IntegerStats[condition.StatKey].ToString();
                    case StatTypes.String:
                        return characterToCheck.CustomStats.StringStats[condition.StatKey];
                    case StatTypes.Bool:
                        return characterToCheck.CustomStats.BooleanStats[condition.StatKey] ? "1" : "0";
                }
            }
            else
            {
                switch (condition.StatKey)
                {
                    case "Constitution":
                        return characterToCheck.Constitution.ToString();
                    case "Name":
                        return characterToCheck.Name;
                    case "Draws":
                        return characterToCheck.Draws.ToString();
                    case "MaxHandSize":
                        return characterToCheck.MaxHandSize.ToString();
                }
            }

            return "NULL_STRING_CONVERSION";
        }

        private static bool CompareInt(ComparissonTypes comparisson, int x, int y)
        {
            switch (comparisson)
            {
                case ComparissonTypes.GreaterThanOrEqual:
                    return x >= y;
                case ComparissonTypes.GreaterThan:
                    return x >= y;
                case ComparissonTypes.LessThanOrEqual:
                    return x <= y;
                case ComparissonTypes.LessThan:
                    return x < y;
                case ComparissonTypes.Equal:
                    return x == y;
                default:
                    return false;
            }
        }

        private static bool CompareString(string x, string y)
        {
            return x == y;
        }
    }
}