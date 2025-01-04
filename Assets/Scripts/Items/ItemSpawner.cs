using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawner : MonoBehaviour
{
    // ScriptableObject-based item lists
    public List<ItemUpgrade> normalItems;
    public List<ItemUpgrade> rareItems;
    public List<ItemUpgrade> legendaryItems;

    // UI and item assignment
    public TextMeshPro itemPriceTag;
    public SpriteRenderer itemIcon;

    public ItemUpgrade currentItemUpgrade;

    public bool isFree;

    private void Start()
    {
        SpawnItem(ChooseRarity());
    }

    private void SpawnItem(List<ItemUpgrade> items)
    {
        int randomItem = Random.Range(0, items.Count);
        currentItemUpgrade = items[randomItem];

        // Update the UI to display the item's data
        itemPriceTag.text = isFree ? "" : currentItemUpgrade.upgradePrice.ToString();
        itemIcon.sprite = currentItemUpgrade.upgradeIcon;
        itemIcon.transform.position = gameObject.transform.position;
    }

    private List<ItemUpgrade> ChooseRarity()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (isFree || (player != null && player.power >= currentItemUpgrade.upgradePrice))
            {
                if (!isFree)
                {
                    player.power -= currentItemUpgrade.upgradePrice;
                    player.EmitPowerAdded();
                }

                // Apply the upgrade and "consume" the item
                currentItemUpgrade.ApplyUpgrade();
                Destroy(gameObject);
            }
        }
    }
}
