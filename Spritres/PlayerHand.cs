using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [Header("Параметры раскладки")]
    public List<DraggableCard> cardsInHand = new List<DraggableCard>();
    public float radius = 600f;           // Радиус дуги руки
    public float angleSpread = 25f;       // Угол между крайними картами
    public float centerOffsetY = -200f;   // Смещение центра дуги вниз
    public float cardSpacing = 0.2f;      // Интервал для плавности

    [Header("Ограничения области руки")]
    public RectTransform handArea;        // Область, где должны находиться карты

    private void UpdateHandLayout()
    {
        if (cardsInHand.Count == 0) return;

        // Центральный угол
        float startAngle = -angleSpread / 2f;
        float step = angleSpread / Mathf.Max(cardsInHand.Count - 1, 1);

        for (int i = 0; i < cardsInHand.Count; i++)
        {
            DraggableCard card = cardsInHand[i];
            if (card == null) continue;

            // Расчёт позиции по дуге
            float angle = startAngle + step * i;
            float rad = Mathf.Deg2Rad * angle;

            Vector3 pos = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius + centerOffsetY, 0f);
            card.SetTargetPosition(pos);
            card.SetRotation(angle * -0.6f); // лёгкий поворот для эстетики
            card.SetSortingOrder(i);
        }
    }

    public void AddCard(DraggableCard card)
    {
        card.transform.SetParent(transform, false);
        cardsInHand.Add(card);
        UpdateHandLayout();
    }

    public void ReorderCard(DraggableCard card, int newIndex)
    {
        cardsInHand.Remove(card);
        cardsInHand.Insert(newIndex, card);
        UpdateHandLayout();
    }

    private void Update()
    {
        UpdateHandLayout();

        // Ограничиваем карты внутри handArea (если есть)
        if (handArea)
        {
            foreach (var card in cardsInHand)
            {
                Vector3 pos = card.GetComponent<RectTransform>().anchoredPosition;
                float halfWidth = handArea.rect.width / 2f - 100f; // отступы
                pos.x = Mathf.Clamp(pos.x, -halfWidth, halfWidth);
                card.GetComponent<RectTransform>().anchoredPosition = pos;
            }
        }
    }
}
