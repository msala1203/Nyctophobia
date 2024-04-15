using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaConroller : MonoBehaviour
{
    // Start is called before the first frame update

    public float playerStamia = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;
    public bool hasRegenerated = true;
    public bool isSprinting = false;

    private float stamiaDrain = 0.5f;
    private float staminaRegen = 0.5f;

    private Image staminaProgressUI = null;
    private CanvasGroup sliderCanvasGroup = null;




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
