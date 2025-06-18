using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int golpes = 3; // Vida inicial modificable desde el inspector
    private int golpesIniciales; // Almacena el valor inicial de golpes
    public Transform contenedorDesactivados; // Objeto padre donde se guardar�n los objetos desactivados

    private void Awake()
    {
        golpesIniciales = golpes; // Guardamos el valor inicial de golpes
    }

    // M�todo para recibir da�o
    public void RecibirGolpe(int cantidad)
    {
        golpes -= cantidad;
        Debug.Log(gameObject.name + " recibi� " + cantidad + " golpe(s). Vida restante: " + golpes);

        if (golpes <= 0)
        {
            Desactivar();
        }
    }

    // M�todo para desactivar y almacenar el objeto
    void Desactivar()
    {
        Debug.Log(gameObject.name + " ha sido desactivado y almacenado.");

        // Reiniciar golpes al valor inicial
        golpes = golpesIniciales;

        // Mueve el objeto al contenedor de desactivados si est� asignado
        if (contenedorDesactivados != null)
        {
            transform.SetParent(contenedorDesactivados);
        }

        // Desactiva el objeto
        gameObject.SetActive(false);
    }
}
