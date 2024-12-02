using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private BoxCollider2D triggerCollider;
    [SerializeField] private BoxCollider2D playerCollider;


    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {

        yield return new WaitForSeconds(2.5f);

        triggerCollider.enabled = true;
        player.enabled = true;
        playerCollider.enabled = true;

    }

}
