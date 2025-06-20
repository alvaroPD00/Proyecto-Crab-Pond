using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Objeto a spawnear")]
    public GameObject objetoPrefab; // Prefab del objeto a crear

    [Header("Configuraci�n del Spawner")]
    public Transform areaSpawn; // �rea en la que se generar� el objeto
    public GameObject objetosDesactivados; // Contenedor de objetos desactivados
    public Transform parentSpawn; // Objeto padre para los objetos instanciados

    public float intervaloSpawn = 2f; // Tiempo entre cada spawn (si se usa auto-spawn)
    public bool autoSpawn = false; // Si el spawneo ser� autom�tico o manual

    private float tiempoTranscurrido = 0f; // Tiempo transcurrido desde el inicio
    private Quaternion rotacionOriginal; // Para almacenar la rotaci�n del prefab

    private void Start()
    {
        if (objetoPrefab != null)
        {
            rotacionOriginal = objetoPrefab.transform.rotation; // Guardar la rotaci�n original del prefab
        }

        if (autoSpawn)
        {
            tiempoTranscurrido = 0f;
        }
    }

    public void SpawnObjeto()
    {
        if (objetoPrefab == null || areaSpawn == null || objetosDesactivados == null || parentSpawn == null)
        {
            Debug.LogWarning("Spawner: Aseg�rate de asignar un objetoPrefab, areaSpawn, objetosDesactivados y parentSpawn en el Inspector.");
            return;
        }

        string tagBuscada = objetoPrefab.tag; // Obtener la tag del prefab

        // Buscar entre los hijos de ObjetosDesactivados un objeto con la misma tag
        foreach (Transform obj in objetosDesactivados.transform)
        {
            if (!obj.gameObject.activeSelf && obj.CompareTag(tagBuscada))
            {
                // Reactivar el objeto, moverlo a la posici�n de spawn y asignarle el nuevo padre
                obj.gameObject.SetActive(true);
                obj.position = areaSpawn.position;
                obj.rotation = rotacionOriginal; // Aplicar la rotaci�n original
                obj.SetParent(parentSpawn); // Asignar el objeto al nuevo padre

                Debug.Log($"Reactivado objeto {obj.name} con tag {tagBuscada} como hijo de {parentSpawn.name}.");
                return;
            }
        }

        // Si no hay un objeto desactivado con la tag, instanciar uno nuevo con la rotaci�n original
        GameObject nuevoObjeto = Instantiate(objetoPrefab, areaSpawn.position, rotacionOriginal, parentSpawn);
        Debug.Log($"Instanciado nuevo objeto {nuevoObjeto.name} con tag {tagBuscada} como hijo de {parentSpawn.name}.");
    }

    private IEnumerator SpawnCadaTiempo()
    {
        while (true)
        {
            yield return null; // Espera el siguiente frame

            tiempoTranscurrido += Time.deltaTime;

            if (tiempoTranscurrido >= intervaloSpawn)
            {
                SpawnObjeto();
                tiempoTranscurrido -= intervaloSpawn; // En vez de resetear a 0, restamos el intervalo exacto
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObjeto();
        }

        if (autoSpawn && tiempoTranscurrido == 0f)
        {
            StartCoroutine(SpawnCadaTiempo());
        }
    }
}
