using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarBorrador : MonoBehaviour
{
    [Header("Objeto borrador de la escena")]
    public GameObject borrador;

    [Header("Punto donde aparecerá el borrador")]
    public Transform puntoAparicionBorrador;

    public void MostrarOcultarBorrador()
    {
        if (borrador == null)
        {
            Debug.LogWarning("No se asignó el objeto Eraser.");
            return;
        }

        bool activar = !borrador.activeSelf;

        borrador.SetActive(activar);

        if (activar)
        {
            ColocarBorrador();
        }
    }

    private void ColocarBorrador()
    {
        if (puntoAparicionBorrador == null)
        {
            Debug.LogWarning("No se asignó PuntoBorrador.");
            return;
        }

        borrador.transform.SetPositionAndRotation(
            puntoAparicionBorrador.position,
            puntoAparicionBorrador.rotation
        );

        Rigidbody rb = borrador.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}