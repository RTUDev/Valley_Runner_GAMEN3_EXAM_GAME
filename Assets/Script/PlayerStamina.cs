using UnityEngine;
using UnityEngine.UI;
using StarterAssets; // Namespace for the downloaded asset

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float drainRate = 20f;
    public float regenRate = 10f;

    [Header("UI Reference")]
    public Slider staminaSlider; // Drag your UI Slider here

    private float currentStamina;
    private ThirdPersonController controller; // Reference to the asset's script
    private StarterAssetsInputs inputs; // Reference to input script

    void Start()
    {
        currentStamina = maxStamina;
        controller = GetComponent<ThirdPersonController>();
        inputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        HandleStamina();
        UpdateUI();
    }

    void HandleStamina()
    {
        // Check if player is trying to sprint and moving
        if (inputs.sprint && inputs.move != Vector2.zero)
        {
            if (currentStamina > 0)
            {
                currentStamina -= drainRate * Time.deltaTime;
            }
            else
            {
                // Force stop sprinting if stamina hits 0
                inputs.sprint = false;
                controller.MoveSpeed = 2.0f; // Force walk speed (check specific value in inspector)
            }
        }
        else
        {
            // Regenerate if not sprinting
            if (currentStamina < maxStamina)
            {
                currentStamina += regenRate * Time.deltaTime;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
    }

    void UpdateUI()
    {
        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina / maxStamina;
        }
    }
}