using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PantallasInicio : MonoBehaviour
{
    public Image[] slides; // Arreglo de im�genes a mostrar
    public float slideDuration = 3f; // Duraci�n en segundos por slide
    public string nextSceneName; // Nombre de la escena a cargar

    private int currentSlideIndex = 0;
    private Coroutine autoAdvanceCoroutine;

    void Start()
    {
        // Desactiva todas las im�genes
        foreach (Image slide in slides)
        {
            slide.gameObject.SetActive(false);
        }

        // Activa la primera imagen si hay al menos una
        if (slides.Length > 0)
        {
            slides[0].gameObject.SetActive(true);
            autoAdvanceCoroutine = StartCoroutine(AutoAdvanceSlide());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (autoAdvanceCoroutine != null)
            {
                StopCoroutine(autoAdvanceCoroutine);
            }
            NextSlide();
        }
    }

    IEnumerator AutoAdvanceSlide()
    {
        while (currentSlideIndex < slides.Length - 1)
        {
            yield return new WaitForSeconds(slideDuration);
            NextSlide();
        }

        // Espera antes de cambiar de escena en la �ltima imagen
        yield return new WaitForSeconds(slideDuration);
        SceneManager.LoadScene(nextSceneName);
    }

    void NextSlide()
    {
        // Desactiva la imagen actual
        slides[currentSlideIndex].gameObject.SetActive(false);

        // Avanza al siguiente slide si hay m�s
        if (currentSlideIndex < slides.Length - 1)
        {
            currentSlideIndex++;
            slides[currentSlideIndex].gameObject.SetActive(true);
            autoAdvanceCoroutine = StartCoroutine(AutoAdvanceSlide());
        }
        else
        {
            // Si es la �ltima imagen, iniciar la transici�n final
            StartCoroutine(WaitAndLoadScene());
        }
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(slideDuration);
        SceneManager.LoadScene(nextSceneName);
    }
}
