using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : State
{
    [SerializeField]
    private GameObject LoseScreenUI;

        public override void Enter()
    {
        LoseScreenUI.SetActive(true);
    }

    public override void Exit()
    {
        gameManager.WipeCurrentLevel();
        LoseScreenUI.SetActive(false);
    }
}
