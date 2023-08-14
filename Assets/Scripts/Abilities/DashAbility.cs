using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Dash")]
public class DashAbility : Ability
{
    public float dashVelocity;
    public AudioClip SFX;
    private float normalSpeed;

    public override void Activate(PlayerController player)
    {
        if (player.IsMoving()) // Only execute if the player is moving
        {
            normalSpeed = player.playerData.speed;
            player.aceleration = dashVelocity;
            player.SetAndPlaySFX(SFX);
        }
    }

    public override void BeginCooldown(PlayerController player)
    {
        player.aceleration = normalSpeed;
    }
}
