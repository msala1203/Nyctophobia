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

    private float staminaDrain = 0.5f;
    private float staminaRegen = 0.25f;

    public Image staminaProgressUI;
    public CanvasGroup sliderCanvasGroup;

    private void Start()
    {
        staminaProgressUI = GetComponent<Image>();
        sliderCanvasGroup = GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        if (!isSprinting)
        {
            if (playerStamina <= maxStamina - 0.1)
            {
                playerStamina += staminaRegen * Time.deltaTime;
                updateStaminaValue();

                if (playerStamina >= maxStamina)
                {
                    sliderCanvasGroup.alpha = 0;
                    hasRegenerated = true;

                }
            }
        }
    }

    private void Sprinting()
    {
        if (hasRegenerated)        
        {
            isSprinting = true;
            playerStamina -= staminaDrain * Time.deltaTime;
            updateStaminaValue();

            if (playerStamina <= 0)
            {
                hasRegenerated = false;
                sliderCanvasGroup.alpha = 0;
            }
        }
    }
    private void updateStaminaValue()
    {
        staminaProgressUI.fillAmount = playerStamina / maxStamina;

    }
}
