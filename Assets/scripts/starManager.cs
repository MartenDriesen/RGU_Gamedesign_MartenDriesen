using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starManager : MonoBehaviour
{
    public GameObject starPrefab; // Prefab for the flower
    public float spawnInterval = 4f; // Time between spawns in seconds
    public int maxFlowers = 5; // Maximum number of flowers allowed in the scene

    private float minX = 10f;
    private float maxX = 14f;
    private float minY = 13f;
    private float maxY = 13f;


    void Start()
    {
        if (starPrefab == null)
        {
            Debug.LogError("Flower prefab not assigned in the Inspector!");
            return;
        }

        // Start the spawn coroutine
        StartCoroutine(SpawnStars());
    }

    IEnumerator SpawnStars()
    {
        while (true)
        {
            // Count the number of flowers currently in the scene
            int currentFlowerCount = FindObjectsOfType<Flower>().Length;

            // Spawn a new flower if there are fewer than the maximum allowed
            if (currentFlowerCount < maxFlowers)
            {
                SpawnStar();
            }

            // Wait for the specified interval before checking again
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnStar()
    {
        // Generate a random position within the specified bounds
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // Instantiate the flower prefab at the random position
        Instantiate(starPrefab, spawnPosition, Quaternion.identity);
    }
}
