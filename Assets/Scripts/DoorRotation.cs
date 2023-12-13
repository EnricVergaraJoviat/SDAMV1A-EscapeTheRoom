using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openSpeed = 1f; // Velocidad de apertura
    public float openAngle = 90f; // Ángulo de la puerta cuando está abierta
    public float waitTime = 2f; // Tiempo de espera antes de que la puerta se abra o se cierre

    private bool isOpen = false;

    void Start()
    {
        StartCoroutine(OpenAndCloseDoor()); // Inicia la corutina para abrir y cerrar la puerta
    }

    IEnumerator OpenAndCloseDoor()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime); // Espera un tiempo determinado

            if (isOpen)
            {
                StartCoroutine(RotateDoor(0)); // Inicia la corutina para cerrar la puerta
            }
            else
            {
                StartCoroutine(RotateDoor(openAngle)); // Inicia la corutina para abrir la puerta
            }

            isOpen = !isOpen;
        }
    }

    IEnumerator RotateDoor(float targetAngle)
    {
        Quaternion fromAngle = transform.rotation;
        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles.x, targetAngle, transform.eulerAngles.z);
        float elapsedTime = 0.0f;

        while (elapsedTime < openSpeed)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, elapsedTime / openSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = toAngle;
    }
}