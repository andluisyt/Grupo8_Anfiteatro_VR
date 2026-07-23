using System.Collections;
using UnityEngine;

public class ResetTrazos : MonoBehaviour
{
    public void EliminarTodosLosTrazos()
    {
        GameObject contenedor = GameObject.Find("Drawing Container");

        if (contenedor == null)
        {
            Debug.Log("RESET: No existen trazos para eliminar.");
            return;
        }

        Transform contenedorTransform = contenedor.transform;

        for (int i = contenedorTransform.childCount - 1; i >= 0; i--)
        {
            GameObject trazo =
                contenedorTransform.GetChild(i).gameObject;

            trazo.SetActive(false);
            Destroy(trazo);
        }
    }
}