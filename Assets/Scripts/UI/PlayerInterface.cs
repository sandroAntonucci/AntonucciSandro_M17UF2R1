using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerInterface : MonoBehaviour
{
    public static PlayerInterface Instance { get; private set; }

    [SerializeField] private Image[] hearts;

    [SerializeField] private TextMeshProUGUI orbsText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        Player.OnPowerAdded += ChangeOrbsQuant;
        Player.OnHealthChanged += ChangeHearts;
        Player.OnCurrentHealthChanged += ChangeCurrentHealth;
    }

    public void ChangeOrbsQuant(int orbs)
    {
        orbsText.text = orbs.ToString();
    }

    private void ChangeHearts(int health)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void ChangeCurrentHealth(int currentHealth)
    {

        for (int i = hearts.Length - 1; i >= 0; i--)
        {
            if (i+1 > currentHealth && hearts[i].IsActive())
            {
                hearts[i].color = new Color(150f / 255f, 150f / 255f, 150f / 255f, 1);
            }
            else if (i + 1 <= currentHealth && hearts[i].IsActive())
            {
                hearts[i].color = new Color(1, 1, 1, 1);
            }
        }


    }


}
