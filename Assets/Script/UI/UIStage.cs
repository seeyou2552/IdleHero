using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStage : MonoBehaviour
{
    public void InBattle(int stage)
    {
        GameManager.Instance.OnStage(stage);
    }
}
