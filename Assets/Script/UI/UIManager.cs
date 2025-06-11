using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIInventory inventory;
    public LuckDraw draw;
    public GameObject activeObj;
    public TextMeshProUGUI goldText;
    // string playerDataPath = "Assets/ExportedJson/Player_data.json";

    void Start()
    {
        goldText.text = DataManager.Instance.playerData.gold.ToString() + " G";
    }

    public void ToggleMenu(GameObject menu)
    {
        if (inventory.selectedPrefab != null)
        {
            Destroy(inventory.selectedPrefab);
            inventory.selectedName.text = "";
            inventory.selectedValue.text = "";
        }
        else if (draw.getPrefab != null)
        {
            Destroy(draw.getPrefab);
            draw.getNameText.text = "";
            draw.getStatusText.text = "";
        }

        if (activeObj == menu && activeObj.activeSelf)
        {
            activeObj.SetActive(false);
            return;
        }
        else if (activeObj != null) activeObj.SetActive(false);

        activeObj = menu;
        activeObj.SetActive(true);

    }

    public void UseGold(int pay)
    {
        DataManager.Instance.playerData.gold -= pay;
        // DataManager.Instance.SaveJson(playerDataPath);
        goldText.text = DataManager.Instance.playerData.gold.ToString() + " G";
    }
}
