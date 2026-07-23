using System.Collections;
using System.Collections;
using UnityEngine;

public class PinPanelInformativo : MonoBehaviour
{
    [Header("Administrador de paneles")]
    public GestorPanelesInformativos gestorPaneles;

    [Header("Panel correspondiente")]
    public GameObject panelCorrespondiente;

    [Header("Posición del panel")]
    public float alturaSobreElPin = 0.35f;
    public float distanciaHaciaElUsuario = 0.08f;

    [Header("Orientación")]
    public Transform camaraVR;
    public bool orientarHaciaCamara = true;
    public bool girarPanel180 = true;

    [Header("Cierre")]
    public float retrasoParaOcultar = 0.15f;

    private Coroutine rutinaOcultar;
    private bool rayoSobreElPin;

    public void MostrarInformacion()
    {
        Debug.Log("ENTRÓ EL RAYO AL PIN: " + gameObject.name);

        rayoSobreElPin = true;

        if (rutinaOcultar != null)
        {
            StopCoroutine(rutinaOcultar);
            rutinaOcultar = null;
        }

        if (gestorPaneles == null)
        {
            Debug.LogError("No se asignó el gestor en " + gameObject.name);
            return;
        }

        if (panelCorrespondiente == null)
        {
            Debug.LogError("No se asignó el panel en " + gameObject.name);
            return;
        }

        ColocarPanelSobreElPin();
        gestorPaneles.MostrarPanel(panelCorrespondiente);
    }

    public void OcultarInformacion()
    {
        Debug.Log("SALIÓ EL RAYO DEL PIN: " + gameObject.name);

        rayoSobreElPin = false;

        if (rutinaOcultar != null)
        {
            StopCoroutine(rutinaOcultar);
        }

        rutinaOcultar = StartCoroutine(OcultarConRetraso());
    }

    private void ColocarPanelSobreElPin()
    {
        Transform camaraUsada = camaraVR;

        if (camaraUsada == null && Camera.main != null)
        {
            camaraUsada = Camera.main.transform;
        }

        // Posición inicial: encima del pin.
        Vector3 nuevaPosicion =
            transform.position + Vector3.up * alturaSobreElPin;

        // Acercarlo ligeramente hacia el usuario para evitar
        // que quede dentro del modelo anatómico.
        if (camaraUsada != null)
        {
            Vector3 direccionHaciaUsuario =
                (camaraUsada.position - nuevaPosicion).normalized;

            nuevaPosicion +=
                direccionHaciaUsuario * distanciaHaciaElUsuario;
        }

        panelCorrespondiente.transform.position = nuevaPosicion;

        // Hacer que el panel mire hacia el usuario.
        if (orientarHaciaCamara && camaraUsada != null)
        {
            panelCorrespondiente.transform.LookAt(camaraUsada.position);

            if (girarPanel180)
            {
                panelCorrespondiente.transform.Rotate(0f, 180f, 0f);
            }
        }
    }

    private IEnumerator OcultarConRetraso()
    {
        yield return new WaitForSecondsRealtime(retrasoParaOcultar);

        if (!rayoSobreElPin &&
            gestorPaneles != null &&
            panelCorrespondiente != null)
        {
            gestorPaneles.OcultarPanel(panelCorrespondiente);
        }

        rutinaOcultar = null;
    }

    private void OnDisable()
    {
        rayoSobreElPin = false;

        if (rutinaOcultar != null)
        {
            StopCoroutine(rutinaOcultar);
            rutinaOcultar = null;
        }

        if (gestorPaneles != null &&
            panelCorrespondiente != null)
        {
            gestorPaneles.OcultarPanel(panelCorrespondiente);
        }
    }
}