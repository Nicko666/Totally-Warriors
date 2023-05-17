using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitMenu : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] Canvas canvas;
    [SerializeField] int currentUnit;
    [SerializeField] Color color;

    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    Vector3 basicPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        basicPosition = rectTransform.localPosition;

    }

    public void OnBeginDrag(PointerEventData eventData) => canvasGroup.blocksRaycasts = false;

    public void OnDrag(PointerEventData eventData) => rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        rectTransform.localPosition = basicPosition;
    }

    public void OnPointerClick(PointerEventData eventData) => ChangeUnit();

    public void ChangeUnit()
    {
        currentUnit = Mathf.RoundToInt(Mathf.Repeat(++currentUnit, BattleManager.Instance.Units.Count));
        Debug.Log("ChangeUnit");

    }

    public Unit GetUnit()
    {
        return BattleManager.Instance.Units[currentUnit];

    }

}
