using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarColisiones : MonoBehaviour
{
    [Header("Tiempo en segundos para realizar el movimiento")]
    public float tiempoMovimiento = 1.0f;

    [System.Serializable]
    public class ConfiguracionColision
    {
        public string tag1;
        public string tag2;
        public float desplazamiento1;
        public float desplazamiento2;
        public int golpesA;
        public int golpesB;
    }

    [Header("Configuraciones de colisiones")]
    public List<ConfiguracionColision> colisionesDetectables;

    private void OnTriggerEnter(Collider other)
    {
        string tagA = gameObject.tag;
        string tagB = other.gameObject.tag;

        foreach (var config in colisionesDetectables)
        {
            if ((config.tag1 == tagA && config.tag2 == tagB) || (config.tag1 == tagB && config.tag2 == tagA))
            {
                GameObject objetoA = gameObject;
                GameObject objetoB = other.gameObject;

                float desplazamientoA = (config.tag1 == tagA) ? config.desplazamiento1 : config.desplazamiento2;
                float desplazamientoB = (config.tag1 == tagB) ? config.desplazamiento1 : config.desplazamiento2;

                int golpesA = (config.tag1 == tagA) ? config.golpesA : config.golpesB;
                int golpesB = (config.tag1 == tagB) ? config.golpesA : config.golpesB;

                // Direcciones basadas en las posiciones relativas
                Vector3 direccionAB = (objetoA.transform.position - objetoB.transform.position).normalized;
                Vector3 direccionBA = -direccionAB;

                // Iniciar movimientos
                StartCoroutine(MoverObjeto(objetoA, direccionAB * desplazamientoA));
                StartCoroutine(MoverObjeto(objetoB, direccionBA * desplazamientoB));

                // Aplicar daños
                AplicarGolpe(objetoA, golpesA);
                AplicarGolpe(objetoB, golpesB);

                break;
            }
        }
    }

    IEnumerator MoverObjeto(GameObject objeto, Vector3 desplazamiento)
    {
        if (objeto == null || desplazamiento == Vector3.zero) yield break;

        Vector3 posicionInicial = objeto.transform.position;
        Vector3 posicionFinal = posicionInicial + desplazamiento;

        float tiempo = 0f;

        while (tiempo < tiempoMovimiento)
        {
            objeto.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, tiempo / tiempoMovimiento);
            tiempo += Time.deltaTime;
            yield return null;
        }

        objeto.transform.position = posicionFinal;
    }

    void AplicarGolpe(GameObject objeto, int cantidad)
    {
        if (cantidad > 0)
        {
            Vida vida = objeto.GetComponent<Vida>();
            if (vida != null)
            {
                vida.RecibirGolpe(cantidad);
            }
        }
    }
}
