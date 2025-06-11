using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject[] target;
    public int index=0;

    void Awake()
    {
        GameManager.Instance.cameraObj = gameObject.GetComponent<CameraFollow>();
        target = new GameObject[DataManager.Instance.playerData.characters.Count];
    }

    void Update()
    {
        if (target[index] != null) gameObject.transform.position = new Vector3(target[index].transform.position.x + 10, gameObject.transform.position.y, target[index].transform.position.z);
    }

    public void ChangeTarget()
    {
        index++;
        if (target.Length == index) index = 0;
    }
}
