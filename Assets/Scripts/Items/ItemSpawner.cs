using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSpawner : MonoBehaviour
{

    public List<GameObject> normalItems;
    public List<GameObject> rareItems;
    public List<GameObject> legendaryItems;

    public TextMeshPro itemPriceTag;

    public Transform parentTransform;

    public ItemUpgrade itemUpgrade;

    public bool isFree;

    public List<GameObject> ChooseRarity()
    {

        int randomRarity = Random.Range(0, 100);

        // 60% normal, 30% rare, 10% legendary
        if (randomRarity < 60)
        {
            return normalItems;
        }
        else if (randomRarity < 90)
        {
            return rareItems;
        }
        else
        {
            return legendaryItems;
        }
    }

    public void SpawnItem(List<GameObject> items)
    {
        int randomItem = Random.Range(0, items.Count);

        itemUpgrade = Instantiate(items[randomItem], parentTransform).GetComponent<ItemUpgrade>();
        itemUpgrade.transform.localPosition = Vector3.zero; // Reset position relative to parent
    }

    void Start()
    {
        SpawnItem(ChooseRarity());
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
                collision.gameObject.GetComponent<Player>().EmitPowerAdded();
                itemUpgrade.Upgrade();
                Destroy(gameObject);
            }
        }
    }

}
