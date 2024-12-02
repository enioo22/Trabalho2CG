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

    public UnityEngine.UI.Button UpgradeVisual0, UpgradeVisual1, UpgradeVisual2, UpgradeVisual3;

    public GameObject totalenergytext;
    public GameObject powerperclick;
    public GameObject powerpersecondtext;
    public GameObject co2emissiontext;
    public GameObject upgradesmenu1;
    public GameObject upgradesmenu2;
    public UnityEngine.UI.Button[] upgradeButton;
    public GameObject[] upgrades;
    public GameObject MainClick;

    public float target;


    double PowerValue = 0;
    double PowerGenerationPerClick;
    double PowerPerClickEolica = 0, PowerPerClickHidro = 0, PowerPerClickBio = 0, PowerPerClickSolar = 0, PowerPerClickGas = 0, PowerPerClickOleo = 0, PowerPerClickCarvao = 0, PowerPerClickNuke = 0;
    float PowerGenerationPerSecond;
    double TotalClicks;
    double TotalCO2Emission;

    float clickrotation;

    const int NumberOfUpgrades = 6;
    public int EnergyType = 0; // 0 = Renov�veis, 1 = N�o Renovaveis;
    private int purchaseMultiplier = 1;

    string[] UpgradeRenovaveis = { "Eólica", "Hidrelétrica", "Biomassa", "Solar" };
    double[] UpgradeCostRenovaveis = { 10, 1000, 100000, 200000 };
    double[] UpgradeLevelRenovaveis = { 0, 0, 0, 0 };
    double[] CO2EmissionPerUpgradeRenovaveis = { 13, 21, 0, 43 };  // Emissão em KCO₂/MW/h

    string[] UpgradeNaoRenovaveis = { "Gás Natural", "Óleo", "Carvão", "Nuclear" };
    double[] UpgradeCostNaoRenovaveis = { 500, 2000, 5000, 1000000 };
    double[] UpgradeLevelNaoRenovaveis = { 0, 0, 0, 0 };
    double[] CO2EmissionPerUpgradeNaoRenovaveis = { 486, 840, 1000, 13 }; // Emissão em KCO₂/MW/h

    double upgradeVisualEolico1 = 0, upgradeVisualEolico2 = 0, upgradeVisualHidro1 = 0, upgradeVisualEolico3 = 0;


    public double getUpgradeCost(double InitialCost, int NumberOfUpgrades)
    {
        return InitialCost * Math.Pow(1.2, NumberOfUpgrades);
    }



    public void mainClick()
    {
        this.TotalClicks += this.PowerGenerationPerClick;
        this.TotalClicks += 10000000000;
        this.PowerValue += this.PowerGenerationPerClick;
        this.TotalClicksText.text = Math.Round(this.TotalClicks, 2).ToString();
        if (TotalClicks >= 1)
        {
            totalenergytext.SetActive(true);
            powerperclick.SetActive(true);
        }
        if (TotalClicks >= 10)
        {
            //dialogmanager.callDialog(0);
            upgradesmenu1.SetActive(true);
        }
        for (int i = 0; i < 4; i++)
        {
            if( TotalClicks> UpgradeCostRenovaveis[i])
            {
                upgrades[i].SetActive(true);
            }
        }

    }

    public void onClickStartButton()
    {
        GameObject.Find("/CanvasMain/TitleName").SetActive(false);
        MainClick.SetActive(true);
    }

    // Upgrade para Renováveis
    public void MakeUpgradeRenovavel(int position)
    {
        double totalCost = 0;

        for (int i = 0; i < purchaseMultiplier; i++)
        {
            totalCost += UpgradeCostRenovaveis[position] * Math.Pow(1.1, UpgradeLevelRenovaveis[position] + i);
        }

        if (totalCost > TotalClicks)
        {
            UpgradeButtonText[position].color = Color.red;
            return;
        }
        for (int i = 0; i < purchaseMultiplier; i++)
        {
            ++UpgradeLevelRenovaveis[position];
            TotalCO2Emission += CO2EmissionPerUpgradeRenovaveis[position];
            UpgradeCostRenovaveis[position] *= 1.1;

            PowerPerClickEolica = UpgradeLevelRenovaveis[0] * 0.5 * (1 + upgradeVisualEolico1 + upgradeVisualEolico2);
            PowerPerClickHidro = UpgradeLevelRenovaveis[1] * 5;
            PowerPerClickBio = UpgradeLevelRenovaveis[2] * 25;
            PowerPerClickSolar = UpgradeLevelRenovaveis[3] * 100;
        }

        TotalClicks -= totalCost;

        if (UpgradeLevelRenovaveis[0] >= 25)
        {
            upgradesmenu2.SetActive(true);
        }
            
        double upgradelevel = UpgradeLevelRenovaveis[position];

        switch (position) 
        {
            case 0:
                if (upgradelevel < 6)
                {
                    GameObject.Find("/CanvasMain/Backgroundentities/Eolicos/Eolico" + upgradelevel).SetActive(true);
                    GameObject.Find("/CanvasMain/Backgroundentities/Eolicos/Eolico" + upgradelevel + "Pole").SetActive(true);
                }
                break;
            case 1:
                if(upgradelevel < 4)
                {
                    GameObject.Find("/CanvasMain/Backgroundentities/Hidreletrica/corpo" + upgradelevel).SetActive(true);
                }
                break;
            case 3:
                if(upgradelevel < 6)
                {
                    GameObject.Find("/CanvasMain/Backgroundentities/Solar/corpo" + upgradelevel).SetActive(true);
                }
                break;
            default:
                break;


        }
    }

    public void MakeUpgradeNaoRenovavel(int position)
    {
        double totalCost = 0;

        for (int i = 0; i < purchaseMultiplier; i++)
        {
            totalCost += UpgradeCostNaoRenovaveis[position] * Math.Pow(1.15, UpgradeLevelNaoRenovaveis[position] + i);
        }

        if (totalCost > TotalClicks)
        {
            Upgrade1ButtonText.color = Color.red;
            return;
        }

        for (int i = 0; i < purchaseMultiplier; i++)
        {
            ++UpgradeLevelNaoRenovaveis[position];
            TotalCO2Emission += CO2EmissionPerUpgradeNaoRenovaveis[position];
            UpgradeCostNaoRenovaveis[position] *= 1.15;

            PowerPerClickGas = UpgradeLevelNaoRenovaveis[0] * 2;
            PowerPerClickOleo = UpgradeLevelNaoRenovaveis[1] * 10;
            PowerPerClickCarvao = UpgradeLevelNaoRenovaveis[2] * 15;
            PowerPerClickNuke = UpgradeLevelNaoRenovaveis[3] * 50;
        }

        TotalClicks -= totalCost;
    }
    private int GetPurchaseMultiplier()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            return 10;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            return 25;
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            return 100;
        }
        return 1;
    }
    public void upgradeVisual(int upgrade)
    {
        switch (upgrade)
        {
            case 0:

                upgradeVisualEolico1 = 2;
                TotalClicks = TotalClicks - 10000;
                break;
            case 1:
                TotalClicks -= 1000000;
                upgradeVisualEolico2 = 5;
                
                break;
        }

    }
    string GetNumeroFormatado(double numero)
    {

        if (numero > 10000)
        {
            int power = (int)Math.Floor(Math.Log10(numero) + 1);
            if (power >= 5 && power < 7)
            {
                string returnstring = "";

                returnstring = Math.Round((numero / 1000), 3).ToString();
                returnstring += "k";
                return returnstring;
            }
            if (power >= 7)
            {
                string returnstring = "";
                returnstring = Math.Round((numero / 1000000), 3).ToString();
                returnstring += "M";
                return returnstring;

            }
        }
        return Math.Round(numero, 2).ToString();


    }
    public void MakeUpgrade(int position)
    {
        powerpersecondtext.SetActive(true);
        if (EnergyType == 0)
        {
            MakeUpgradeRenovavel(position);
        }
        else
        {
            MakeUpgradeNaoRenovavel(position);
        }
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
        for(int i = 1; i < 6; i++)
        {
            GameObject.Find("/CanvasMain/Backgroundentities/Eolicos/Eolico" + i).SetActive(false);
            GameObject.Find("/CanvasMain/Backgroundentities/Eolicos/Eolico" + i + "Pole").SetActive(false);
            GameObject.Find("/CanvasMain/Backgroundentities/Solar/corpo" + i).SetActive(false);
            if (i < 4)
            {
                GameObject.Find("/CanvasMain/Backgroundentities/Hidreletrica/corpo" + i).SetActive(false);
            }
            
        }
        this.TotalClicks = 0;
        this.PowerGenerationPerSecond = 0;
        this.PowerGenerationPerClick = 1;
        this.TotalCO2Emission = 0;
    }

    private double GetTotalCost(double baseCost, double currentLevel, int multiplier)
    {
        double totalCost = 0;
        for (int i = 0; i < multiplier; i++)
        {
            totalCost += baseCost * Math.Pow(1.1, currentLevel + i);
        }
        return totalCost;
    }

    private void Update()
{
    purchaseMultiplier = GetPurchaseMultiplier();

    // Atualiza os cálculos visuais
    TotalClicksText.text = GetNumeroFormatado(TotalClicks);
    PowerGenerationPerClick = 1 + PowerPerClickEolica + PowerPerClickHidro + PowerPerClickBio + PowerPerClickSolar 
                               + PowerPerClickGas + PowerPerClickCarvao + PowerPerClickOleo + PowerPerClickNuke;
    PowerGenerationPerSecond = 0.1f * (float)(PowerPerClickEolica + PowerPerClickHidro + PowerPerClickBio + PowerPerClickSolar);

    PowerGenerationPerSecondText.text = GetNumeroFormatado(PowerGenerationPerSecond) + " MW/s";
    PowerPerClick.text = GetNumeroFormatado(PowerGenerationPerClick) + " MW/Click";
    CO2EmissionText.text = $"CO2 emitido: {Math.Round(TotalCO2Emission, 2)} KG de CO2";

        UpgradeVisual0.interactable = (TotalClicks >= 10000 && upgradeVisualEolico1 == 0);

        UpgradeVisual1.interactable = (TotalClicks >= 1000000 && UpgradeLevelRenovaveis[0] >= 100) && upgradeVisualEolico2 == 0 ;

        UpgradeVisual2.interactable = (TotalClicks >= 1000000 && UpgradeLevelRenovaveis[1] >= 50) && upgradeVisualHidro1 == 0;
        UpgradeVisual3.interactable = false;

        switch (EnergyType)
        {
            case 0:
                for (int i = 0; i < UpgradeRenovaveis.Length; i++)
                {
                    UpgradeButtonText[i].text = $"{UpgradeRenovaveis[i]}\nNível: {UpgradeLevelRenovaveis[i]}\nCusto ({purchaseMultiplier}x): {GetNumeroFormatado(GetTotalCost(UpgradeCostRenovaveis[i], UpgradeLevelRenovaveis[i], purchaseMultiplier))}";

                    upgradeButton[i].interactable = TotalClicks >= GetTotalCost(UpgradeCostRenovaveis[i], UpgradeLevelRenovaveis[i], purchaseMultiplier);
                    UpgradeButtonText[i].color = Color.black;
                }
                break;

            case 1:
                for (int i = 0; i < UpgradeNaoRenovaveis.Length; i++)
                {
                    UpgradeButtonText[i].text = $"{UpgradeNaoRenovaveis[i]}\nNível: {UpgradeLevelNaoRenovaveis[i]}\nCusto ({purchaseMultiplier}x): {GetNumeroFormatado(GetTotalCost(UpgradeCostNaoRenovaveis[i], UpgradeLevelNaoRenovaveis[i], purchaseMultiplier))}";

                    upgradeButton[i].interactable = TotalClicks >= GetTotalCost(UpgradeCostNaoRenovaveis[i], UpgradeLevelNaoRenovaveis[i], purchaseMultiplier);
                    UpgradeButtonText[i].color = Color.black;
                }
                break;
        }
    }
}
