using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPopUp : MonoBehaviour
{
    public GameObject UIContainer; // El GameObject vacío que contiene los elementos UI
    public float speed = 1.0f; // Velocidad de movimiento de la UI
    private Vector3 originalPosition;
    private Vector3 offscreenPosition;
    private Coroutine currentCoroutine;

    void Start()
    {
        originalPosition = UIContainer.transform.position;
        offscreenPosition = originalPosition + Vector3.down * CalculateTotalUIHeight();
        HideUI(); // Asegurarse de que al inicio esté desactivado
    }
    void Update()
    {
        
    }

    float CalculateTotalUIHeight()
    {
        float totalHeight = 0f;

        foreach (RectTransform childRect in UIContainer.GetComponentsInChildren<RectTransform>())
        {
            totalHeight += childRect.rect.height;
        }

        return totalHeight;
    }

    public void ShowUI()
    {
        UIContainer.SetActive(true);
        currentCoroutine = StartCoroutine(MoveUI(offscreenPosition, originalPosition));
    }

    public void HideUI()
    {
        currentCoroutine = StartCoroutine(MoveUI(originalPosition, offscreenPosition));
    }

    IEnumerator MoveUI(Vector3 startPos, Vector3 endPos)
    {
        float elapsedTime = 0f;
        Vector3 start = startPos;
        Vector3 end = endPos;

        while (elapsedTime < 1f)
        {
            UIContainer.transform.position = Vector3.Lerp(start, end, elapsedTime);
            elapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        UIContainer.transform.position = end;

        if (end == offscreenPosition)
        {
            UIContainer.SetActive(false);
        }
    }
}
