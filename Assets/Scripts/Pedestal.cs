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

            case "Sanctuary":
                RoomController.instance.currentWorldName = "FinalBoss";
                GameManager.Instance.LoadScene("FinalBossMain");
                break;

            case "FinalBoss":
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                GameObject finalMenu = GameObject.FindGameObjectWithTag("FinalMenuCanvas");
                finalMenu.GetComponent<Canvas>().enabled = true;

                // Disables the pause menu
                GameObject pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
                if (pauseCanvas != null) pauseCanvas.GetComponent<PauseMenu>().enabled = false;

                break;
        }

    }



}
