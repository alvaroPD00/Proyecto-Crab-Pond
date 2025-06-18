using UnityEngine;
using UnityEngine.SceneManagement;

public class Iniciar : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "NombreDeLaEscena"; // Nombre de la escena a cargar

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 es el bot�n izquierdo del mouse
        {
            Debug.Log($"Cambiando a la escena: {sceneToLoad}");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
