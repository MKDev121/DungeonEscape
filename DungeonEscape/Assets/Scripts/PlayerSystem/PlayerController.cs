using UnityEngine;

namespace PlayerSystem
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerStateMachine _stateMachine;
        private int _availableMovement;
        private int _availableActionPower;

        [Header("Status")]
        [SerializeField] private float maxHealth = 100f;
        private float _currentHealth;

        private void Awake()
        {
            _stateMachine = new PlayerStateMachine();
            _currentHealth = maxHealth;
        }

        private void OnEnable()
        {
            _stateMachine.OnStateChanged += HandleStateChanged;
            PlayerEventSystem.OnDiceRolled += HandleDiceRolled;
            PlayerEventSystem.OnActionChosen += HandleActionChosen;
            PlayerEventSystem.OnMoveRequested += HandleMoveRequested;
            PlayerEventSystem.OnActionPerformed += HandleActionPerformed;
        }

        private void OnDisable()
        {
            _stateMachine.OnStateChanged -= HandleStateChanged;
            PlayerEventSystem.OnDiceRolled -= HandleDiceRolled;
            PlayerEventSystem.OnActionChosen -= HandleActionChosen;
            PlayerEventSystem.OnMoveRequested -= HandleMoveRequested;
            PlayerEventSystem.OnActionPerformed -= HandleActionPerformed;
        }

        private void Update()
        {
            // Test inputs
            if (_stateMachine.CurrentState == PlayerState.Idle)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _stateMachine.ChangeState(PlayerState.DiceRoll);
                }
            }
        }

        private void HandleStateChanged(PlayerState newState)
        {
            Debug.Log($"Player State: {newState}");
        }

        private void HandleDiceRolled(int movement, int action)
        {
            _availableMovement = movement;
            _availableActionPower = action;
            _stateMachine.ChangeState(PlayerState.Choosing);
        }

        private void HandleActionChosen(ActionType actionType)
        {
            if (_stateMachine.CurrentState != PlayerState.Choosing) return;
            
            // Logic to validate if action is possible
            Debug.Log($"Action Chosen: {actionType}");
            
            // For now, immediately move to performing
            // Real implementation would wait for target selection
            // _stateMachine.ChangeState(PlayerState.Performing); 
        }

        private void HandleMoveRequested(int steps, Vector2Int target)
        {
             if (_availableMovement < steps) 
             {
                 Debug.LogWarning("Not enough movement!");
                 return;
             }
             
             // Execute Move
             Debug.Log($"@mcp:unityMCP:MoveTo {target.x},{target.y}"); // Tag for mcp execution
             _availableMovement -= steps;
        }

        private void HandleActionPerformed(ActionType action, Vector2Int target)
        {
            string mcpCommand = "";
            switch (action)
            {
                case ActionType.Slash: mcpCommand = "PerformSlash"; break;
                case ActionType.FireSpell: mcpCommand = "PerformFireSpell"; break;
                case ActionType.Heal: mcpCommand = "PerformHeal"; break;
            }
            
            Debug.Log($"@mcp:unityMCP:{mcpCommand} on {target}");
            
            _stateMachine.ChangeState(PlayerState.Idle); // Turn done
            PlayerEventSystem.TriggerTurnEnded();
        }
        
        // Public method for UI to trigger action selection
        public void SelectAction(ActionType action)
        {
             if (_stateMachine.CurrentState == PlayerState.Choosing)
             {
                 PlayerEventSystem.TriggerActionChosen(action);
             }
        }
    }
}