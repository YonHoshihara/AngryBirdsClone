using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float healthMax;

    public Image healthBar;

    public void UpdateHealthBar(float health)
    {
        healthBar.fillAmount = health / healthMax;
    }

}
