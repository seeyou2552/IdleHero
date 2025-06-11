using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LuckDraw : MonoBehaviour
{
    public UIInventory inventory;
    public UIManager uiManager;
    string playerDataPath = "Assets/ExportedJson/Player_data.json";
    CharacterDTO drawCharacter;
    public Transform floor;
    public int normalPay;

    [Header("Get Character")]
    public GameObject getPrefab;
    public TextMeshProUGUI getNameText;
    public TextMeshProUGUI getStatusText;
    CharacterDTO temp;

    public void OnLuckDraw() // 랜덤 뽑기
    {
        if (DataManager.Instance.playerData.gold < normalPay)
        {
            Debug.Log("골드 부족");
            return;
        }

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
            CharacterLevelUp(temp);
        }

        uiManager.UseGold(normalPay);
        DataManager.Instance.SaveJson(playerDataPath);

        if (getPrefab != null) Destroy(getPrefab);

        getNameText.text = drawCharacter.characterName;

        getStatusText.text = drawCharacter.characterName + "를(을) 획득하였습니다!";


        Vector3 pos = new Vector3(floor.position.x, floor.position.y + 1, floor.position.z);
        getPrefab = Instantiate(Resources.Load<GameObject>($"Character/{drawCharacter.characterName}"), pos, Quaternion.identity);

    }

    CharacterDTO LandomCharacterDraw() // 데이터 랜덤 추출
    {
        int index = Random.Range(0, DataManager.Instance.characters.characters.Count);
        Debug.Log(DataManager.Instance.characters.characters[index].characterName + "획득");

        return DataManager.Instance.characters.characters[index];

    }

    public void CharacterLevelUp(CharacterDTO character)
    {
        character.level += 1;
        character.pow += 1;
        character.def += 1;
        character.health += 2;
    }


}
