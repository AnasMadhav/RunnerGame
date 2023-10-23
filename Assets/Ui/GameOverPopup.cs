using UnityEngine;
using UnityEngine.UI;

public class GameOverPopup : MonoBehaviour
{
    public GameObject popup;
    //public Button skipButton;
    //public Button attendQuizButton;

    private float displayTime = 15f; // 15 seconds
    private float timer = 0f;

    public Text TimeText;
    public Animator Character;
    private void Start()
    {
        // Disable the popup initially
        popup.SetActive(false);
    }

    private void Update()
    {
        // Check if the popup is active
        if (popup.activeSelf)
        {
            Character.gameObject.SetActive(true);
            int val = (int)displayTime;
            TimeText.text = val.ToString();

            Character.SetTrigger("Sad");
            // Update the timer
            timer += Time.deltaTime;

            // Check if the timer exceeds the display time
            if (timer >= displayTime)
            {
                // Hide the popup
                popup.SetActive(false);
                // Reset the timer
                timer = 0f;
            }
        }
    }

    public void ShowPopup()
    {
        // Show the popup and reset the timer
        popup.SetActive(true);
        timer = 0f;
    }
}
