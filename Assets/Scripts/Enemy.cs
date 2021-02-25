using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    private PlayerManager playerManager;
    private CharacterStats currentStats;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        currentStats = GetComponent<CharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();

        // Attack enemy
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();

        if(playerCombat != null)
            playerCombat.Attack(currentStats);
    }
}
