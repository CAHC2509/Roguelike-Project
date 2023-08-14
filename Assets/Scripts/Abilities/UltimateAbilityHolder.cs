using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAbilityHolder : MonoBehaviour
{
    [Header("Ability settings")]
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private Ability ultimateAbility;
    [SerializeField]
    private AbilityState abilityState = AbilityState.Ready;
    
    private float cooldownTime;
    private float activeTime;

    [HideInInspector]
    public bool activateUltimateAbility;

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
                if (activateUltimateAbility)
                {
                    ultimateAbility.Activate(player);
                    abilityState = AbilityState.Active;
                    activeTime = ultimateAbility.activeTime;
                }
                break;

            case AbilityState.Active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ultimateAbility.BeginCooldown(player);
                    abilityState = AbilityState.Cooldown;
                    cooldownTime = ultimateAbility.cooldownTime;
                }
                break;

            case AbilityState.Cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    activateUltimateAbility = false;
                    abilityState = AbilityState.Ready;
                }
                break;
        }
    }
}
