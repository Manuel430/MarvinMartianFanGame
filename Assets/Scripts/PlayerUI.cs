using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUI : MonoBehaviour
{
    [Header("HealthUI")]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] GameObject healthUI;
    Health playerHealth;

    [Header("GameOverUI")]
    [SerializeField] GameObject gameOverUI;

    [Header("PauseUI")]
    [SerializeField] GameObject pauseUI;
    PlayerControls playerControls;
    bool isPaused = false;
    bool cannotPause = false;

    [Header("NextLevelUI")]
    [SerializeField] GameObject nextLevelUI;

    #region Public
    public void UpdateHealth()
    {
        healthText.text = playerHealth.GetHealth().ToString();
    }

    public void GameOverUI()
    {
        gameOverUI.SetActive(true);
        healthUI.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CannotPause()
    {
        cannotPause = true;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void SetUnpause()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        healthUI.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetNextLevel()
    {
        nextLevelUI.SetActive(true);
    }

    #endregion
    private void Awake()
    {
        SetUnpause();

        playerHealth = GetComponent<Health>();
        if (playerHealth != null )
        {
            healthText.text = playerHealth.GetMaxHealth().ToString();
        }

        playerControls = new PlayerControls();
        playerControls.UI.Pause.Enable();

        playerControls.UI.Pause.performed += Pause;
    }

    private void Pause(InputAction.CallbackContext context)
    {
        if (cannotPause) { return; }

        isPaused = !isPaused;

        if (context.performed)
        {
            if (isPaused)
            {
                Time.timeScale = 0f;
                if (this.IsDestroyed()) { return; }
                pauseUI.SetActive(true);
                healthUI.SetActive(false);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f;
                if (this.IsDestroyed()) { return; }
                pauseUI.SetActive(false);
                healthUI.SetActive(true);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

}
