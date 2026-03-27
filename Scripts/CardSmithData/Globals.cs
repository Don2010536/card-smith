using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Globals
{
    public static string ProjectPath = "SRC";
    public static string ProjectName = "";

    public static string CombinedPath => $"{ProjectPath}/{ProjectName}";

    //Paths
    public static string CHARACTERSP => $"{CombinedPath}/Characters";
    public static string DIALOGTREEP => $"{CombinedPath}/Dialog/Trees";
    public const string SCENESP = "res://Scenes";

    //Files
    public static string CARDSF => $"{CombinedPath}/Catalog.bin";
    public static string BUCKETSF => $"{CombinedPath}/Buckets.bin";
    public static string STATSF => $"{CombinedPath}/Stats.bin";
}