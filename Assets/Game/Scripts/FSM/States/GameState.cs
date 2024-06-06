using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : State
{
    public GameObject GamePlayerUI;

    public override void Enter()
    {
        GamePlayerUI.SetActive(true);
        gameManager.SpawnSpireStructures();
        gameManager.SpawnHelixBall();
    }

    public override void Exit()
    {
        GamePlayerUI.SetActive(false);
    }
}
