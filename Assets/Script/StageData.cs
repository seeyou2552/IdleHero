using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "Stage/Stage Stats")]
[System.Serializable]
public class StageData : ScriptableObject
{
    public CharacterDTO[] enemys;
    public int gold;

    public StageData Clone()
    {
        StageData clone = CreateInstance<StageData>();
        clone.enemys = new CharacterDTO[enemys.Length];

        for (int i = 0; i < enemys.Length; i++)
        {
            clone.enemys[i] = enemys[i].Clone();
        }

        return clone;
    }
}
