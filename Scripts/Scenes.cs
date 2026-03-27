using GGC.Interfaces;
using System.Collections.Generic;

public enum TSCN
{
    Launcher,
    Main,
    BucketCreator,
    StatEditor,
    CharacterCreator,
    CharacterSelector,
    ConditionsCreator,
    TagCreator,
    DialogTreeEditor,
}

public class Scenes : IMap<TSCN, string>
{
    public Dictionary<TSCN, string> Map { get; set; } = new Dictionary<TSCN, string>()
    {
        { TSCN.Launcher, "launcher" },
        { TSCN.Main, "main_scene" },
        { TSCN.BucketCreator, "bucket_list" },
        { TSCN.StatEditor, "stat_editor" },
        { TSCN.CharacterCreator, "character_editor" },
        { TSCN.CharacterSelector, "character_selector" },
        { TSCN.ConditionsCreator, "conditions_creator" },
        { TSCN.TagCreator, "tag_creator" },
        { TSCN.DialogTreeEditor, "dialog_tree_editor" },
    };
}
