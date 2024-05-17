using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Sprite beforeHoverSprite;
    Image imageComp;
    Sprite defSprite;
    void Start()
    {
        imageComp = GetComponent<Image>();
        defSprite =imageComp.sprite;
    }
   
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        imageComp.sprite = beforeHoverSprite;
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        imageComp.sprite = defSprite;
    }
}
