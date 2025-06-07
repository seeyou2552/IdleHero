using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckDraw : MonoBehaviour
{
    public UIInventory inventory;
    string path = "Assets/ExportedJson/Player_data.json";
    CharacterDTO drawCharacter;
    public void OnLuckDraw()
    {
        drawCharacter = LandomCharacterDraw();

        if (DataManager.Instance.GetCharacterByName(drawCharacter.characterName) == null) // 캐릭터를 가지고 있지 않으면 추가
        {
            DataManager.Instance.playerData.characters.Add(drawCharacter);
            inventory.InventoryUpdate(drawCharacter);
        }
        else
        {
            CharacterDTO temp;
            temp = DataManager.Instance.GetCharacterByName(drawCharacter.characterName);
            temp.level += 1;
        }

        DataManager.Instance.SaveJson(path);

    }

    CharacterDTO LandomCharacterDraw()
    {
        int index = Random.Range(0, DataManager.Instance.characters.characters.Count);
        Debug.Log(DataManager.Instance.characters.characters[index].characterName + "획득");

        return DataManager.Instance.characters.characters[index];

    }





    


}
