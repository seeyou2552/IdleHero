using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public CharacterDTO character;
    public Button button;
    public TextMeshProUGUI slotName;
    public UIInventory inventory;
    public GameObject characterPrefab;
    public int index;

    public void OnClickButton()
    {
        inventory.SelectCharacter(index);
    }

}
