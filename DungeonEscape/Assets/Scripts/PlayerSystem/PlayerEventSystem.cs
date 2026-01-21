using System;
using UnityEngine;

namespace PlayerSystem
{
    public enum ActionType
    {
        Slash,
        FireSpell,
        Heal
    }

    public static class PlayerEventSystem
    {
        public static event Action<int, int> OnDiceRolled;
        public static event Action<ActionType> OnActionChosen;
        public static event Action<int, Vector2Int> OnMoveRequested;
        public static event Action<ActionType, Vector2Int> OnActionPerformed;
        public static event Action OnTurnEnded;

        public static void TriggerDiceRolled(int movementDice, int actionDice) => OnDiceRolled?.Invoke(movementDice, actionDice);
        public static void TriggerActionChosen(ActionType action) => OnActionChosen?.Invoke(action);
        public static void TriggerMoveRequested(int steps, Vector2Int targetTile) => OnMoveRequested?.Invoke(steps, targetTile);
        public static void TriggerActionPerformed(ActionType action, Vector2Int targetTile) => OnActionPerformed?.Invoke(action, targetTile);
        public static void TriggerTurnEnded() => OnTurnEnded?.Invoke();
    }
}