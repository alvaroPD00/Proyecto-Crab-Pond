using UnityEngine;
using UnityEngine.AI;

public class CrabIA : MonoBehaviour
{
    [Header("Objetivos")]
    public string tagObjetivo = "Player";

    [Header("Referencias de movimiento idle")]
    public Transform circuloA;
    public Transform circuloB;

    [Header("Tiempos entre movimientos (segundos)")]
    public float tiempoInicialA = 0.5f; // NUEVO: tiempo para la primera caminata al círculo A
    public float tiempoEntreMovimientosA = 3f;
    public float tiempoEntreMovimientosB = 10f;

    private NavMeshAgent agente;
    private float contadorA;
    private float contadorB;
    private Transform objetivoActual;
    private bool enModoIdle = false;
    private bool primerMovimientoARealizado = false;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        contadorA = tiempoEntreMovimientosA;
        contadorB = tiempoEntreMovimientosB;
    }

    void Update()
    {
        objetivoActual = ObtenerObjetivoMasCercano();

        if (objetivoActual != null)
        {
            agente.SetDestination(objetivoActual.position);
            enModoIdle = false;
            primerMovimientoARealizado = false; // Reiniciamos cuando vuelve a detectar
        }
        else
        {
            if (!enModoIdle)
            {
                if (!agente.pathPending && agente.hasPath)
                    agente.ResetPath();

                enModoIdle = true;
                contadorA = tiempoInicialA; //  Se usa el tiempo especial solo una vez
                contadorB = tiempoEntreMovimientosB;
            }

            contadorA -= Time.deltaTime;
            contadorB -= Time.deltaTime;

            if (contadorA <= 0f && circuloA != null)
            {
                float radioA = ObtenerRadioDesdeTransform(circuloA);
                Vector3 destino = PuntoEnCircunferencia(circuloA.position, radioA);
                agente.SetDestination(destino);

                //  Para siguientes movimientos, usamos el tiempo habitual
                contadorA = tiempoEntreMovimientosA;
                primerMovimientoARealizado = true;
            }

            if (contadorB <= 0f && circuloB != null)
            {
                float radioB = ObtenerRadioDesdeTransform(circuloB);
                Vector3 destino = PuntoEnCircunferencia(circuloB.position, radioB);
                agente.SetDestination(destino);
                contadorB = tiempoEntreMovimientosB;
            }
        }
    }

    Transform ObtenerObjetivoMasCercano()
    {
        GameObject[] objetivos = GameObject.FindGameObjectsWithTag(tagObjetivo);
        if (objetivos.Length == 0) return null;

        Transform masCercano = null;
        float distanciaMin = Mathf.Infinity;
        Vector3 origen = transform.position;

        foreach (GameObject obj in objetivos)
        {
            float dist = Vector3.Distance(origen, obj.transform.position);
            if (dist < distanciaMin)
            {
                distanciaMin = dist;
                masCercano = obj.transform;
            }
        }

        return masCercano;
    }

    float ObtenerRadioDesdeTransform(Transform t)
    {
        return t.localScale.x * 0.5f;
    }

    Vector3 PuntoEnCircunferencia(Vector3 centro, float radio)
    {
        float angulo = Random.Range(0f, Mathf.PI * 2f);
        float x = Mathf.Cos(angulo) * radio;
        float z = Mathf.Sin(angulo) * radio;
        return new Vector3(centro.x + x, transform.position.y, centro.z + z);
    }
}
