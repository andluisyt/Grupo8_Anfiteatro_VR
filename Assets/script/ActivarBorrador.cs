using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarBorrador : MonoBehaviour
{
    [Header("Objeto borrador de la escena")]
    public GameObject borrador;

    public void MostrarOcultarBorrador()
    {
        if (borrador == null)
        {
            Debug.LogWarning("No se ha asignado el objeto Eraser.");
            return;
        }

        borrador.SetActive(!borrador.activeSelf);
    }
}