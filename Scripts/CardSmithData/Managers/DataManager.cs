using CardSmithData.Dialog;
using GGC.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DataManager
{
    private static DataManager instance;
    public static DataManager Instance { get => instance ??= new(); }

    public string ProjectDir { get; set; } = String.Empty;
    public string DefaultsDir { get; set; } = "Defaults";
    public string DataDir { get; set; } = "Data";

    public string CharactersFile { get; set; } = "Characters.wad";
    public string ConditionsFile { get; set; } = "Conditions.wad";
    public string BaseStatsFile { get; set; } = "BaseStats.wad";
    public string ActionsFile { get; set; } = "Actions.wad";
    public string TagsFile { get; set; } = "Tags.wad";
    public string IDFile { get; set; } = "IDs.wad";

    public IDManager IDManager { get; set; } = new();
    public CharacterManager CharacterManager { get; set; } = new();
    public ConditionManager<Condition> ConditionManager { get; set; } = new();
    public TagManager<Tag> TagManager { get; set; } = new();
    public ActionManager ActionManager { get; set; } = new();
    public BaseStats BaseStats { get; set; } = new();

    public void Initialize()
    {
        if (ProjectDir == String.Empty)
        {
            throw new Exception("Empty project directory");
        } 
        else 
        {
            InitializeDefaults();
            InitializeData();
        }
    }

    private void InitializeData()
    {
        string path = $"{ProjectDir}/{DataDir}";

        Init:
        if (Directory.Exists(path))
        {
            Initialize($"{path}/{CharactersFile}", CharacterManager, CharacterManager);
            Initialize($"{path}/{ConditionsFile}", ConditionManager, ConditionManager);
            Initialize($"{path}/{ActionsFile}", ActionManager, ActionManager);
            Initialize($"{path}/{TagsFile}", TagManager, TagManager);
            Initialize($"{path}/{IDFile}", IDManager, IDManager);
        }
        else
        {
            Directory.CreateDirectory(path);
            goto Init;
        }
    }

    private void InitializeDefaults()
    {
        string path = $"{ProjectDir}/{DefaultsDir}";

        Init:
        if (Directory.Exists(path))
        {
            Initialize($"{path}/{BaseStatsFile}", BaseStats, BaseStats);
        }
        else
        {
            Directory.CreateDirectory(path);
            goto Init;
        }
    }

    private void Initialize(string path, ISavable savable, ILoadable loadable)
    {
        if (File.Exists(path))
        {
            Load(path, loadable);
        }
        else
        {
            Save(path, savable);
        }
    }

    public void Save(string path, ISavable savable)
    {
        using (FileStream stream = new(path, FileMode.OpenOrCreate))
        {
            BinaryWriter writer = new(stream);

            savable.Save(ref writer);

            writer.Dispose();
        }
    }

    public void Load(string path, ILoadable loadable)
    {
        using (FileStream stream = new(path, FileMode.OpenOrCreate))
        {
            BinaryReader reader = new(stream);

            loadable.Load(ref reader);

            reader.Dispose();
        }
    }

    public void SaveProject()
    {
        SaveIDs();
        SaveCharacters();
        SaveConditions();
        SaveBaseStats();
        SaveActions();
        SaveTags();
    }

    public void SaveIDs()
    {
        Save($"{ProjectDir}/{DataDir}/{IDFile}", IDManager);
    }

    public void SaveCharacters()
    {
        Save($"{ProjectDir}/{DataDir}/{CharactersFile}", CharacterManager);
        SaveIDs();
    }

    public void SaveConditions()
    {
        Save($"{ProjectDir}/{DataDir}/{ConditionsFile}", ConditionManager);
        SaveIDs();
    }

    public void SaveActions()
    {
        Save($"{ProjectDir}/{DataDir}/{ActionsFile}", ActionManager);
        SaveIDs();
    }

    public void SaveTags()
    {
        Save($"{ProjectDir}/{DataDir}/{TagsFile}", TagManager);
        SaveIDs();
    }

    public void SaveBaseStats()
    {
        Save($"{ProjectDir}/{DefaultsDir}/{BaseStatsFile}", BaseStats);
        SaveIDs();
    }

    public void SaveDialogTree(DialogTree tree)
    {
        if (!Directory.Exists(Globals.DIALOGTREE_DATAP))
        {
            Directory.CreateDirectory(Globals.DIALOGTREE_DATAP);
        }

        Save($"{Globals.DIALOGTREE_DATAP}/{tree.Name}.diag", tree);
    }
}
