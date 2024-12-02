using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerInterface : MonoBehaviour
{

    [SerializeField] private Image[] hearts;

    [SerializeField] private TextMeshProUGUI orbsText;

    private void OnEnable()
    {
        Player.OnPowerAdded += ChangeOrbsQuant; // Subscribe to the event
        Player.OnHealthChanged += ChangeHearts; // Subscribe to the event
    }

    public void ChangeOrbsQuant(int orbs)
    {
        orbsText.text = orbs.ToString();
    }

    private void ChangeHearts(int health)
    {

        Debug.Log("Changing hearts to " + health);

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


}
