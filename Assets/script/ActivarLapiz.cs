using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarLapiz : MonoBehaviour
{
    [Header("Objeto lápiz de la escena")]
    public GameObject lapiz;

    void Start()
    {
        if (lapiz != null)
        {
            lapiz.SetActive(false);
        }
    }

    public void MostrarOcultarLapiz()
    {
        if (lapiz == null)
        {
            Debug.LogWarning("No se ha asignado el objeto Pen.");
            return;
        }

        lapiz.SetActive(!lapiz.activeSelf);
    }
}