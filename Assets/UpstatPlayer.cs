using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpstatPlayer : MonoBehaviour
{
    public Button hp;
    public Button mp;
    public Button atk;
    public Button def;
    public Button heal;
    public Button fireball;
    public void upstat(int index)
    {
        switch (index)
        {
            case 0:
                if(Player.Instance.Coin >= 15 * Player.Instance.playerProperty.HpLv)
                {
                    Player.Instance.Coin -= 15 * Player.Instance.playerProperty.HpLv;
                    Player.Instance.playerProperty.HpLv += 1;
                }
                break;
            case 1:
                if(Player.Instance.Coin >= 15 * Player.Instance.playerProperty.MpLv)
                {
                    Player.Instance.Coin -= 15 * Player.Instance.playerProperty.MpLv;
                    Player.Instance.playerProperty.MpLv += 1;
                }
                break;
            case 2:
                if(Player.Instance.Coin >= 15 * Player.Instance.playerProperty.AtkLv)
                {
                    Player.Instance.Coin -= 15 * Player.Instance.playerProperty.AtkLv;
                    Player.Instance.playerProperty.AtkLv += 1;
                }
                break;
            case 3:
                if(Player.Instance.Coin >= 15* Player.Instance.playerProperty.DefLv)
                {
                    Player.Instance.Coin -= 15 * Player.Instance.playerProperty.DefLv;
                    Player.Instance.playerProperty.DefLv += 1;
                }
                break;
            case 4:
                //upstat PLayer's heal
                break;
            case 5:
                //upstat Player's fireball
                break;
        }
    }

}
