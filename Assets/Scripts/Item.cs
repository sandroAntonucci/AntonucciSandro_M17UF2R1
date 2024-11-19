using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    
    public TextMeshPro itemPriceTag;

    public ItemUpgrade itemUpgrade;

    public int powerPrice;

    void Start()
    {
        itemPriceTag.text = powerPrice.ToString();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().power >= powerPrice)
            {
                collision.gameObject.GetComponent<Player>().power -= powerPrice;
                itemUpgrade.Upgrade();
                Destroy(gameObject);
            }
        }
    }

}
