using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Melee,
    Range
}

[CreateAssetMenu(fileName = "New Character", menuName = "Character/Character Stats")]
[System.Serializable]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public int health;
    public int mana;
    public int pow;
    public int def;
    public int exp;
    public int level;
    public AttackType type;

}

[System.Serializable]
public class CharacterDTO
{
    public string characterName;
    public int health;
    public int mana;
    public int pow;
    public int def;
    public int exp;
    public int level;
    public AttackType type;

    public CharacterDTO() { }

    public CharacterDTO(CharacterData data)
    {
        characterName = data.characterName;
        health = data.health;
        mana = data.mana;
        pow = data.pow;
        def = data.def;
        exp = data.exp;
        level = data.level;
        type = data.type;
    }

    public CharacterDTO Clone()
    {
        return new CharacterDTO
        {
            characterName = this.characterName,
            health = this.health,
            mana = this.mana,
            pow = this.pow,
            def = this.def,
            exp = this.exp,
            level = this.level,
            type = this.type
        };
    }
}

[System.Serializable]
public class CharacterDTOList
{
    public List<CharacterDTO> characters;

    private Dictionary<string, CharacterDTO> characterDict;

    public void InitializeDictionary()
    {
        characterDict = new Dictionary<string, CharacterDTO>();
        foreach (var character in characters)
        {
            if (!characterDict.ContainsKey(character.characterName))
                characterDict.Add(character.characterName, character);
        }
    }

    public CharacterDTO GetCharacterByName(string name)
    {
        if (characterDict == null)
            InitializeDictionary();

        characterDict.TryGetValue(name, out var character);
        return character;
    }
}

