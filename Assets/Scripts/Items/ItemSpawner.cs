using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSpawner : MonoBehaviour
{

    public List<GameObject> items;
    
    public TextMeshPro itemPriceTag;

    public ItemUpgrade itemUpgrade;

    public bool isFree;

    public void SpawnItem()
    {
        int randomItem = Random.Range(0, items.Count);
        itemUpgrade = Instantiate(items[randomItem], transform.position, Quaternion.identity).GetComponent<ItemUpgrade>();
    }

    void Start()
    {
        SpawnItem();
        if(!isFree) itemPriceTag.text = itemUpgrade.upgradePrice.ToString();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (isFree)
            {
                itemUpgrade.Upgrade();
                Destroy(gameObject);
            }
            else if (collision.gameObject.GetComponent<Player>().power >= itemUpgrade.upgradePrice)
            {
                collision.gameObject.GetComponent<Player>().power -= itemUpgrade.upgradePrice;
                itemUpgrade.Upgrade();
                Destroy(gameObject);
            }
        }
    }

}
