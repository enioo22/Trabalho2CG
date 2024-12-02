using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    public GameObject PanelUpgrades;
    public GameObject PanelUpgrades2;
    public Sprite CleanEnergySprite;
    public Sprite DirtyEnergySprite;
    public UnityEngine.UI.Button UpgradesShow;
    public UnityEngine.UI.Button UpgradeTypeChange;
    public TextMeshProUGUI UpgradeTypeText;
    public manager manager;
    public UnityEngine.UI.Image[] UpgradeImages;
    public Sprite[] RenewableEnergySprites;
    public Sprite[] NonRenewableEnergySprites;

    public Sprite[] EolicoSprites;
    public Sprite[] HidroSprites;
    public Sprite[] SolarSprites;

    public UnityEngine.UI.Image[] EolicoImages;
    public UnityEngine.UI.Image[] HidroImages;
    public UnityEngine.UI.Image[] SolarImages;


    public GameObject Upgrade0;
    public GameObject Upgrade1;

    public UnityEngine.UI.Button Upgrades2Show;
    public UnityEngine.UI.Button Upgrades2Close;


    public void onClickUpgrades()
    {
        PanelUpgrades.SetActive(true);
    }

    public void onClickUpgrades2()
    {
        PanelUpgrades2.SetActive(true);
    }

    public void onClickCloseUpgrades()
    {
        PanelUpgrades.SetActive(false);
    }

    public void onClickCloseUpgrades2()
    {
        PanelUpgrades2.SetActive(false);
    }

    public void onClickChangeVisual(int position)
    {
        switch (position)
        {
            case 0:
                for(int i = 0; i <9; i+=2)
                {
                    EolicoImages[i].sprite = EolicoSprites[2];
                    EolicoImages[i+1].sprite = EolicoSprites[3];
                }
                break;

            case 1:
                for (int i = 0; i < 9; i += 2)
                {
                    EolicoImages[i].sprite = EolicoSprites[4];
                    EolicoImages[i + 1].sprite = EolicoSprites[5];
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    HidroImages[i].sprite = HidroSprites[0];
                }
                break;
        }   
        
    }

    public void onClickChangeEnergyType()
    {
        if (manager.EnergyType == 0)
        {
            manager.EnergyType = 1;
            UpgradeTypeText.text = "Não Renováveis";
            UpgradeTypeChange.image.sprite = CleanEnergySprite; // Troca para energia não renovável
            UpdateEnergyIcons();
        }
        else
        {
            manager.EnergyType = 0;
            UpgradeTypeText.text = "Renováveis";
            UpgradeTypeChange.image.sprite = DirtyEnergySprite; // Troca para energia renovável
            UpdateEnergyIcons();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Configurar energia renovável como padrão
        manager.EnergyType = 0;
        UpgradeTypeChange.image.sprite = DirtyEnergySprite;
        UpdateEnergyIcons();
    }
    public void UpdateEnergyIcons()
    {
        if (manager.EnergyType == 0)
        {
            for (int i = 0; i < UpgradeImages.Length; i++)
            {
                UpgradeImages[i].sprite = RenewableEnergySprites[i];
            }
        }
        else
        {
            for (int i = 0; i < UpgradeImages.Length; i++)
            {
                UpgradeImages[i].sprite = NonRenewableEnergySprites[i];
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
