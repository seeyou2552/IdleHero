using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Battle Pos")]
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float enemyPos; // z값 양수

    [Header("Battle")]
    public StageData enemy;
    public GameObject minion;
    public CameraFollow cameraObj;
    public int enemyCount;
    public int playerCount;
    public int stageData;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnStage(int stage)
    {
        if (stage > DataManager.Instance.playerData.stage)
        {
            Debug.Log("진입 불가");
            return;
        }

        enemy = Resources.Load<StageData>($"Stage/Stage{stage}");

        SceneManager.LoadScene("BattleScene");
        Time.timeScale = 1f;

        Invoke("minionSetting", 1f);
        stageData = stage;

    }

    void minionSetting()
    {
        for (int i = 0; i < DataManager.Instance.playerData.characters.Count; i++) // 플레이어
        {
            string name = DataManager.Instance.playerData.characters[i].characterName;
            Vector3 pos = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
            minion = Instantiate(Resources.Load<GameObject>($"Character/{name}"), pos, Quaternion.identity);
            minion.GetComponent<CharacterManager>().enabled = true;
            minion.GetComponent<CharacterStat>().enabled = true;
            minion.GetComponent<CharacterStat>().character = DataManager.Instance.playerData.characters[i].Clone();
            Transform child = minion.transform.Find("Model");
            child.tag = "Player";
            cameraObj.target[i] = minion;


        }
        playerCount = DataManager.Instance.playerData.characters.Count;

        for (int i = 0; i < enemy.enemys.Length; i++) // 적
        {
            string name = enemy.enemys[i].characterName;
            Vector3 pos = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ + enemyPos, maxZ + enemyPos));
            minion = Instantiate(Resources.Load<GameObject>($"Character/{name}"), pos, Quaternion.Euler(0, 180, 0));
            minion.name = $"Enemy{i}";
            minion.GetComponent<CharacterManager>().enabled = true;
            minion.GetComponent<CharacterManager>().thisEnemy = true;
            minion.GetComponent<CharacterStat>().enabled = true;
            minion.GetComponent<CharacterStat>().character = enemy.enemys[i].Clone();
            Transform child = minion.transform.Find("Model");
            child.tag = "Enemy";
        }
        enemyCount = enemy.enemys.Length;
    }




}
