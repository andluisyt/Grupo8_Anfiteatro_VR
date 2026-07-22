using UnityEngine;
using UnityEngine.UI;

public class ResetButtonColor : MonoBehaviour
{
    public Image buttonImage;

    private readonly Color32 colorOn =
        new Color32(255, 255, 255, 255);

    private readonly Color32 colorOff =
        new Color32(180, 180, 180, 255);

    private bool activo = false;

    private void Start()
    {
        if (buttonImage != null)
        {
            buttonImage.color = colorOff;
        }
    }

    public void OnPress()
    {
        if (buttonImage == null)
        {
            Debug.LogWarning(
                "No se asignó la imagen del botón Reset."
            );

            return;
        }

        activo = !activo;

        if (activo)
        {
            buttonImage.color = colorOn;
        }
        else
        {
            buttonImage.color = colorOff;
        }
    }
}