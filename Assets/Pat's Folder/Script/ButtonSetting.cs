using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetting : MonoBehaviour
{
    //property in mini bar
    public TMP_Text name;
    public TMP_Text description;
    public Image image;
    public GameObject companion;

    Button button;

    private void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(()=>StatWindowSetting.Instance.SetStatWindow(companion));
    }

}
