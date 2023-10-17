using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyController : MonoBehaviour
{
    public Enemy myenemy;
    public float spawnInterval;
    public Transform spawnArea;
    public int numberOfEnemiesToSpawn;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemys());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    IEnumerator SpawnEnemys()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Vector3 randomSpawnPoint = new Vector3(
                Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
                spawnArea.position.y,
                Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2, spawnArea.position.z + spawnArea.localScale.z / 2));


            Instantiate(enemyPrefab, randomSpawnPoint, Quaternion.identity);
            myenemy = new Enemy(transform);
            myenemy.rb = this.GetComponent<Rigidbody2D>();
            myenemy.FixedUpdate();
            yield return new WaitForSeconds(spawnInterval);
        }
       
    }


}
