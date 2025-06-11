using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Image hpBar;
    public GameObject target;
    public Vector3 offset;
    public RectTransform uiElement; // 체력바 UI
    public CharacterStat minion;
    public TextMeshProUGUI hpText;
    public int maxHP;

    void Update()
    {
        if (target == null || uiElement == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position + offset);
        uiElement.position = screenPos;
    }

    public void UpdateHPBar()
    {
        float ratio = (float)minion.character.health / maxHP;
        hpBar.fillAmount = ratio;
        if (minion.character.health < 0) hpText.text = 0.ToString();
        else hpText.text = minion.character.health.ToString();
    }
}
