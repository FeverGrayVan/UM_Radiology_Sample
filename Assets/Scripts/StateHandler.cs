using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "General/States/StateHandler")]
public class StateHandler : ScriptableObject
{
    public State currentState { get; private set; }
    [SerializeField] private State[] allStatesOrdered;

    

    private int currentStateIndex;

    private void OnEnable()
    {
        currentState = allStatesOrdered[0];
        currentStateIndex = 0;


    }

    public void SetState(State _state)
    {
        if (currentState == _state)
        {
            return;
        }
        else
        {
            currentState = _state;
        }
    }

    public void SwitchToNextState()
    {
        if (currentStateIndex < allStatesOrdered.Length)
        {
            currentStateIndex++;
            currentState = allStatesOrdered[currentStateIndex];
        }

        Debug.Log(currentState.ToString());
    }
}