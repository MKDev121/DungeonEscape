using System;
using UnityEngine;

namespace PlayerSystem
{
    public enum PlayerState
    {
        Idle,
        DiceRoll,
        Choosing,
        Performing
    }

    public class PlayerStateMachine
    {
        public PlayerState CurrentState { get; private set; }
        public event Action<PlayerState> OnStateChanged;

        public PlayerStateMachine()
        {
            CurrentState = PlayerState.Idle;
        }

        public void ChangeState(PlayerState newState)
        {
            if (CurrentState == newState) return;
            
            Debug.Log($"[PlayerStateMachine] Transition: {CurrentState} -> {newState}");
            CurrentState = newState;
            OnStateChanged?.Invoke(newState);
        }
    }
}