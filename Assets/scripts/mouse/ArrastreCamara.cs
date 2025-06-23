using UnityEngine;

public class ArrastreCamara : MonoBehaviour
{
    [Header("Cámara a mover")]
    public Transform camara;

    [Header("Sensibilidad del arrastre")]
    public float sensibilidadMouse = 0.1f;
    public float sensibilidadTouch = 0.05f;

    [Header("Limites opcionales")]
    public bool limitarMovimiento = false;
    public Vector2 limiteX = new Vector2(-10f, 10f);
    public Vector2 limiteZ = new Vector2(-10f, 10f);

    private Vector2 ultimaPosicionInput;
    private bool arrastrando = false;

    void Update()
    {
        if (camara == null)
        {
            Debug.LogWarning("No se asignó la cámara al script CamaraDragIsometrica.");
            return;
        }

        // --- MOUSE ---
        if (Input.GetMouseButtonDown(0))
        {
            ultimaPosicionInput = Input.mousePosition;
            arrastrando = true;
        }
        else if (Input.GetMouseButton(0) && arrastrando)
        {
            Vector2 delta = (Vector2)Input.mousePosition - ultimaPosicionInput;
            Arrastrar(delta, sensibilidadMouse);
            ultimaPosicionInput = (Vector2)Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            arrastrando = false;
        }

        // --- TOUCH ---
        if (Input.touchCount == 1)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.phase == TouchPhase.Began)
            {
                ultimaPosicionInput = toque.position;
                arrastrando = true;
            }
            else if (toque.phase == TouchPhase.Moved && arrastrando)
            {
                Vector2 delta = toque.position - ultimaPosicionInput;
                Arrastrar(delta, sensibilidadTouch);
                ultimaPosicionInput = toque.position;
            }
            else if (toque.phase == TouchPhase.Ended || toque.phase == TouchPhase.Canceled)
            {
                arrastrando = false;
            }
        }
    }

    void Arrastrar(Vector2 deltaPantalla, float sensibilidad)
    {
        Vector3 direccionDerecha = new Vector3(1f, 0f, -1f).normalized;
        Vector3 direccionArriba = new Vector3(1f, 0f, 1f).normalized;

        Vector3 movimiento = (direccionDerecha * deltaPantalla.x + direccionArriba * deltaPantalla.y) * sensibilidad;

        camara.position -= movimiento; // Inversión del movimiento para efecto de arrastre

        if (limitarMovimiento)
        {
            float clampedX = Mathf.Clamp(camara.position.x, limiteX.x, limiteX.y);
            float clampedZ = Mathf.Clamp(camara.position.z, limiteZ.x, limiteZ.y);
            camara.position = new Vector3(clampedX, camara.position.y, clampedZ);
        }
    }

}
