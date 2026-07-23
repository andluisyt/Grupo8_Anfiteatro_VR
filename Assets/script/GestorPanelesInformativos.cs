using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorPanelesInformativos : MonoBehaviour
{
    private GameObject panelActual;

    private void Start()
    {
        OcultarTodosLosPaneles();
    }

    public void MostrarPanel(GameObject panelNuevo)
    {
        if (panelNuevo == null)
        {
            Debug.LogWarning("No se asignó un panel informativo.");
            return;
        }

        // Oculta el panel anterior.
        if (panelActual != null && panelActual != panelNuevo)
        {
            panelActual.SetActive(false);
        }

        panelActual = panelNuevo;
        panelActual.SetActive(true);
    }

    public void OcultarPanel(GameObject panelQueSale)
    {
        if (panelActual == panelQueSale)
        {
            panelActual.SetActive(false);
            panelActual = null;
        }
    }

    public void OcultarTodosLosPaneles()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        panelActual = null;
    }
}
