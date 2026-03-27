using GGC.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CharacterManager : ISavable, ILoadable
{
    public Dictionary<int, ICharacter> Characters { get; set; } = [];

    public long AddCharacter(ICharacter character, int id = -1)
    {
        if (id == -1)
        {
            character.Id = IDManager.GetID();
            Characters.Add(character.Id, character);
            
            return character.Id;
        }
        else
        {
            Characters[id] = character;

            return id;
        }
    }

    public ICharacter GetCharacter(int id)
    {
        return Characters[id];
    }

    public void Save(ref BinaryWriter writer)
    {
        writer.Write(Characters.Count);

        foreach (int key in Characters.Keys)
        {
            Characters[key].Save(ref writer);
        }
    }

    public void Load(ref BinaryReader reader)
    {
        int len = reader.ReadInt32();
        Character character;

        for (int i = 0; i < len; i++)
        {
            character = new Character();
            character.Load(ref reader);

            Characters.Add(character.Id, character);
        }
    }
}