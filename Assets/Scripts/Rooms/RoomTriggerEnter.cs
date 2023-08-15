using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerEnter : MonoBehaviour
{
    public RoomManager roomManager;

    private void Start()
    {
        roomManager = FindAnyObjectByType<RoomManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            roomManager.StartNextWave();
            gameObject.SetActive(false);
        }
    }
}
