using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{

    [SerializeField] private GameObject teleportEffect;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        teleportEffect.SetActive(true);
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);

        switch (RoomController.instance.currentWorldName)
        {
            case "Catacombs":
                RoomController.instance.currentWorldName = "Basement";
                GameManager.Instance.LoadScene("BasementMain");
                break;
        }

    }



}
