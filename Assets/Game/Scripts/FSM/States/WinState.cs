using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : State
{
    [SerializeField]
    private GameObject WinScreenUI; 

        public override void Enter()
    {
        WinScreenUI.SetActive(true);
    }

    public override void Exit()
    {
        gameManager.WipeCurrentLevel();
        WinScreenUI.SetActive(false);
    }
}
