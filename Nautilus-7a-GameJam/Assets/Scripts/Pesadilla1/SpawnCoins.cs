using System.Collections;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{
    public GameObject monedaPrefab;
    public Collider2D zonaDeSpawnCollider;
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
            // Crear una nueva moneda en una posición aleatoria dentro del Collider
            GameObject nuevaMoneda = Instantiate(monedaPrefab, ObtenerPosicionAleatoriaEnZona(), Quaternion.identity);

            // Activar la moneda recién creada
            nuevaMoneda.SetActive(true);

            // Esperar antes de spawnear la siguiente moneda
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }

    Vector3 ObtenerPosicionAleatoriaEnZona()
    {
        if (zonaDeSpawnCollider != null)
        {
            // Obtener el tamaño del Collider para calcular la posición aleatoria dentro del mismo
            Vector3 zonaDeSpawnSize = zonaDeSpawnCollider.bounds.size;

            Vector3 randomPosition = zonaDeSpawnCollider.transform.position + new Vector3(
                Random.Range(-zonaDeSpawnSize.x / 2f, zonaDeSpawnSize.x / 2f),
                Random.Range(-zonaDeSpawnSize.y / 2f, zonaDeSpawnSize.y / 2f),
                Random.Range(-zonaDeSpawnSize.z / 2f, zonaDeSpawnSize.z / 2f)
            );

            // Verificar si la posición aleatoria está dentro del Collider
            if (zonaDeSpawnCollider.bounds.Contains(randomPosition))
            {
                return randomPosition;
            }
            else
            {
                Debug.LogWarning("La posición generada no está dentro del Collider. Generando otra posición.");
                return ObtenerPosicionAleatoriaEnZona(); // Llamada recursiva para generar otra posición
            }
        }
        else
        {
            Debug.LogError("No se proporcionó un Collider para la zona de spawn.");
            return Vector3.zero; // Otra posición predeterminada si no hay Collider
        }
    }
}
