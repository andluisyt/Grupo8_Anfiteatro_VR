using UnityEngine;

public class TransferPropsToSkinMaterial_VR : MonoBehaviour
{
    public Material skinMaterial;   // asignar UnlockedSkin o LockedSkin
    public Transform aR_Window;

    void Update()
    {
        if (aR_Window == null || skinMaterial == null) return;
        skinMaterial.SetVector("_CenterPoint", aR_Window.position);
    }
}