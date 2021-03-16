using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnRange = 9;

    private int enemyCount;
    private int enemyWave = 2;

    public GameObject powerUpPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(enemyWave);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            enemyWave++;
            SpawnEnemyWave(enemyWave);
            int numberOfPowerUp = Random.Range(0, 3);
            for (int i = 0; i < numberOfPowerUp; i++)
            {
                Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
            }
        }
    }

    /// <summary>
    /// Genera una posición aleatoria dentro de la zona de juego
    /// </summary>
    /// <returns>Devuelve una posición aleatoria dentro de la zona de juego</returns>
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
    
    /// <summary>
    /// Método que genera un número determinado de enemigos en pantalla
    /// </summary>
    /// <param name="numberOfEnemies">Númer de enemigos a crear</param>
    private void SpawnEnemyWave(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}

