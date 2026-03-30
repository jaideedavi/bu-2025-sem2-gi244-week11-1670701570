using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public Wave[] waves;

    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    void Start()
    {
         Debug.Log("Total Waves: " + waves.Length);

    if (waves.Length > 0)
    {
        Debug.Log("Wave 1 enemy count: " + waves[0].totalSpawnEnemies);
    }

    StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            Wave wave = waves[i];

            Debug.Log("Start Wave " + (i + 1));

           
            yield return new WaitForSeconds(wave.delayStart);

           
            Transform[] selectedPoints = GetRandomSpawnPoints(wave.numberOfRandomSpawnPoint);

         
            for (int j = 0; j < wave.totalSpawnEnemies; j++)
            {
                Transform spawnPoint = selectedPoints[Random.Range(0, selectedPoints.Length)];

                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

                yield return new WaitForSeconds(wave.spawnInterval);
            }

          
            for (int k = 0; k < wave.numberOfPowerUp; k++)
            {
                Transform spawnPoint = selectedPoints[Random.Range(0, selectedPoints.Length)];

                Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
            }

           
            yield return new WaitUntil(() =>
                GameObject.FindGameObjectsWithTag("Enemy").Length == 0
            );

            Debug.Log("End Wave " + (i + 1));
        }

        Debug.Log("All Waves Finished");
    }

    
    Transform[] GetRandomSpawnPoints(int count)
    {
        Transform[] result = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = spawnPoints[Random.Range(0, spawnPoints.Length)];
        }

        return result;
    }
}