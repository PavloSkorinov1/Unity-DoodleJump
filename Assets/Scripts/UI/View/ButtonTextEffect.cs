using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

namespace UI.View
{
    [RequireComponent(typeof(Button))] 
    public class ButtonTextEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private float pressOffset = -20f;

    private Vector3 originalLocalPosition;

    void Awake()
    {
        Initialise();
    }

    private void Initialise()
    {
        if (buttonText == null)
        {
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText == null)
            {
                Debug.LogError("ButtonTextEffect: No Text child found", this);
                enabled = false;
                return;
            }
        }

        originalLocalPosition = buttonText.rectTransform.localPosition;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.rectTransform.localPosition = originalLocalPosition + new Vector3(0, pressOffset, 0);
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.rectTransform.localPosition = originalLocalPosition;
        }
    }
    
    void OnDisable()
    {
        if (buttonText != null)
        {
            buttonText.rectTransform.localPosition = originalLocalPosition;
        }
    }
    }
}
