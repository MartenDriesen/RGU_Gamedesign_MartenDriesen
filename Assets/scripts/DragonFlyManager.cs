using System.Collections;
using UnityEngine;

//couldn't get this right so chatGpt helped
//https://chatgpt.com/share/67333bce-fa48-8001-ad18-344d7e7bdbba
public class DragonFlyManager : MonoBehaviour
{
    public GameObject dragonflyPrefab; // Prefab for the dragonfly
    public Transform spawnPoint1;      // First spawn point
    public Transform spawnPoint2;      // Second spawn point
    public float minSpawnInterval = 15f; // Minimum spawn interval (in seconds)
    public float maxSpawnInterval = 25f; // Maximum spawn interval (in seconds)

    void Start()
    {
        // Start the periodic check for spawning dragonflies
        StartCoroutine(SpawnDragonflyRoutine());
    }

    // Coroutine to spawn dragonflies if the total count is less than 4
    private IEnumerator SpawnDragonflyRoutine()
    {
        while (true)
        {
            // Get a random spawn interval between min and max
            float randomInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            // Wait for the random interval before checking again
            yield return new WaitForSeconds(randomInterval);

            // Check how many dragonflies are currently in the scene
            int dragonflyCount = GameObject.FindGameObjectsWithTag("Dragonfly").Length;

            // If there are less than 4 dragonflies, spawn a new one
            if (dragonflyCount < 4)
            {
                SpawnDragonfly();
            }
        }
    }

    // Method to spawn a dragonfly
    private void SpawnDragonfly()
    {
        // Make sure we're only spawning if there are fewer than 4 dragonflies
        int dragonflyCount = GameObject.FindGameObjectsWithTag("Dragonfly").Length;
        if (dragonflyCount >= 4)
        {
            return; // Skip spawning if there are 4 or more dragonflies
        }

        // Randomly choose between spawnPoint1 or spawnPoint2
        Transform spawnPoint = Random.Range(0f, 1f) < 0.5f ? spawnPoint1 : spawnPoint2;

        // Instantiate the new dragonfly at the chosen spawn point
        GameObject newDragonfly = Instantiate(dragonflyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Log the current number of dragonflies in the game
        dragonflyCount = GameObject.FindGameObjectsWithTag("Dragonfly").Length;
        Debug.Log($"Dragonfly spawned. Total dragonflies: {dragonflyCount}");
    }
}
