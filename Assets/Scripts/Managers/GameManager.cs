using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform playerSpawnPoint;

    void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        GameObject playerObject = Instantiate(playerPrefab, playerSpawnPoint.position, playerPrefab.transform.rotation);
    }
}
