using UnityEngine;

public class RaycastTagBlocker : MonoBehaviour
{
    [Header("Tags a bloquear mientras este objeto est� activo")]
    public string[] tagsABloquear;

    // M�todo para que el detector de clics consulte si debe bloquear el objeto tocado
    public bool DeberiaBloquear(GameObject objeto)
    {
        foreach (string tag in tagsABloquear)
        {
            if (objeto.CompareTag(tag))
                return true;
        }
        return false;
    }
}
