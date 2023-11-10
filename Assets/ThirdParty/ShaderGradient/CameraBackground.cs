using UnityEngine;

public class CameraBackground : MonoBehaviour
{
    public Camera mainCamera; // Referencia a la cámara principal

    void Update()
    {
        // Asegúrate de que el quad siempre esté detrás de todo lo demás y enfrente de la cámara
        // Se posiciona justo dentro del far clip plane de la cámara.
        float distanceFromCamera = mainCamera.farClipPlane - 20f;
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;

        // Ajusta el tamaño del quad según el tamaño de la vista de la cámara
        float height = 2f * distanceFromCamera * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float width = height * mainCamera.aspect;
        transform.localScale = new Vector3(width, height, 1f);

        // Rota el quad para que siempre mire hacia la cámara
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                         mainCamera.transform.rotation * Vector3.up);
    }
}
