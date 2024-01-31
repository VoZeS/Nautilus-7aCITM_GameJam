using System.Collections;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    public GameObject monedaPrefab;
    public Transform[] posicionesDeSpawn; // Arreglo de posiciones dadas desde el inspector
    public float tiempoEntreSpawns = 2f;

    void Start()
    {
        // Iniciar la rutina de spawn de monedas
        StartCoroutine(SpawnearMonedas());
    }

    IEnumerator SpawnearMonedas()
    {
        while (true)
        {
            // Seleccionar una posici�n aleatoria del arreglo de posiciones
            Vector3 posicionAleatoria = posicionesDeSpawn[Random.Range(0, posicionesDeSpawn.Length)].position;

            // Crear una nueva moneda en la posici�n aleatoria
            GameObject nuevaMoneda = Instantiate(monedaPrefab, posicionAleatoria, Quaternion.identity);

            // Activar la moneda reci�n creada
            nuevaMoneda.SetActive(true);

            // Esperar antes de spawnear la siguiente moneda
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }
}
