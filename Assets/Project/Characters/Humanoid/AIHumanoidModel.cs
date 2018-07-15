using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHumanoidModel : HumanoidModel {
    
    [SerializeField]
    private HumanoidTargeter targeter;

    public override void EffectOnSeeEnemy(HumanoidModel enemy)
    {
        base.EffectOnSeeEnemy(enemy);
        targeter.SeesEnemy(enemy, enemy.InfoGetCenterBottom());
    }

    public override void EffectDoesNotSeeEnemy(HumanoidModel enemy)
    {
        base.EffectDoesNotSeeEnemy(enemy);
        targeter.DoesNotSeeEnemy(enemy);
    }
}
