using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class CrabURL : MonoBehaviour
{
    [Header("URL actual asignada")]
    [SerializeField] private string url;

    private Button boton;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void AbrirEnNuevaPestana(string url);
#endif

    void Awake()
    {
        boton = GetComponent<Button>();
        if (boton != null)
        {
            boton.onClick.AddListener(AbrirURL);
        }
        else
        {
            Debug.LogWarning("CrabURL requiere que este GameObject tenga un componente Button.");
        }
    }

    /// <summary>
    /// Método que CrabData usará para asignar la URL a este objeto
    /// </summary>
    public void AsignarURL(string nuevaURL)
    {
        url = nuevaURL;
    }

    public void AbrirURL()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        AbrirEnNuevaPestana(url);
#else
        Application.OpenURL(url);
#endif
    }
}
