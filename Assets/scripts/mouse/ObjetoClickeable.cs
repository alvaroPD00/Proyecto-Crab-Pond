using UnityEngine;
using System.Runtime.InteropServices;

public class ObjetoClickeable : MonoBehaviour
{
    [Header("URL que se abrirá al hacer clic o tap")]
    public string url;

    [Header("Opcional: cursor de manito al pasar")]
    public Texture2D cursorManito;
    public Vector2 hotspot = Vector2.zero;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void AbrirEnNuevaPestana(string url);
#endif

    private bool presionadoDesdeDetector = false;

    public void AbrirURL()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        AbrirEnNuevaPestana(url);
#else
        Application.OpenURL(url);
#endif
    }

    public void AvisarPresionado()
    {
        presionadoDesdeDetector = true;
    }

    public void AvisarSoltado()
    {
        if (presionadoDesdeDetector)
        {
            AbrirURL();
            presionadoDesdeDetector = false;
        }
    }

    private void OnMouseEnter()
    {
        if (cursorManito != null)
            Cursor.SetCursor(cursorManito, hotspot, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
