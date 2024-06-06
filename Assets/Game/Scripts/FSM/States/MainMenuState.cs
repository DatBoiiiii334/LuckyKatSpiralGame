using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : State
{
    public GameObject Mainmenu;
    public override void Enter()
    {
        // display main menu
        Mainmenu.SetActive(true);
    }

    public override void OnUpdate()
    {
        // if(Input.GetKeyDown(KeyCode.Alpha1)){
        //     myFSM.SetCurrentState(typeof(DefaultState));
        // }
    }

    public override void Exit()
    {
        // set main menu inactive
        Mainmenu.SetActive(false);
    }
}
