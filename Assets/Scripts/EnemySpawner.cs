using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public int enemyCount = 1;
    public GameObject enemy;
    public float minDistance = 0f;
    public float maxDistance = 20f;
    public int currentEnemies = 0;
    public Image Marker;
    public GameObject player;
    public GameObject waypoint;

    void Start()
    {
        SpawnEnemies();
        Marker = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
        Marker.enabled = false;
        player = GameObject.Find("Player");
        waypoint = GameObject.Find("Hud/WayPoint");
    }

    void Update()
    {
        if (Camera.main.gameObject.GetComponent<WayPoint>().QuestTaken)
        {
           if (Vector3.Distance(transform.position, player.transform.position)< 20)
            {
                Marker.enabled = true;
                waypoint.SetActive(false);
            }
            else
            {
                Marker.enabled = false;
                waypoint.SetActive(true);
            } 
         }
        
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject currentEnemy = Instantiate(enemy);
            currentEnemies++;
            currentEnemy.transform.position = Vector3.zero;
            while (currentEnemy.transform.position == Vector3.zero)
            {
                currentEnemy.transform.position = GenerateSpawnPosition();
            }
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(minDistance, maxDistance);
        randomDirection += transform.position;
        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(randomDirection, out navMeshHit, maxDistance, NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }
        return Vector3.zero;
    }
}
