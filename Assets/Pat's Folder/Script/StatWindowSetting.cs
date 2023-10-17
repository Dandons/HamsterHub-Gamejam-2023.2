using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatWindowSetting : MonoBehaviour
{
    public static StatWindowSetting Instance;

    public TMP_Text name;
    public TMP_Text description;
    public Image icon;
    public Image fullSprite;
    private void Start()
    {
        Instance = this;
    }
    public void SetStatWindow(GameObject companion)
    {
        CompanionSubclass companionProp = companion.GetComponent<CompanionSubclass>();
        name.text = companionProp.name;
        description.text = companionProp.description;
        icon.sprite = companionProp.icon;
        fullSprite.sprite = companion.GetComponent<SpriteRenderer>().sprite;
    }
}
