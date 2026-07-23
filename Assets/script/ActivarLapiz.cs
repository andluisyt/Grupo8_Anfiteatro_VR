using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarLapiz : MonoBehaviour
{
    public GameObject lapiz;
    public Transform puntoAparicionLapiz;

    public void MostrarOcultarLapiz()
    {
        if (lapiz == null)
        {
            Debug.LogWarning("No se asignó el objeto Pen.");
            return;
        }

        bool activar = !lapiz.activeSelf;

        lapiz.SetActive(activar);

        if (activar)
        {
            ColocarLapiz();
        }
    }

    private void ColocarLapiz()
    {
        if (puntoAparicionLapiz == null)
        {
            Debug.LogWarning("No se asignó PuntoLapiz.");
            return;
        }

        lapiz.transform.SetPositionAndRotation(
            puntoAparicionLapiz.position,
            puntoAparicionLapiz.rotation
        );

        Rigidbody rb = lapiz.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}