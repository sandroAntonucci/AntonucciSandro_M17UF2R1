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

        GameManager.Instance.LoadScene("BasementLevel2");
    }



}
