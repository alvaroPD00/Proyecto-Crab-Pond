using UnityEngine;
using System.Runtime.InteropServices;

public class ObjetoClickeable : MonoBehaviour
{
    [Header("URL que se abrir� al hacer clic")]
    public string url;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void AbrirEnNuevaPestana(string url);
#endif

    private void OnMouseDown()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        AbrirEnNuevaPestana(url);
#else
        Application.OpenURL(url); // En editor o build no WebGL
#endif
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Ac� podr�as usar una textura de manito si quer�s
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Restaurar
    }
}
