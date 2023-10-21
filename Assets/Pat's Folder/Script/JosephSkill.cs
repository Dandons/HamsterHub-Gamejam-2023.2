using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JosephSkill : Skill
{
    public override string skillName { get => "Joseph..Beammmmm"; }
    public override float baseDamage => 150;
    public override float baseMpUsage => 60;

    public override void UseSkill(Vector3 position,Collider2D enemy,float damage,float castDistance, int skillLv)
    {
        float damageAmplfier = (150 + (skillLv * 5)) * 0.01f;
        float damageOut = damage * damageAmplfier;
        float coneAngle = 30f;
        Vector2 direction = enemy.transform.position - position;
        Quaternion coneRotation = Quaternion.AngleAxis(-coneAngle / 2, Vector3.forward);
        for (int i = 0; i < 360; i += 10) // You can adjust the increment for smoother or coarser results.
        {
            Vector2 rotatedDirection = coneRotation * direction;
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, rotatedDirection, castDistance);
            for (int j = 0; j < hit.Length; j++)
            {
                meleeEnemy mEnemy;
                rangeEmemy rEnemy;
                if (hit[j].collider.TryGetComponent(out mEnemy))
                {
                    hit[j].collider.GetComponent<meleeEnemy>().myenemy.takeDamge(damageOut);
                }
                if (hit[j].collider.TryGetComponent(out rEnemy))
                {
                    hit[j].collider.GetComponent<rangeEmemy>().myenemy.takeDamge(damageOut);
                }
            }
            coneRotation *= Quaternion.AngleAxis(10, Vector3.forward); // Rotate the direction for the next ray.
        }
    }
}
