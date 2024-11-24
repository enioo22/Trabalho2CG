using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class manager : MonoBehaviour
{
    public TextMeshProUGUI UpgradeTitleRenovaveis;
    public TextMeshProUGUI TotalClicksText;
    public TextMeshProUGUI PowerGenerationPerSecondText;
    public TextMeshProUGUI Upgrade1ButtonText;
    public TextMeshProUGUI PowerPerClick;
    public TextMeshProUGUI CO2EmissionText;
    public TextMeshProUGUI[] UpgradeButtonText;

    float PowerValue;
    float PowerGenerationPerClick;
    float PowerGenerationPerSecond;
    double TotalClicks;
    double TotalCO2Emission;
    const int NumberOfUpgrades = 6;

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
    }

 // Upgrade para Renováveis
    public void MakeUpgradeRenovavel(int position)
    {
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

    public String getUpgradeCost(int position)
    {
        return this.UpgradeCostRenovaveis[position].ToString();
    }
    
    private void Update()
    {
        this.TotalClicksText.text = Math.Round(this.TotalClicks, 2).ToString();
        this.PowerPerClick.text = this.PowerGenerationPerClick.ToString() + "MW/Click";
        this.CO2EmissionText.text = $"CO2 emitido: {Math.Round(this.TotalCO2Emission, 2)} KCO2";

         for (int i = 0; i < UpgradeRenovaveis.Length; i++)
        {
            UpgradeButtonText[i].text = 
                $"{UpgradeRenovaveis[i]}\nNível: {UpgradeLevelRenovaveis[i]}\nCusto: {Math.Round(UpgradeCostRenovaveis[i], 2)}\nemissão de CO2: {CO2EmissionPerUpgradeRenovaveis[i]} kg/MWh";
        }

        
        for (int i = 0; i < UpgradeNaoRenovaveis.Length; i++)
        {
            UpgradeButtonText[i + UpgradeRenovaveis.Length].text =
                $"{UpgradeNaoRenovaveis[i]}\nNível: {UpgradeLevelNaoRenovaveis[i]}\nCusto: {Math.Round(UpgradeCostNaoRenovaveis[i], 2)}\nCO2: {CO2EmissionPerUpgradeNaoRenovaveis[i]} kg/MWh";
        }
    }
}
