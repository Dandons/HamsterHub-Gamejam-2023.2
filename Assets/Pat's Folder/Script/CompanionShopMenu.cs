using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompanionShopMenu : MonoBehaviour
{
    public Image icon;
    public Image fullSprite;
    public TMP_Text name;
    public TMP_Text description;

    private static GameObject companion;

    public void SelectedCompanion(GameObject Companion)
    {
        companion = Companion;
        SetStatWindow();
    }
    private void SetStatWindow()
    {
        Companion companionProperty = companion.GetComponent<Companion>();
        icon.sprite = companionProperty.icon;
        fullSprite.sprite = companion.GetComponent<SpriteRenderer>().sprite;
        name.text = companionProperty.name;
        description.text = companionProperty.description;
    }
}
