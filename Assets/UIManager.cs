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
    public UnityEngine.UI.Image[] UpgradeImages;
    public Sprite[] RenewableEnergySprites;
    public Sprite[] NonRenewableEnergySprites;
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
