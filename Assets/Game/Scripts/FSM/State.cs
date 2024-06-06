using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{   
    public FSM myFSM {get; set;}
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void OnUpdate() { }
    public void FixedUpdate() { }

    protected GameManager gameManager;

    private void Start() {
        gameManager = GetComponent<GameManager>();
        if(!gameManager){
            Debug.LogError("State is not attached to GameManager!");
        }
    }
}
