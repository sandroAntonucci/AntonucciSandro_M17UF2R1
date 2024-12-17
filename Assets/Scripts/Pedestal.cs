using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{

    [SerializeField] private GameObject teleportEffect;

    bool isTeleporting = false; 

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (isTeleporting) return;

        teleportEffect.SetActive(true);
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        isTeleporting = true;

        yield return new WaitForSeconds(1f);

        Debug.Log(RoomController.instance.currentWorldName);

        switch (RoomController.instance.currentWorldName)
        {
            case "Catacombs":
                RoomController.instance.currentWorldName = "Basement";
                GameManager.Instance.LoadScene("BasementMain");
                break;

            case "Basement":
                RoomController.instance.currentWorldName = "Sanctuary";
                GameManager.Instance.LoadScene("SanctuaryMain");
                break;
        }

    }



}
