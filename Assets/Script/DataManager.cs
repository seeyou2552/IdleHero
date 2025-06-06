using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    string path;

    public static DataManager Instance { get; private set; }

    public CharacterDTOList characters;
    public Player playerData;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        path = "Assets/ExportedJson/character_data.json"; // 캐릭터 데이터
        if (!File.Exists(path))
        {
            ScriptableObjectExportJson();
        }

        characters = ScriptableObjectLoadJson();

        path = "Assets/ExportedJson/Player_data.json"; // 플레이어 데이터
        Debug.Log(path);
        if (!File.Exists(path))
        {
            Debug.Log("초기 플레이어 데이터 저장");
            playerData = CreateDefaultPlayerData();
            SaveJson(path);
        }

        LoadPlayer(path);
    }

    public void ScriptableObjectExportJson()
    {
        string folderPath = "Assets/ScrptableObject/Character";  // ScriptableObject 위치
        string exportPath = "Assets/ExportedJson/character_data.json";

        // ScriptableObject 타입 명시
        string[] guids = AssetDatabase.FindAssets("t:CharacterData", new[] { folderPath });

        List<CharacterDTO> allDataList = new List<CharacterDTO>();

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            CharacterData data = AssetDatabase.LoadAssetAtPath<CharacterData>(assetPath);

            if (data != null)
                allDataList.Add(new CharacterDTO(data));
        }

        // JsonUtility는 배열을 직접 직렬화 못함 => Wrapper 필요
        var wrapper = new CharacterDataWrapper { characters = allDataList.ToArray() };
        string json = JsonUtility.ToJson(wrapper, true);

        // 경로 폴더가 없으면 생성
        string exportFolder = Path.GetDirectoryName(exportPath);
        if (!Directory.Exists(exportFolder))
            Directory.CreateDirectory(exportFolder);

        File.WriteAllText(exportPath, json);
        AssetDatabase.Refresh();

        Debug.Log($"Exported {allDataList.Count} items to JSON at {exportPath}");
    }

    public CharacterDTOList ScriptableObjectLoadJson()
    {
        string jsonPath = "Assets/ExportedJson/character_data.json";
        string json = File.ReadAllText(jsonPath);
        CharacterDTOList dtoList = JsonUtility.FromJson<CharacterDTOList>(json);
        dtoList.InitializeDictionary();

        Debug.Log($"JSON에서 {dtoList.characters.Count}개의 캐릭터 로드 완료");
        return dtoList;
    }

    // JsonUtility는 배열을 감싸는 클래스가 필요함
    [System.Serializable]
    public class CharacterDataWrapper
    {
        public CharacterDTO[] characters;
    }

    public void SaveJson(string savePath)
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(savePath, json);
        Debug.Log("저장 완료: " + savePath);
    }

    public void LoadPlayer(string savePath)
    {
        string json = File.ReadAllText(savePath);
        playerData = JsonUtility.FromJson<Player>(json);
    }

    private Player CreateDefaultPlayerData()
    {
        return new Player
        {
            gold = 0,
            stage = 1,
            characters = new List<CharacterDTO>
            {
                new CharacterDTO
                {
                    characterName = "Hero",
                    health = 50,
                    mana = 20,
                    pow = 2,
                    def = 1,
                    exp = 0,
                    level = 1,
                    type = AttackType.Melee
                }
            }
        };
    }

    public CharacterDTO GetCharacterByName(string targetName)
    {
        return playerData.characters.Find(c => c.characterName == targetName);
    }
}

