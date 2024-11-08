using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject prefab;

    public float spawnRadius = 5f;

    public float spawnHeight = 2f;

    public int maxInstances = 5;

    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;

    private int currentInstances = 0;

    void Start()
    {
        // Inicia la corrutina de aparici�n autom�tica
        StartCoroutine(SpawnPrefabsWithInterval());
    }

    IEnumerator SpawnPrefabsWithInterval()
    {
        // Mientras el n�mero de instancias actuales sea menor que el l�mite m�ximo
        while (currentInstances < maxInstances)
        {
            // Generar una posici�n aleatoria dentro del radio
            Vector2 randomPos = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(transform.position.x + randomPos.x, spawnHeight, transform.position.z + randomPos.y);

            // Instanciar el prefab en la posici�n calculada
            Instantiate(prefab, spawnPosition, Quaternion.identity);

            // Aumentar el contador de instancias
            currentInstances++;

            // Esperar un intervalo aleatorio o determinado antes de la siguiente aparici�n
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);
        }
    }
}
