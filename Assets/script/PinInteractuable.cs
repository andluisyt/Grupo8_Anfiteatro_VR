using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinInteractuable : MonoBehaviour
{
    [Header("Configuración del Panel")]
    [Tooltip("Escribe exactamente el Tag del panel correspondiente.")]
    [SerializeField] private string tagDelPanelAActivar;

    [Header("Ajustes de Posición")]
    [SerializeField] private float alturaSobreElPin = 0.3f;

    [Tooltip("Distancia del panel hacia el usuario.")]
    [SerializeField] private float distanciaHaciaUsuario = 0.1f;

    private GameObject panelAsociado;
    private Transform camaraVR;

    // Esta variable es compartida por todos los pines.
    private static GameObject panelActualmenteVisible;

    private void Start()
    {
        if (Camera.main != null)
        {
            camaraVR = Camera.main.transform;
        }
        else
        {
            Debug.LogError(
                "[PinInteractuable] No existe una cámara con el Tag MainCamera."
            );
        }

        if (string.IsNullOrEmpty(tagDelPanelAActivar))
        {
            Debug.LogWarning(
                "[PinInteractuable] El pin " + gameObject.name +
                " no tiene configurado el Tag del panel."
            );

            return;
        }

        panelAsociado =
            GameObject.FindGameObjectWithTag(tagDelPanelAActivar);

        if (panelAsociado == null)
        {
            Debug.LogError(
                "[PinInteractuable] No se encontró el panel con Tag: " +
                tagDelPanelAActivar +
                " para el pin: " + gameObject.name
            );

            return;
        }

        panelAsociado.SetActive(false);
    }

    public void MostrarInformacion()
    {
        Debug.Log(
            "Pin detectado: " + gameObject.name +
            " | Panel: " +
            (panelAsociado != null ? panelAsociado.name : "NULL")
        );

        if (panelAsociado == null)
        {
            return;
        }

        // Oculta el panel anterior.
        if (panelActualmenteVisible != null &&
            panelActualmenteVisible != panelAsociado)
        {
            panelActualmenteVisible.SetActive(false);
        }

        PosicionarPanel();

        panelAsociado.SetActive(true);
        panelActualmenteVisible = panelAsociado;

        Debug.Log(
            "Panel visible: " + panelAsociado.name +
            " | Activo: " + panelAsociado.activeInHierarchy
        );
    }

    private void PosicionarPanel()
    {
        Vector3 posicionNueva =
            transform.position + Vector3.up * alturaSobreElPin;

        if (camaraVR != null)
        {
            Vector3 direccionHaciaUsuario =
                camaraVR.position - posicionNueva;

            direccionHaciaUsuario.y = 0f;

            if (direccionHaciaUsuario.sqrMagnitude > 0.001f)
            {
                direccionHaciaUsuario.Normalize();

                posicionNueva +=
                    direccionHaciaUsuario * distanciaHaciaUsuario;
            }
        }

        panelAsociado.transform.position = posicionNueva;
        OrientarPanel();
    }

    private void LateUpdate()
    {
        if (panelAsociado != null &&
            panelAsociado.activeSelf &&
            camaraVR != null)
        {
            OrientarPanel();
        }
    }

    private void OrientarPanel()
    {
        Vector3 direccionHaciaCamara =
            camaraVR.position - panelAsociado.transform.position;

        direccionHaciaCamara.y = 0f;

        if (direccionHaciaCamara.sqrMagnitude > 0.001f)
        {
            panelAsociado.transform.rotation =
                Quaternion.LookRotation(-direccionHaciaCamara);
        }
    }

    public void OcultarInformacion()
    {
        if (panelAsociado == null)
        {
            return;
        }

        panelAsociado.SetActive(false);

        if (panelActualmenteVisible == panelAsociado)
        {
            panelActualmenteVisible = null;
        }
    }
}