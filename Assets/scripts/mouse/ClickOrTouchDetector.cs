using UnityEngine;

public class ClickOrTouchDetector : MonoBehaviour
{
    private RaycastTagBlocker bloqueador;
    private CrabData objetoPresionadoData;

    void Start()
    {
        bloqueador = FindObjectOfType<RaycastTagBlocker>();
    }

    void Update()
    {
        // --- MOUSE ---
        if (Input.GetMouseButtonDown(0))
        {
            DetectarPresionado(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            DetectarSoltado(Input.mousePosition);
            objetoPresionadoData = null;
        }

        // --- TOUCH ---
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.phase == TouchPhase.Began)
            {
                DetectarPresionado(toque.position);
            }

            if (toque.phase == TouchPhase.Ended)
            {
                DetectarSoltado(toque.position);
                objetoPresionadoData = null;
            }

            if (toque.phase == TouchPhase.Canceled || toque.phase == TouchPhase.Moved)
            {
                objetoPresionadoData = null;
            }
        }
    }

    void DetectarPresionado(Vector2 posicionPantalla)
    {
        GameObject obj = DetectarObjeto(posicionPantalla);
        if (obj == null) return;

        objetoPresionadoData = obj.GetComponent<CrabData>();
    }

    void DetectarSoltado(Vector2 posicionPantalla)
    {
        GameObject obj = DetectarObjeto(posicionPantalla);
        if (obj == null) return;

        if (obj.GetComponent<CrabData>() == objetoPresionadoData)
        {
            objetoPresionadoData?.MostrarInfo();
        }
    }

    GameObject DetectarObjeto(Vector2 posicionPantalla)
    {
        Ray ray = Camera.main.ScreenPointToRay(posicionPantalla);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject objeto = hit.collider.gameObject;

            if (bloqueador != null && bloqueador.gameObject.activeInHierarchy)
            {
                if (bloqueador.DeberiaBloquear(objeto))
                    return null;
            }

            return objeto;
        }

        return null;
    }
}
