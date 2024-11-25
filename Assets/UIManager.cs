using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject PanelUpgrades;
    public Sprite CleanEnergySprite;
    public Sprite DirtyEnergySprite;
    public Button UpgradesShow;
    public Button UpgradeTypeChange;
    public manager manager;


    public void onClickUpgrades()
    {
        PanelUpgrades.SetActive(true);
    }

    public void onClickCloseUpgrades()
    {
        PanelUpgrades.SetActive(false);
    }

    public void onClickChangeEnergyType()
    {
        if (manager.EnergyType == 0) { manager.EnergyType = 1; }
        else { manager.EnergyType = 0; }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (manager.EnergyType)
        {
            case 0:
                UpgradeTypeChange.image.sprite = DirtyEnergySprite;
                break;
            case 1:
                UpgradeTypeChange.image.sprite = CleanEnergySprite;
                break;
            default:
                UpgradeTypeChange.image.sprite = DirtyEnergySprite;
                break;
        }
    }
}
