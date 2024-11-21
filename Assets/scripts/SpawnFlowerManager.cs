using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlowerManager : MonoBehaviour
{
    public GameObject flowerPrefab;
    public float spawnInterval = 4f;
    public int maxFlowers = 5;

    private float minX = 10f;
    private float maxX = 14f;
    private float minY = 13f;
    private float maxY = 13f;

    void Start()
    {
        if (flowerPrefab == null)
        {
            Debug.LogError("Flower prefab not assigned ");
            return;
        }

        StartCoroutine(SpawnFlowers());
    }

    IEnumerator SpawnFlowers()
    {
        while (true)
        { int currentFlowerCount = FindObjectsOfType<Flower>().Length;

            if (currentFlowerCount < maxFlowers)
            {
                SpawnFlower();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnFlower()
    {   float randomX = Random.Range(minX, maxX);
     float randomY = Random.Range(minY, maxY);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        Instantiate(flowerPrefab, spawnPosition, Quaternion.identity);
    }
}
