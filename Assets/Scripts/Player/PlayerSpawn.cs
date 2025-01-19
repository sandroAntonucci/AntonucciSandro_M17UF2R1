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

        yield return new WaitForSeconds(3f);

        LoadSceneCanvas.Instance.HideLoadingScreen();

        EnablePlayer();
    }

    public void DisablePlayer()
    {
        player.spell.gameObject.GetComponent<SpellOrbit>().enabled = false;
        player.rb.velocity = Vector2.zero;
        player.enabled = false;
        playerCollider.enabled = false;
        triggerCollider.enabled = false;
    }

    public void EnablePlayer()
    {

        player.spell.gameObject.GetComponent<SpellOrbit>().enabled = true;
        triggerCollider.enabled = true;
        player.enabled = true;
        playerCollider.enabled = true;
    }

}
