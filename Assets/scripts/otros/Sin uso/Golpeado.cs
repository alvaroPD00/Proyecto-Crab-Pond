using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpeado : MonoBehaviour
{
    [Header("Configuraci�n de Golpe")]
    public string[] tagsValidas; // Lista de tags que activan la detecci�n de golpes
    public ContadorGolpes contador; // Referencia al script Contador

    private void OnCollisionEnter(Collision collision)
    {
        VerificarColision(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        VerificarColision(other.gameObject);
    }

    private void VerificarColision(GameObject objeto)
    {
        foreach (string tagValida in tagsValidas)
        {
            if (objeto.CompareTag(tagValida))
            {
                if (contador != null)
                {
                    contador.Contar();
                }
                else
                {
                    Debug.LogWarning("Golpeado: No se ha asignado un Contador en el Inspector.");
                }
                return;
            }
        }
    }
}
