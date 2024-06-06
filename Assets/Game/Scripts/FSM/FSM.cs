using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class FSM : MonoBehaviour
{
    protected Dictionary<System.Type , State> m_states = new Dictionary<System.Type, State>();
    protected State m_currentState;

    public FSM(){}

    public void Add(System.Type key, State state){
        state.myFSM = this;
        m_states.Add(key, state);
    }

    public State GetState(System.Type key){
        return m_states[key];
    }

    public void SetCurrentState(System.Type state){
        if(!m_states.ContainsKey(state)){return;}
        if(m_currentState != null){
            m_currentState.Exit();
        }

        m_currentState = m_states[state];

        if(m_currentState != null){
            m_currentState.Enter();
        }
    }

    public void Update(){
        if(m_currentState != null){
            m_currentState.OnUpdate();
        }
    }

    public void FixedUpdate(){
        if(m_currentState != null){
            m_currentState.FixedUpdate();
        }
    }  
}
