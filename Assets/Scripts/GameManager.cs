using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {


    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private int maxEnemiesOnScreen;
    [SerializeField] private int totalEnemies;
    [SerializeField] private int enemiesPerSpawn;
    [SerializeField] public Transform [] wayPoints;
    const float spawnDelay=0.5f;

    public List<Enemy> EnemyList=new List<Enemy>();

	void Start () {
        StartCoroutine(spawnEnemy());
	}

    IEnumerator spawnEnemy()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(Enemies[1]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }
            yield return new WaitForSeconds(spawnDelay); 
            StartCoroutine(spawnEnemy());
        }
    }

    public void registerEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void unregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void destroyAllEnemies()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
    }

}
