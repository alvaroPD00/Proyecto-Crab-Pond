using UnityEngine;

public class ClickOrTouchDetector : MonoBehaviour
{
    private RaycastTagBlocker bloqueador;
    private ObjetoClickeable objetoPresionado;

    void Start()
    {
        bloqueador = FindObjectOfType<RaycastTagBlocker>();
    }

    void Update()
    {
        // Mouse
        if (Input.GetMouseButtonDown(0))
        {
            objetoPresionado = DetectarObjeto(Input.mousePosition);
            objetoPresionado?.AvisarPresionado();
        }

        if (Input.GetMouseButtonUp(0))
        {
            var objetoSoltado = DetectarObjeto(Input.mousePosition);
            if (objetoSoltado != null && objetoSoltado == objetoPresionado)
            {
                objetoSoltado.AvisarSoltado();
            }
            objetoPresionado = null;
        }

        // Touch
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            if (toque.phase == TouchPhase.Began)
            {
                objetoPresionado = DetectarObjeto(toque.position);
                objetoPresionado?.AvisarPresionado();
            }
            if (toque.phase == TouchPhase.Ended)
            {
                var objetoSoltado = DetectarObjeto(toque.position);
                if (objetoSoltado != null && objetoSoltado == objetoPresionado)
                {
                    objetoSoltado.AvisarSoltado();
                }
                objetoPresionado = null;
            }
            if (toque.phase == TouchPhase.Canceled || toque.phase == TouchPhase.Moved)
            {
                objetoPresionado = null;
            }
        }
    }

    ObjetoClickeable DetectarObjeto(Vector2 posicionPantalla)
    {
        Ray ray = Camera.main.ScreenPointToRay(posicionPantalla);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject objetoImpactado = hit.collider.gameObject;

            if (bloqueador != null && bloqueador.gameObject.activeInHierarchy)
            {
                if (bloqueador.DeberiaBloquear(objetoImpactado))
                    return null;
            }

            return objetoImpactado.GetComponent<ObjetoClickeable>();
        }

        return null;
    }
}
