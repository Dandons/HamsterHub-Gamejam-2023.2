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
    
    [SerializeField] float moveSpeed;
    private Vector2 input;
    private Rigidbody2D rb;

    private void Start()
    {
        Instance = this;
        playerProperty = new PlayerProperty(baseatk: baseAtk, basehp: baseHp, basemp: baseMp, basedef: baseDef, basehpRegen: baseHpRegen, basempRegen: baseMpRegen);
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        input.Normalize();
        rb.velocity = new Vector2(input.x * moveSpeed,  input.y * moveSpeed);

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
