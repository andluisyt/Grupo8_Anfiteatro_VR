using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirarAlUsuario : MonoBehaviour
{
    private Transform camaraPrincipal;

    void Start()
    {
        // Buscamos la cámara del visor VR al iniciar
        if (Camera.main != null)
        {
            camaraPrincipal = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        if (camaraPrincipal != null)
        {
            // Hace que el panel rote para mirar directamente al visor Quest
            transform.LookAt(transform.position + camaraPrincipal.forward);
        }
    }
}