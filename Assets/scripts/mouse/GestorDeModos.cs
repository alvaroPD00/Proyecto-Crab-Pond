using UnityEngine;
using UnityEngine.UI;

public class GestorDeModos : MonoBehaviour
{
    [Header("Elementos a alternar")]
    public MonoBehaviour scriptA;  // Componente que se activa en modo A
    public GameObject objetoB;     // GameObject que se activa en modo B

    [Header("Modo inicial (true = modo B activo)")]
    public bool modoInicialActivadoB = false;

    [Header("Toggle asociado (opcional)")]
    public Toggle toggle;

    void Start()
    {
        // Setear el estado inicial
        CambiarModo(modoInicialActivadoB);

        if (toggle != null)
        {
            toggle.isOn = modoInicialActivadoB;
            toggle.onValueChanged.AddListener(CambiarModo);
        }
    }

    /// <summary>
    /// Cambia entre activar el scriptA o el objetoB.
    /// true = modo B activo, false = modo A activo
    /// </summary>
    public void CambiarModo(bool activarModoB)
    {
        if (scriptA != null) scriptA.enabled = !activarModoB;
        if (objetoB != null) objetoB.SetActive(activarModoB);
    }
}
