using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerInterface : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI orbsText;

    private void OnEnable()
    {
        Player.OnPowerAdded += ChangeOrbsQuant; // Subscribe to the event
    }

    private void ChangeOrbsQuant(int orbs)
    {
        orbsText.text = (int.Parse(orbsText.text) + orbs).ToString();
    }


}
