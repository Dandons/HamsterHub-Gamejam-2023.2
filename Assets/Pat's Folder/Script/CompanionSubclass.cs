using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionSubclass : MonoBehaviour
{
    [SerializeField] float baseAtk;
    [SerializeField] float baseHp;
    [SerializeField] float baseMp;
    [SerializeField] float baseDef;
    [SerializeField] float baseHpRegen;
    [SerializeField] float baseMpRegen;
    public Companion companion;
    public Companion.Rarity rarity;
    public Companion.AttacKType attacKType;
    private void Start()
    {
        companion = new Companion(baseatk: baseAtk, basehp: baseHp, basemp: baseMp, basedef: baseDef, basehpRegen: baseHpRegen, basempRegen: baseMpRegen);
        companion.rarity = rarity;
        companion.attackType = attacKType;
    }
    public void TakeDamage(float damage)
    {
        companion.currentHp -= damage;
    }
    public bool DetectEnemy()
    {

        return false;
    }
    public void Update()
    {

    }
}
