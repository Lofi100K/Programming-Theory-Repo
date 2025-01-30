using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform player; // Riferimento al giocatore
    public GameObject[] enemyPrefabs; // Array di prefab nemici
    public float initialSpawnInterval = 5f; // Tempo iniziale tra gli spawn
    public float spawnAcceleration = 0.95f; // Riduzione percentuale dello spawn interval (es. 0.95 = 5% più veloce ogni ciclo)
    public float minimumSpawnInterval = 1f; // Intervallo minimo di spawn
    public float spawnRange = 25f; // Confine massimo per spawnare i nemici
    public float spawnInnerRadius = 7f; // Raggio interno (dal giocatore) in cui non possono spawnare
    public float spawnOuterRadius = 14f; // Raggio esterno (dai confini del campo di gioco)

    private float currentSpawnInterval; // Intervallo attuale di spawn

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentSpawnInterval = initialSpawnInterval;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();

            // Aspetta il tempo attuale di spawn prima del prossimo spawn
            yield return new WaitForSeconds(currentSpawnInterval);

            // Riduci gradualmente l'intervallo di spawn fino al limite minimo
            currentSpawnInterval = Mathf.Max(currentSpawnInterval * spawnAcceleration, minimumSpawnInterval);
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Se la posizione non è valida, non spawnare
        if (spawnPosition == Vector3.zero)
        {
            Debug.LogWarning("Non è stato possibile trovare una posizione valida per lo spawn del nemico.");
            return;
        }

        // Scegli un nemico casuale dall'array
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        // Instanzia il nemico nella posizione calcolata
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomSpawnPosition()
    {
        for (int attempts = 0; attempts < 10; attempts++) // Prova fino a 10 volte a trovare una posizione valida
        {
            // Genera una posizione casuale dentro il range massimo
            float x = Random.Range(-spawnRange, spawnRange);
            float z = Random.Range(-spawnRange, spawnRange);

            // Controlla che sia fuori dai confini + e - 14
            if (Mathf.Abs(x) < spawnOuterRadius && Mathf.Abs(z) < spawnOuterRadius)
                continue;

            // Controlla che sia al di fuori del raggio interno dal giocatore
            Vector3 playerPosition = player.position;
            Vector3 potentialPosition = new Vector3(x, 0, z); // Imposta y a 0 per il piano di gioco
            float distanceFromPlayer = Vector3.Distance(playerPosition, potentialPosition);

            if (distanceFromPlayer >= spawnInnerRadius && distanceFromPlayer <= spawnRange)
                return potentialPosition; // Posizione valida trovata
        }

        return Vector3.zero; // Nessuna posizione valida trovata
    }
}
