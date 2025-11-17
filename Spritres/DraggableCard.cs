using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class DraggableCard : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI настройки")]
    public float hoverScale = 1.1f;
    public float hoverLift = 80f;
    public Color highlightColor = new Color(1, 1, 1, 0.25f);
    public float moveSpeed = 10f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector3 defaultScale;
    private bool isDragging = false;
    private bool isHovering = false;
    private bool isReleased = false;

    private CanvasGroup canvasGroup;
    private Image highlight;
    private RectTransform rect;
    private PlayerHand hand;
    private Canvas canvas;

    private Vector3 lastReleasedPosition; // последн€€ позици€, где карта была отпущена

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        hand = GetComponentInParent<PlayerHand>();
        canvas = GetComponentInParent<Canvas>();

        // создаЄм прозрачную подсветку
        highlight = new GameObject("Highlight").AddComponent<Image>();
        highlight.transform.SetParent(transform, false);
        highlight.rectTransform.anchorMin = Vector2.zero;
        highlight.rectTransform.anchorMax = Vector2.one;
        highlight.rectTransform.offsetMin = Vector2.zero;
        highlight.rectTransform.offsetMax = Vector2.zero;
        highlight.color = new Color(1, 1, 1, 0);
        highlight.raycastTarget = false;

        defaultScale = transform.localScale;
        lastReleasedPosition = rect.anchoredPosition; // начальна€ позици€
    }

    public void SetTargetPosition(Vector3 position)
    {
        if (!isDragging && !isReleased)
            targetPosition = position;
    }

    public void SetRotation(float zRot)
    {
        targetRotation = Quaternion.Euler(0, 0, zRot);
    }

    public void SetSortingOrder(int order)
    {
        var canvas = GetComponent<Canvas>();
        if (canvas)
            canvas.sortingOrder = order;
    }

    private void Update()
    {
        // если карту не тащат Ч она плавно двигаетс€ к целевой позиции
        if (!isDragging)
        {
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, targetPosition, Time.deltaTime * moveSpeed);
            rect.localRotation = Quaternion.Lerp(rect.localRotation, targetRotation, Time.deltaTime * moveSpeed);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        isReleased = false;
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(HoverAnimation(true));
        isDragging = false;
        isReleased = true; // карта УотпущенаФ вручную, не управл€етс€ layout
        canvasGroup.blocksRaycasts = true;

        // запоминаем, где именно отпустили карту
        lastReleasedPosition = rect.anchoredPosition;
        targetPosition = lastReleasedPosition;

        StartCoroutine(HoverAnimation(false));

        // при желании можно добавить проверку: если отпустили вне допустимой зоны Ч вернуть назад
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragging) return;
        isHovering = true;
        //highlight.color = highlightColor;

        StopAllCoroutines();
      //  StartCoroutine(HoverAnimation(true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging) return;
        isHovering = false;
        //highlight.color = new Color(1, 1, 1, 0);

        StopAllCoroutines();
       //   StartCoroutine(HoverAnimation(true));
      //  StartCoroutine(HoverAnimation(false));
    }

    private IEnumerator HoverAnimation(bool up)
    {
        Vector3 startPos = rect.anchoredPosition;
        Vector3 endPos = up ? startPos + Vector3.up * hoverLift : lastReleasedPosition;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = up ? defaultScale * hoverScale : defaultScale;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 6f;
            rect.anchoredPosition = Vector3.Lerp(startPos, endPos, t);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
    }
}
