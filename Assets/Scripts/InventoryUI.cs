using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public TMP_Text collectText;

    public int current = 0;
    public int maxCollectables = 8; // nastavi v Inspectore

    void Awake()
    {
        Debug.Log("InventoryUI Awake()");
        instance = this;
        UpdateUI();
    }

    public void AddItem(string item)
    {
        if (item == "Metal")
        {
            current++;

            if (current > maxCollectables)
                current = maxCollectables;

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        Debug.Log("UpdateUI called");
        collectText.text = current + "/" + maxCollectables;
    }
}