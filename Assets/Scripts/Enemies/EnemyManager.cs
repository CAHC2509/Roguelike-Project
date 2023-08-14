using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private EnemyData[] enemyTypes;
    [SerializeField]
    private Transform[] spawnPoints;

    private void Start()
    {
        // Implementa la lógica para instanciar enemigos basados en enemyTypes y spawnPoints
    }

    // Implementa eventos de juego, como cuando un enemigo es derrotado
    // Puedes utilizar UnityEvent para gestionar estas interacciones
}