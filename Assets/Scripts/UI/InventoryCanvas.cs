using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryCanvas : MonoBehaviour
{

    private InputAction openInventory;
    [SerializeField] private PlayerControls playerControls;
    public static InventoryCanvas Instance { get; private set; }

    public GameObject itemPrefab; // Assign your item prefab here
    public Transform inventoryPanel; // Assign your Panel here
    private List<GameObject> items = new List<GameObject>(); // List to store items

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void OnEnable()
    {
        playerControls = new PlayerControls();

        openInventory = playerControls.Player.OpenInventory;
        openInventory.Enable();
        openInventory.performed += context => GameManager.Instance.OpenOrCloseInventory();
    }
    public void OnDisable()
    {
        openInventory.Disable();
    }

    public void AddItem(Sprite sprite)
    {
        GameObject item = Instantiate(itemPrefab, inventoryPanel);
        item.GetComponent<Image>().sprite = sprite;
        items.Add(item);
    }

}
