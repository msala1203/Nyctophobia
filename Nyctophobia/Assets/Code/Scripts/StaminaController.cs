using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    // Start is called before the first frame update

    public float playerStamina = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;
    public bool hasRegenerated = true;
    public bool isSprinting = false;
    public bool canSprint = true;

    private float staminaDrain = 20f;
    private float staminaRegen = 10f;

    public Image staminaProgressUI;
    public CanvasGroup sliderCanvasGroup;

    

    private void Start()
    {
        //staminaProgressUI = GetComponent<Image>();
        //sliderCanvasGroup = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        print("CurrentStamina: " + playerStamina);
        updateStaminaValue();
        if (!isSprinting)
        {
            if (playerStamina <= maxStamina)
            {
                playerStamina += staminaRegen * Time.deltaTime;
                updateStaminaValue();

                if (playerStamina >= maxStamina)
                {
                    sliderCanvasGroup.alpha = 0;
                    hasRegenerated = true;
                    canSprint = true;

                }
            }
        }
        else
        {
            Sprinting();
        }
    }

    private void Sprinting()
    {
        if (hasRegenerated)
        {
            sliderCanvasGroup.alpha = 1;
            isSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            updateStaminaValue();

            

            if (playerStamina <= 0)
            {
                hasRegenerated = false;
                canSprint = false;
            }
        }
    }
    private void updateStaminaValue()
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;

        float thresholdPercentage = 0.8f;

        if (hasRegenerated)
        {
            if (staminaProgressUI.fillAmount <= thresholdPercentage)
            {
                // Calculate the adjusted stamina percentage within the range [0, 1]
                float value = Mathf.InverseLerp(0.0f, thresholdPercentage, staminaProgressUI.fillAmount);

                // Gradually transition color from red to white based on adjusted stamina percentage
                Color lerpedColor = Color.Lerp(Color.red, Color.white, value);
                staminaProgressUI.color = lerpedColor;
            }
            else
            {
                // Set the color to white if stamina fill amount is above 50%
                staminaProgressUI.color = Color.white;
            }
        }
        else
        {
            staminaProgressUI.color = Color.red;
        }
    }
}
