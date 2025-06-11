using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public CharacterManager minion;
    public CharacterDTO character;
    public string characterName;

    [Header("UIBar")]
    public GameObject hpBar;
    public Transform canvas;
    public StatusBar status;

    void Start()
    {
        // minion = GetComponent<CharacterManager>();
        // if (minion.thisEnemy) character = DataManager.Instance.characters.GetCharacterByName(characterName)?.Clone();
        // else character = DataManager.Instance.GetCharacterByName(characterName);
        canvas = GameObject.Find("Canvas").transform;

        GameObject ui = Instantiate(hpBar, canvas);
        ui.transform.SetAsFirstSibling();
        status = ui.GetComponent<StatusBar>();

        status.target = this.gameObject;
        status.minion = GetComponent<CharacterStat>();
        status.maxHP = character.health;
        status.UpdateHPBar();
    }



}
