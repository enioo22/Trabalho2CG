using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    public GameObject PanelUpgrades;
    public Sprite CleanEnergySprite;
    public Sprite DirtyEnergySprite;
    public UnityEngine.UI.Button UpgradesShow;
    public UnityEngine.UI.Button UpgradeTypeChange;
    public TextMeshProUGUI UpgradeTypeText;
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
        if (manager.EnergyType == 0) 
        { 
            manager.EnergyType = 1;
            UpgradeTypeText.text = "Não Renováveis";
        }
        else { manager.EnergyType = 0; UpgradeTypeText.text = "Renováveis"; }
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
