using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinInteractuable : MonoBehaviour
{
    [Header("Configuracion del Panel")]
    [Tooltip("Escribe aqui exactamente el Tag que le pusiste al Canvas flotante en el Inspector.")]
    [SerializeField] private string tagDelPanelAActivar;

    [Header("Ajustes de Posicion Dinamica")]
    [Tooltip("¿Que tan arriba del pin quieres que aparezca el letrero? (En metros)")]
    [SerializeField] private float alturaSobreElPin = 0.2f; // 20 centimetros por defecto

    private GameObject panelAsociado;
    private Transform camaraVR;

    private void Start()
    {
        // 1. Buscamos la camara principal de las Meta Quest (el jugador)
        if (Camera.main != null)
        {
            camaraVR = Camera.main.transform;
        }

        // 2. VALIDACIÓN CRÍTICA: Validamos si la casilla del Tag no esta vacia antes de buscar
        if (!string.IsNullOrEmpty(tagDelPanelAActivar) && tagDelPanelAActivar.Trim() != "")
        {
            panelAsociado = GameObject.FindGameObjectWithTag(tagDelPanelAActivar);
            
            if (panelAsociado != null)
            {
                // --- POSICIONAMIENTO AUTOMÁTICO ---
                // Teletransporta el panel exactamente sobre el pin sumando la altura en Y
                Vector3 posicionNueva = transform.position + new Vector3(0, alturaSobreElPin, 0);
                panelAsociado.transform.position = posicionNueva;

                // Una vez acomodado en su sitio, lo apagamos para que empiece oculto
                panelAsociado.SetActive(false); 
            }
            else
            {
                // Si el tag está escrito pero te equivocaste en una letra o no existe en la escena
                Debug.LogError($"[PinInteractuable] No se encontro ningun panel con el Tag: '{tagDelPanelAActivar}' en el objeto '{gameObject.name}'. Revisa si el panel esta encendido en la jerarquia.");
            }
        }
        else
        {
            // Si la casilla está vacía, solo lanza un aviso amarillo amistoso sin romper el simulador
            Debug.LogWarning($"[PinInteractuable] El objeto '{gameObject.name}' tiene el script asignado pero su casilla 'Tag Del Panel A Activar' esta vacia.");
        }
    }

    private void Update()
    {
        // --- ROTACIÓN AUTOMÁTICA (BILLBOARD) ---
        // Si el panel esta activo y la camara existe, hacemos que mire al jugador en tiempo real
        if (panelAsociado != null && panelAsociado.activeSelf && camaraVR != null)
        {
            // Calculamos el vector de direccion hacia el usuario
            Vector3 direccionHaciaCamara = camaraVR.position - panelAsociado.transform.position;
            
            // Forzamos a que solo rote en el eje Y (evita que el panel se incline raro si miras hacia arriba/abajo)
            direccionHaciaCamara.y = 0; 

            // Aplicamos la rotacion invertida (-) para que las letras no se vean en modo espejo
            if (direccionHaciaCamara != Vector3.zero)
            {
                panelAsociado.transform.rotation = Quaternion.LookRotation(-direccionHaciaCamara);
            }
        }
    }

    // --- METODOS PUBLICOS PARA LOS EVENTOS HOVER (XR SIMPLE INTERACTABLE) ---

    public void MostrarInformacion()
    {
        if (panelAsociado != null)
        {
            panelAsociado.SetActive(true);
        }
    }

    public void OcultarInformacion()
    {
        if (panelAsociado != null)
        {
            panelAsociado.SetActive(false);
        }
    }
}