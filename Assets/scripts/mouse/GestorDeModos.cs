using UnityEngine;
using UnityEngine.UI;

public class GestorDeModos : MonoBehaviour
{
    [Header("Objetos a alternar")]
    public GameObject modoA;
    public GameObject modoB;

    [Header("Modo inicial (true = modoB activo)")]
    public bool modoInicialActivadoB = false;

    [Header("Toggle asociado (opcional)")]
    public Toggle toggle; // Asignalo solo si quer�s que el script controle tambi�n el estado visual del bot�n

    void Start()
    {
        // Setear estado inicial
        CambiarModo(modoInicialActivadoB);

        // Si hay un Toggle vinculado, sincronizar su valor
        if (toggle != null)
        {
            toggle.isOn = modoInicialActivadoB;
            toggle.onValueChanged.AddListener(CambiarModo);
        }
    }

    /// <summary>
    /// Cambia el estado de los objetos seg�n el valor del toggle.
    /// true = modoB activo, false = modoA activo
    /// </summary>
    public void CambiarModo(bool estado)
    {
        if (modoA != null) modoA.SetActive(!estado);
        if (modoB != null) modoB.SetActive(estado);
    }
}
