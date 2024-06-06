using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    FSM myFSM;

    private void Start() {
        myFSM = GetComponent<FSM>();
    }

    public void StartGame(){
        myFSM.SetCurrentState(typeof(GameState));
    }
}
