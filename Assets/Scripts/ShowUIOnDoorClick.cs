using System.Collections;
using UnityEngine;

public class ShowUIOnDoorClick : MonoBehaviour
{
    public GameObject doorObject;
    public GameObject UIContainer; // El GameObject vac�o que contiene los elementos UI
    public float speed = 1.0f; // Velocidad de movimiento de la UI
    private Vector3 originalPosition;
    private Vector3 offscreenPosition;
    private Coroutine currentCoroutine;

    void Start()
    {
        originalPosition = UIContainer.transform.position;
        offscreenPosition = originalPosition + Vector3.down * CalculateTotalUIHeight();
        HideUI(); // Asegurarse de que al inicio est� desactivado
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;

            if (objectHit == doorObject && Input.GetMouseButtonDown(0))
            {
                if (!UIContainer.activeSelf)
                {
                    ShowUI();
                }
                else
                {
                    HideUI();
                }
            }
        }
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

    void ShowUI()
    {
        UIContainer.SetActive(true);
        currentCoroutine = StartCoroutine(MoveUI(offscreenPosition, originalPosition));
    }

    void HideUI()
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
