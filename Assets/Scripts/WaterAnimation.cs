using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Inicia la coroutine al comenzar.
        StartCoroutine(MoveOverSeconds(gameObject, new Vector3(transform.position.x, -0.58f, transform.position.z), 10));
    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < seconds)
        {
            // Interpola la posición Y del objeto durante el tiempo especificado.
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Asegura que el objeto llegue a la posición final.
        objectToMove.transform.position = end;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
