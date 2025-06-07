using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public CharacterSlot[] slots;
    public Transform slotPanel;
    public Transform floor;

    [Header("Select Character")]
    public CharacterDTO selectCharacter;
    public TextMeshProUGUI selectedName;
    public TextMeshProUGUI selectedValue;
    public GameObject selectedPrefab;

    void Start()
    {
        slots = new CharacterSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<CharacterSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }
        for (int i = 0; i < DataManager.Instance.playerData.characters.Count; i++)
        {
            slots[i].character = DataManager.Instance.playerData.characters[i];
            slots[i].slotName.text = slots[i].character.characterName;
            slots[i].characterPrefab = Resources.Load<GameObject>($"Character/{slots[i].character.characterName}");
        }
    }

    public void SelectCharacter(int index)
    {
        if (slots[index].character.characterName == "") return;
        if (selectedPrefab != null) Destroy(selectedPrefab);

        selectCharacter = slots[index].character;
        selectedName.text = selectCharacter.characterName;
        selectedValue.text = $"{selectCharacter.pow} \n\n{selectCharacter.def} \n\n{selectCharacter.health} \n\n{selectCharacter.level} \n\n{selectCharacter.exp} \n\n{selectCharacter.type}";

        Vector3 pos = new Vector3(floor.position.x, floor.position.y + 1, floor.position.z);
        selectedPrefab = Instantiate(slots[index].characterPrefab, pos, Quaternion.identity);
    }

    public void InventoryUpdate(CharacterDTO character)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].character.characterName == "")
            {
                slots[i].character = character;
                slots[i].slotName.text = character.characterName;
                slots[i].characterPrefab = Resources.Load<GameObject>($"Character/{slots[i].character.characterName}");
                break;
            }
        }
    }
}
