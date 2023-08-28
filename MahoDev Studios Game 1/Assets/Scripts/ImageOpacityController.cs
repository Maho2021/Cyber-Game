using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageOpacityController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private Color originalColor;
    private Color hoverColor;

    private void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
        hoverColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = originalColor; // Opacity 100%
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = hoverColor; // Opacity 50%
    }
}

