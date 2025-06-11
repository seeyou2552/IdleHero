using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    string playerDataPath = "Assets/ExportedJson/Player_data.json";
    public static StageManager Instance { get; private set; }
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI reasonText;
    public int stage;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        stage = GameManager.Instance.stageData;
    }

    public void ToLobbyButton(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void GameResult(String result)
    {
        resultPanel.SetActive(true);
        if (result == "Clear")
        {
            resultText.text = "Game Clear";
            reasonText.text = $"승리하여 {GameManager.Instance.enemy.gold} G를 획득하였습니다.";
            DataManager.Instance.playerData.gold += GameManager.Instance.enemy.gold;
            if (DataManager.Instance.playerData.stage == stage) DataManager.Instance.playerData.stage += 1;
            DataManager.Instance.SaveJson(playerDataPath);
        }
        else if (result == "Over")
        {
            resultText.text = "Game Over";
            reasonText.text = $"모든 아군이 기절하여 패배하였습니다.";
        }
        else Debug.Log("잘못된 값");

        Time.timeScale = 0f;
    }
}
