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

            float x = Random.Range(-zonaDeSpawnSize.x / 2f, zonaDeSpawnSize.x / 2f);
            float y = Random.Range(-zonaDeSpawnSize.y / 2f, zonaDeSpawnSize.y / 2f);
            float z = Random.Range(-zonaDeSpawnSize.z / 2f, zonaDeSpawnSize.z / 2f);

            // Obtener la posici�n aleatoria dentro del Collider
            return zonaDeSpawnCollider.transform.position + new Vector3(x, y, z);
        }
        else
        {
            Debug.LogError("No se proporcion� un Collider para la zona de spawn.");
            return Vector3.zero; // Otra posici�n predeterminada si no hay Collider
        }
    }
}
