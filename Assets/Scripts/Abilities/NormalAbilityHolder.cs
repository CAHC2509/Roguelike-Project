using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAbilityHolder : MonoBehaviour
{
    [Header("Ability settings")]
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private Ability normalAbility;
    [SerializeField]
    private AbilityState abilityState = AbilityState.Ready;

    private float cooldownTime;
    private float activeTime;

    [HideInInspector]
    public bool activateNormalAbility;

    private enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    void Update()
    {
        switch (abilityState)
        {
            case AbilityState.Ready:
                if (activateNormalAbility)
                {
                    normalAbility.Activate(player);
                    abilityState = AbilityState.Active;
                    activeTime = normalAbility.activeTime;
                }
                    break;

            case AbilityState.Active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    normalAbility.BeginCooldown(player);
                    abilityState = AbilityState.Cooldown;
                    cooldownTime = normalAbility.cooldownTime;
                }
                break;

            case AbilityState.Cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    activateNormalAbility = false;
                    abilityState = AbilityState.Ready;
                }
                break;
        }
    }
}
