using UnityEngine;
using TMPro;

namespace PlayerSystem
{
    public class PlayerUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject dicePanel;
        [SerializeField] private GameObject actionPanel;
        [SerializeField] private TextMeshProUGUI movementDiceText; // Display roll result
        [SerializeField] private TextMeshProUGUI actionDiceText;

        private void OnEnable()
        {
            PlayerEventSystem.OnDiceRolled += HandleDiceRolled;
            PlayerEventSystem.OnActionChosen += HandleActionChosen;
            PlayerEventSystem.OnTurnEnded += HandleTurnEnded;
        }

        private void OnDisable()
        {
            PlayerEventSystem.OnDiceRolled -= HandleDiceRolled;
            PlayerEventSystem.OnActionChosen -= HandleActionChosen;
            PlayerEventSystem.OnTurnEnded -= HandleTurnEnded;
        }

        private void HandleDiceRolled(int movement, int action)
        {
            if (dicePanel) dicePanel.SetActive(false); // Hide roll button/panel
            if (actionPanel) actionPanel.SetActive(true); // Show action selection
            
            if (movementDiceText) movementDiceText.text = movement.ToString();
            if (actionDiceText) actionDiceText.text = action.ToString();
        }

        private void HandleActionChosen(ActionType action)
        {
            // Update UI/Feedback
        }

        private void HandleTurnEnded()
        {
            if (dicePanel) dicePanel.SetActive(true);
            if (actionPanel) actionPanel.SetActive(false);
        }
    }
}