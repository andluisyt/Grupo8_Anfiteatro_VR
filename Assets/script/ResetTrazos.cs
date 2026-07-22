using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ResetTrazos : MonoBehaviour
{
    [Header("Imagen visual del botón Reset")]
    public Image imagenBoton;

    [Header("Tiempo que permanece blanco")]
    public float duracionBlanco = 0.25f;

    private readonly Color32 colorBlanco =
        new Color32(255, 255, 255, 255);

    private readonly Color32 colorGris =
        new Color32(180, 180, 180, 255);

    public void EliminarTodosLosTrazos()
    {
        // Busca el contenedor que aparece durante la ejecución
        GameObject contenedor =
            GameObject.Find("Drawing Container");

        if (contenedor == null)
        {
            Debug.LogWarning(
                "RESET: No se encontró Drawing Container."
            );

            return;
        }

        Transform transformContenedor = contenedor.transform;

        Debug.Log(
            "RESET: Trazos encontrados: "
            + transformContenedor.childCount
        );

        // Elimina todos los objetos Drawing
        for (int i = transformContenedor.childCount - 1; i >= 0; i--)
        {
            GameObject trazo =
                transformContenedor.GetChild(i).gameObject;

            // Lo oculta inmediatamente
            trazo.SetActive(false);

            // Lo elimina al terminar el frame
            Destroy(trazo);
        }

        // Efecto visual del botón
        if (imagenBoton != null)
        {
            StopAllCoroutines();
            StartCoroutine(EfectoColor());
        }
    }

    private IEnumerator EfectoColor()
    {
        imagenBoton.color = colorBlanco;

        yield return new WaitForSeconds(duracionBlanco);

        imagenBoton.color = colorGris;
    }
}