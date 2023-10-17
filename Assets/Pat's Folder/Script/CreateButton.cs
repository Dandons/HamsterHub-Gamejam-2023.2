using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateButton : MonoBehaviour
{
    private GameObject[] allCompanion;
    public GameObject buttonPrefab;
    void Awake()
    {
        allCompanion = GameObject.FindGameObjectsWithTag("Companion");
        for(int i = 0;i<allCompanion.Length;i++)
        {
            float yAxis = 475 - (100 * i);
            GameObject button = Instantiate(buttonPrefab);
            button.transform.parent = this.transform;
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(0, yAxis, 0);
            ButtonSetting buttonSetting = button.GetComponent<ButtonSetting>();
            CompanionSubclass companion = allCompanion[i].GetComponent<CompanionSubclass>();
            buttonSetting.name.text = companion.name;
            buttonSetting.description.text = companion.description;
            buttonSetting.image.sprite = companion.icon;
        }
    }
    
}
