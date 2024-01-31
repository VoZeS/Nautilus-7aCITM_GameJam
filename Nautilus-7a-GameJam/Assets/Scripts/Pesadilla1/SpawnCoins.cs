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
            // Crear una nueva moneda en una posici�n aleatoria dentro del Collider
            GameObject nuevaMoneda = Instantiate(monedaPrefab, ObtenerPosicionAleatoriaEnZona(), Quaternion.identity);

            // Activar la moneda reci�n creada
            nuevaMoneda.SetActive(true);

            // Esperar antes de spawnear la siguiente moneda
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }

    Vector3 ObtenerPosicionAleatoriaEnZona()
    {
        if (zonaDeSpawnCollider != null)
        {
            // Obtener el tama�o del Collider para calcular la posici�n aleatoria dentro del mismo
            Vector3 zonaDeSpawnSize = zonaDeSpawnCollider.bounds.size;

            Vector3 randomPosition = zonaDeSpawnCollider.transform.position + new Vector3(
                Random.Range(-zonaDeSpawnSize.x / 2f, zonaDeSpawnSize.x / 2f),
                Random.Range(-zonaDeSpawnSize.y / 2f, zonaDeSpawnSize.y / 2f),
                Random.Range(-zonaDeSpawnSize.z / 2f, zonaDeSpawnSize.z / 2f)
            );

            // Verificar si la posici�n aleatoria est� dentro del Collider
            if (zonaDeSpawnCollider.bounds.Contains(randomPosition))
            {
                return randomPosition;
            }
            else
            {
                Debug.LogWarning("La posici�n generada no est� dentro del Collider. Generando otra posici�n.");
                return ObtenerPosicionAleatoriaEnZona(); // Llamada recursiva para generar otra posici�n
            }
        }
        else
        {
            Debug.LogError("No se proporcion� un Collider para la zona de spawn.");
            return Vector3.zero; // Otra posici�n predeterminada si no hay Collider
        }
    }
}
