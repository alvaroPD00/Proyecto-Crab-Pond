using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CrabData : MonoBehaviour
{
    [Header("Datos asignados a este objeto")]
    public string nombre;
    [TextArea] public string descripcion;
    public string callToAction;
    public string url;

    [Header("Referencias a UI")]
    public TextMeshProUGUI textoNombre;
    public TextMeshProUGUI textoDescripcion;
    public TextMeshProUGUI textoCallToAction;

    [Header("Objeto receptor de URL")]
    public CrabURL receptorDeURL; // Tu script de apertura de URL

    private void OnMouseDown()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return; // Evitar clics sobre UI

        MostrarInfo();
    }

    public void MostrarInfo()
    {
        if (textoNombre != null) textoNombre.text = nombre;
        if (textoDescripcion != null) textoDescripcion.text = descripcion;
        if (textoCallToAction != null) textoCallToAction.text = callToAction;
        if (receptorDeURL != null) receptorDeURL.AsignarURL(url);
    }

}
