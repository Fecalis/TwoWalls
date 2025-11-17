using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteAlways]
public class CreatureCard : MonoBehaviour
{
    [Header("Основные данные карты")]
    public string cardName = "Безымянное Существо";
    [TextArea(2, 4)] public string description = "Описание эффекта существа.";
    public string lineage = "Бестия";
    public int attack = 3;
    public int health = 5;
    public int cost = 2;
    public Sprite artwork;

    [Header("UI элементы (назначить вручную)")]
    public TMP_Text topLeftText;
    public TMP_Text topRightText;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text attackText;
    public TMP_Text healthText;
    public SpriteRenderer artworkImage;

    private void OnValidate() => UpdateCard();
    private void Start() => UpdateCard();

    public void UpdateCard()
    {
        if (topLeftText) topLeftText.text = lineage;
        if (topRightText) topRightText.text = cost.ToString();
        if (nameText) nameText.text = cardName;
        if (descriptionText) descriptionText.text = description;
        if (attackText) attackText.text = attack.ToString();
        if (healthText) healthText.text = health.ToString();
        if (artworkImage && artwork) artworkImage.sprite = artwork;
    }
}
