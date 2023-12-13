using UnityEngine;

public class SimpleHighlight : MonoBehaviour
{
    private Renderer rend;
    private Color startColor;
    private Color hoverColor = Color.red;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
