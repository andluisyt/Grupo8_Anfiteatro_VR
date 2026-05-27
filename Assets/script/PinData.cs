using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PinData : MonoBehaviour
{
    [Header("Configuración de UI")]
    public GameObject panel; // El Canvas que está en "InformacionDePines"

    [Header("Ajuste de Posición")]
    [Tooltip("Distancia en metros por encima del pin para que flote el panel")]
    public float alturaDesfase = 0.15f; 

    private XRSimpleInteractable interactable;
    private Transform camaraPrincipal;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        if (panel != null)
            panel.SetActive(false);

        if (Camera.main != null)
        {
            camaraPrincipal = Camera.main.transform;
        }
    }

    void OnEnable()
    {
        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(OnPinDetectado);
            interactable.hoverExited.AddListener(OnPinPerdido);
        }
    }

    void OnDisable()
    {
        if (interactable != null)
        {
            interactable.hoverEntered.RemoveListener(OnPinDetectado);
            interactable.hoverExited.RemoveListener(OnPinPerdido);
        }
    }

    void LateUpdate()
    {
        // SEGURIDAD: Si te olvidaste de arrastrar el panel en el inspector, 
        // el código se salta esta parte y evita que salga el error rojo en consola.
        if (panel == null) return; 

        // Si el panel está activo, lo posicionamos y rotamos
        if (panel.activeSelf)
        {
            Vector3 posicionPin = transform.position;
            panel.transform.position = new Vector3(posicionPin.x, posicionPin.y + alturaDesfase, posicionPin.z);

            if (camaraPrincipal != null)
            {
                panel.transform.LookAt(panel.transform.position + camaraPrincipal.forward);
            }
        }
    }

    private void OnPinDetectado(HoverEnterEventArgs args)
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    private void OnPinPerdido(HoverExitEventArgs args)
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
}