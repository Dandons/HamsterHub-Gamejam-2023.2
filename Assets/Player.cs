using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> 
{
    public PlayerProperty playerProperty;
    public static Player Instance;
    [SerializeField] float baseAtk;
    [SerializeField] float baseHp;
    [SerializeField] float baseMp;
    [SerializeField] float baseDef;
    [SerializeField] float baseHpRegen;
    [SerializeField] float baseMpRegen;
    [SerializeField] float atkRange;
    public int Coin;
    public int Tear;

    private void Start()
    {
        Instance = this;
        playerProperty = new PlayerProperty(baseatk: baseAtk, basehp: baseHp, basemp: baseMp, basedef: baseDef, basehpRegen: baseHpRegen, basempRegen: baseMpRegen);
    }
    public void TakeDamage(float damage)
    {
        if (playerProperty.def > damage) { playerProperty.currentHp -= damage * 0.1f; }
        else { playerProperty.currentHp -= damage - playerProperty.def; }
    }
    private void Regeneration()
    {
        StartCoroutine(Regen());
    }
    IEnumerator Regen()
    {
        yield return new WaitForSeconds(1);
        playerProperty.currentHp += playerProperty.hpRegen;
        playerProperty.currentMp += playerProperty.mpRegen;
    }
}
