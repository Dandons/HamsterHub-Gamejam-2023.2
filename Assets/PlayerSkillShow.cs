using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSkillShow : MonoBehaviour
{
    public TMP_Text heal;
    public TMP_Text fireball;
    public TMP_Text upHeal;
    public TMP_Text upFireball;

    private void Update()
    {
        heal.GetComponent<TMP_Text>().text = "Heal Level : ";
        fireball.GetComponent<TMP_Text>().text = "Heal Level : ";
    }
}
