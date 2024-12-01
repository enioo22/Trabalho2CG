using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class manager : MonoBehaviour
{

    public DialogManager dialogmanager;
    public TextMeshProUGUI UpgradeTitleRenovaveis;

    public TextMeshProUGUI TotalClicksText;
    public TextMeshProUGUI PowerGenerationPerSecondText;
    public TextMeshProUGUI Upgrade1ButtonText;
    public TextMeshProUGUI PowerPerClick;
    public TextMeshProUGUI CO2EmissionText;
    public TextMeshProUGUI[] UpgradeButtonText;

    public GameObject totalenergytext;
    public GameObject powerperclick;
    public GameObject powerpersecondtext;
    public GameObject co2emissiontext;
    public GameObject upgradesmenu1;
    public GameObject upgradesmenu2;
    public UnityEngine.UI.Button[] upgradeButton;
    public GameObject[] upgrades;

    public float target;


    float PowerValue;
    float PowerGenerationPerClick;
    float PowerGenerationPerSecond;
    double TotalClicks;
    double TotalCO2Emission;

    float clickrotation;

    const int NumberOfUpgrades = 6;
    public int EnergyType = 0; // 0 = Renov�veis, 1 = N�o Renovaveis;


    string[] UpgradeRenovaveis = { "Eólica", "Hidrelétrica", "Biomassa", "Solar" };
    double[] UpgradeCostRenovaveis = { 10, 1000, 100000, 200000 };
    double[] UpgradeLevelRenovaveis = { 0, 0, 0, 0 };
    double[] CO2EmissionPerUpgradeRenovaveis = { 13, 21, 0, 43 };  // Emissão em KCO₂/MW/h

    string[] UpgradeNaoRenovaveis = { "Gás Natural", "Óleo", "Carvão", "Nuclear" };
    double[] UpgradeCostNaoRenovaveis = { 500, 2000, 5000, 1000000 };
    double[] UpgradeLevelNaoRenovaveis = { 0, 0, 0, 0 };
    double[] CO2EmissionPerUpgradeNaoRenovaveis = { 486, 840, 1000, 13 }; // Emissão em KCO₂/MW/h
    public double getUpgradeCost(double InitialCost, int NumberOfUpgrades)
    {
        return InitialCost * Math.Pow(1.2, NumberOfUpgrades);
    }

    

    public void mainClick()
    {
        this.TotalClicks += this.PowerGenerationPerClick;
        this.PowerValue += this.PowerGenerationPerClick;
        this.TotalClicksText.text = Math.Round(this.TotalClicks, 2).ToString();
        if(TotalClicks >= 1)
        {
            totalenergytext.SetActive(true);
            powerperclick.SetActive(true);
        }
        if(TotalClicks == 10)
        {
            dialogmanager.callDialog(0);
            upgradesmenu1.SetActive(true);     
        }
        if(TotalClicks >UpgradeCostRenovaveis[0])
        {
            upgrades[0].SetActive(true);
            
        }
        if(TotalClicks > UpgradeCostRenovaveis[1])
        {
            upgrades[1].SetActive(true);
        }
        if(TotalClicks > UpgradeCostRenovaveis[2])
        {
            upgrades[2].SetActive(true);
        }
        if (TotalClicks > UpgradeCostRenovaveis[3])
        {
            upgrades[3].SetActive(true);
        }
        
    }

 // Upgrade para Renováveis
    public void MakeUpgradeRenovavel(int position)
    {
        powerpersecondtext.SetActive(true);

        if(EnergyType == 0) {
            if (this.UpgradeCostRenovaveis[position] > this.TotalClicks)
            {
                UpgradeButtonText[position].color = Color.red;
                return;
            }

            ++this.UpgradeLevelRenovaveis[position];
            double upgradelevel = this.UpgradeLevelRenovaveis[position];

            if (upgradelevel >= 1 && upgradelevel <= 3)
            {

                GameObject.Find("/CanvasMain/Backgroundentities/Eolicos/Eolico" + upgradelevel).SetActive(true);
                GameObject.Find("/CanvasMain/Backgroundentities/Eolicos/Eolico" + upgradelevel + "Pole").SetActive(true);

            }
            this.TotalClicks -= this.UpgradeCostRenovaveis[position];
            if (UpgradeLevelRenovaveis[0] >= 25)
            {
                upgradesmenu2.SetActive(true);
            }

            this.UpgradeCostRenovaveis[position] *= 1.2;
            this.PowerGenerationPerClick++;
            this.TotalCO2Emission += CO2EmissionPerUpgradeRenovaveis[position];
            return;
        }
        if (this.UpgradeCostRenovaveis[position] > this.TotalClicks)
        {
            Upgrade1ButtonText.color = Color.red; 
            return;
        }
        this.TotalClicks -= this.UpgradeCostRenovaveis[position];
        this.UpgradeLevelRenovaveis[position]++;
        this.UpgradeCostRenovaveis[position] *= 1.2;
        this.PowerGenerationPerClick++;
        this.TotalCO2Emission += CO2EmissionPerUpgradeRenovaveis[position];
    }
    // Upgrade para Não Renováveis
    public void MakeUpgradeNaoRenovavel(int position)
    {
        if (this.UpgradeCostNaoRenovaveis[position] > this.TotalClicks)
        {
            return;
        }
        this.TotalClicks -= this.UpgradeCostNaoRenovaveis[position];
        this.UpgradeLevelNaoRenovaveis[position]++;
        this.UpgradeCostNaoRenovaveis[position] *= 1.3;
        this.PowerGenerationPerClick += 2;
        this.TotalCO2Emission += CO2EmissionPerUpgradeNaoRenovaveis[position];


    }
    IEnumerator Waiter(float duration)
    {
        Debug.Log($"Started at {Time.time}, waiting for {duration} seconds");
        yield return new WaitForSeconds(duration);
        Debug.Log($"Ended at {Time.time}");
    }
    // Start is called before the first frame update
    void Start() 
    {
        this.TotalClicks = 0;
        this.PowerGenerationPerSecond = this.PowerValue = 0;
        this.PowerGenerationPerClick = 1;
        this.TotalCO2Emission = 0;
    }
    
    private void Update()
    {
        this.TotalClicksText.text = Math.Round(this.TotalClicks, 2).ToString();
        this.PowerPerClick.text = this.PowerGenerationPerClick.ToString() + "MW/Click";
        this.CO2EmissionText.text = $"CO2 emitido: {Math.Round(this.TotalCO2Emission, 2)} KG de CO2";
        switch (EnergyType)
        {
            case 0:
                for (int i = 0; i < UpgradeRenovaveis.Length; i++)
                {
                    UpgradeButtonText[i].text = UpgradeRenovaveis[i] + "\nNível: " + UpgradeLevelRenovaveis[i] + "\nCusto: " + Math.Round(UpgradeCostRenovaveis[i], 2);
                    if(TotalClicks < UpgradeCostRenovaveis[i])
                    {
                        upgradeButton[i].interactable = false;
                    }
                    UpgradeButtonText[i].color = Color.black;
                    upgradeButton[i].interactable = true;

                }
                    break;
            case 1:
                for (int i = 0; i < UpgradeNaoRenovaveis.Length; i++)
                {
                    UpgradeButtonText[i].text = UpgradeNaoRenovaveis[i] + "\nNível: " + UpgradeLevelNaoRenovaveis[i] + "\nCusto: " + Math.Round(UpgradeCostNaoRenovaveis[i], 2);

                    if (TotalClicks < UpgradeCostNaoRenovaveis[i])
                    {
                        upgradeButton[i].interactable = false;
                    }
                    UpgradeButtonText[i].color = Color.black;
                    upgradeButton[i].interactable = true;
                }
                break;

        }
    }
}
