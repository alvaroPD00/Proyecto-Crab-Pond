using UnityEngine;
using UnityEngine.AI;

public class ClickNavegar : MonoBehaviour
{
    private NavMeshAgent agente;
    private Camera camaraPrincipal;

    [Header("Distancia mínima para considerar que llegó al destino")]
    public float umbralLlegada = 0.1f;

    private bool tieneDestino = false;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        camaraPrincipal = Camera.main;
    }

    void Update()
    {
        // MOUSE
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camaraPrincipal.ScreenPointToRay(Input.mousePosition);
            ProcesarClick(ray);
        }

        // TOUCH
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = camaraPrincipal.ScreenPointToRay(Input.GetTouch(0).position);
            ProcesarClick(ray);
        }

        // Verificar si llegó al destino
        if (tieneDestino && !agente.pathPending && agente.remainingDistance <= umbralLlegada)
        {
            agente.ResetPath(); // Detener completamente
            tieneDestino = false;
        }
    }

    void ProcesarClick(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            agente.SetDestination(hit.point);
            tieneDestino = true;
        }
    }
}
