using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComidita : MonoBehaviour
{
    [Header("Objeto a spawnear")]
    public GameObject objetoPrefab;

    [Header("Configuración del Spawner")]
    public GameObject objetosDesactivados; // Contenedor con objetos inactivos
    public Transform parentSpawn;
    public float particlesPerSecond = 10f;
    public float alturaSpawn = 5f; // <-- Altura fija desde donde se instanciarán los objetos

    private float holdTime = 0f;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            holdTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                int spawnCount = Mathf.RoundToInt(holdTime * particlesPerSecond);

                // Usamos las coordenadas X y Z del hit, pero con altura fija Y
                Vector3 posicionFinal = new Vector3(hit.point.x, alturaSpawn, hit.point.z);
                SpawnParticles(posicionFinal, spawnCount);
            }

            holdTime = 0f;
        }
    }

    void SpawnParticles(Vector3 position, int count)
    {
        if (objetoPrefab == null || objetosDesactivados == null || parentSpawn == null)
        {
            Debug.LogWarning("Spawner: Faltan referencias en el Inspector.");
            return;
        }

        string tagBuscada = objetoPrefab.tag;
        int activados = 0;

        // 1. Reactivar objetos inactivos
        foreach (Transform obj in objetosDesactivados.transform)
        {
            if (!obj.gameObject.activeSelf && obj.CompareTag(tagBuscada))
            {
                Vector3 offset = Random.insideUnitSphere * 0.2f;
                obj.position = position + offset;
                obj.rotation = Quaternion.identity;
                obj.SetParent(parentSpawn);
                obj.gameObject.SetActive(true);

                activados++;
                if (activados >= count) return;
            }
        }

        // 2. Si no alcanzó con los reciclados, instanciar los faltantes
        for (int i = activados; i < count; i++)
        {
            Vector3 offset = Random.insideUnitSphere * 0.2f;
            GameObject nuevo = Instantiate(objetoPrefab, position + offset, Quaternion.identity, parentSpawn);
            Debug.Log($"Instanciado nuevo objeto {nuevo.name} con tag {tagBuscada}.");
        }
    }
}
