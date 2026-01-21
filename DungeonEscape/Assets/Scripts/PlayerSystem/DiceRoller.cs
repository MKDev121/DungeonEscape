using UnityEngine;
using UnityEngine.UI;

namespace PlayerSystem
{
    public class DiceRoller : MonoBehaviour
    {
        // Placeholder for the actual NapoleonsDice component reference
        // public NapoleonsDice diceInstance; 
        
        [SerializeField] private Button rollButton;

        private void Start()
        {
            if (rollButton != null)
            {
                rollButton.onClick.AddListener(RollDice);
            }
        }

        public void RollDice()
        {
            // Logic to trigger NapoleonsDice animation/calculation
            // For now, we simulate the result since we don't have the asset API verified
            SimulateRoll();
        }

        private void SimulateRoll()
        {
            int movement = UnityEngine.Random.Range(1, 7);
            int action = UnityEngine.Random.Range(1, 7);
            
            Debug.Log($"[DiceRoller] Rolled: Movement={movement}, Action={action}");
            PlayerEventSystem.TriggerDiceRolled(movement, action);
        }
    }
}