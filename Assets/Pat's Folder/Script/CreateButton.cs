using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreateButton : MonoBehaviour
{
    private GameObject[] allCompanion;
    public GameObject buttonPrefab;
    void Awake()
    {
        allCompanion = GameObject.FindGameObjectsWithTag("Companion");
        for(int i = 0;i<allCompanion.Length;i++)
        {
            //instantiate button
            float yAxis = 475 - (100 * i);
            GameObject button = Instantiate(buttonPrefab);
            button.transform.parent = this.transform;

            //GetComponent for setting value in button
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            ButtonSetting buttonSetting = button.GetComponent<ButtonSetting>();
            Companion companion = allCompanion[i].GetComponent<Companion>();

            //Set value
            rectTransform.anchoredPosition = new Vector3(0, yAxis, 0);
            buttonSetting.name.text = companion.name;
            buttonSetting.description.text = companion.description;
            buttonSetting.image.sprite = companion.icon;
            buttonSetting.companion = allCompanion[i];
            
        }
    }
    
}
