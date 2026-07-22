using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ColorBotonReset : MonoBehaviour
{
    [Header("Imagen visual del botón Reset")]
    public Image imagenBoton;

    [Header("Duración del color blanco")]
    public float duracionBlanco = 0.25f;

    private readonly Color32 colorActivo =
        new Color32(255, 255, 255, 255);

    private readonly Color32 colorGris =
        new Color32(180, 180, 180, 255);

    private Coroutine rutinaColor;

    void Start()
    {
        if (imagenBoton != null)
        {
            imagenBoton.color = colorGris;
        }
    }

    public void MostrarPulsacion()
    {
        if (imagenBoton == null)
        {
            Debug.LogWarning("No se asignó la imagen del botón Reset.");
            return;
        }

        if (rutinaColor != null)
        {
            StopCoroutine(rutinaColor);
        }

        rutinaColor = StartCoroutine(CambiarColorTemporalmente());
    }

    private IEnumerator CambiarColorTemporalmente()
    {
        imagenBoton.color = colorActivo;

        yield return new WaitForSeconds(duracionBlanco);

        imagenBoton.color = colorGris;

        rutinaColor = null;
    }
}