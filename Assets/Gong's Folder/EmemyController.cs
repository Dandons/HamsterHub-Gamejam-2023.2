using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyController : MonoBehaviour
{

    public float spawnInterval;
    public Transform spawnArea;
    public int numberOfEnemiesToSpawn;
    public GameObject meleeEnemyPrefab;
    public GameObject rangeEnemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemys());
    }

    // Update is called once per frame
    private void Update()
    {
        numberOfEnemiesToSpawn = DayCount.days;

    }

    IEnumerator SpawnEnemys()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Vector2 randomSpawnPoint = new Vector2(
                Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
                Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2, spawnArea.position.y + spawnArea.localScale.y / 2));

            int enemynum = Random.Range(0, 100);
            if (enemynum > 50)
            {
                Instantiate(meleeEnemyPrefab, randomSpawnPoint, Quaternion.identity) ;
            }
            if (enemynum < 49)
            {
                Instantiate(rangeEnemyPrefab, randomSpawnPoint, Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(10f);
    }

    
}
