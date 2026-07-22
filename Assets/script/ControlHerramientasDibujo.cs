using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHerramientasDibujo : MonoBehaviour
{
    [Header("Objetos existentes en la escena")]
    public GameObject lapiz;
    public GameObject borrador;

    [Header("Objeto padre de todos los trazos")]
    public Transform contenedorTrazos;

    // Se ejecuta desde el botón del lápiz
    public void MostrarOcultarLapiz()
    {
        if (lapiz == null)
        {
            Debug.LogWarning("No se asignó el objeto Lápiz.");
            return;
        }

        bool nuevoEstado = !lapiz.activeSelf;
        lapiz.SetActive(nuevoEstado);
    }

    // Se ejecuta desde el botón del borrador
    public void MostrarOcultarBorrador()
    {
        if (borrador == null)
        {
            Debug.LogWarning("No se asignó el objeto Borrador.");
            return;
        }

        bool nuevoEstado = !borrador.activeSelf;
        borrador.SetActive(nuevoEstado);
    }

    // Se ejecuta desde el botón Reset
    public void EliminarTodosLosTrazos()
    {
        if (contenedorTrazos == null)
        {
            Debug.LogWarning("No se asignó el contenedor de trazos.");
            return;
        }

        for (int i = contenedorTrazos.childCount - 1; i >= 0; i--)
        {
            Destroy(contenedorTrazos.GetChild(i).gameObject);
        }
    }
}
