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
    public DialogManager dialogmanager;
    public TextMeshProUGUI TotalClicksText;
    public TextMeshProUGUI PowerGenerationPerSecondText;
    public TextMeshProUGUI Upgrade1ButtonText;
    public TextMeshProUGUI PowerPerClick;
    public TextMeshProUGUI[] UpgradeButtonText;

    float PowerValue = 0;
    float PowerGenerationPerClick = 0;
    float PowerGenerationPerSecond = 0;
    double TotalClicks = 0;
    const int NumberOfUpgrades = 6;
    public int EnergyType = 0; // 0 = Limpas, 1 = Sujas;

    String[] UpgradeName = { "Hidrelétrica", "Usina Solar", "Usina Nuclear" };
    String[] DirtyUpgradeName = { "Carvão", "Petróleo", "Diesel" };
    double[] UpgradeCost = { 10, 10000, 100000000 };
    double[] DirtyUpgradeCost = { 500, 1000, 2500000000 };
    double[] UpgradeLevel = { 0, 0, 0 };
    double[] DirtyUpgradeLevel = { 0, 0, 0 };

    public double getUpgradeCost(double InitialCost, int NumberOfUpgrades)
    {
        return InitialCost * Math.Pow(1.2, NumberOfUpgrades);
    }

    

    public void mainClick()
    {
        this.TotalClicks += this.PowerGenerationPerClick;
        this.PowerValue += this.PowerGenerationPerClick;
        this.TotalClicksText.text = Math.Round(this.TotalClicks, 2).ToString();
        if(PowerValue >= 1)
        {
            dialogmanager.callDialog(0);
        }
    }

    public void MakeUpgrade(int position)
    {
        if (this.UpgradeCost[position] > this.TotalClicks)
        {
            Upgrade1ButtonText.color = Color.red;
            Upgrade1ButtonText.color = Color.black;
            return;
        }
        this.TotalClicks -= this.UpgradeCost[position];
        this.UpgradeLevel[position]++;
        this.UpgradeCost[position] *= 1.2;
        this.PowerGenerationPerClick++;
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
    }

    public String getUpgradeCost(int position)
    {
        return this.UpgradeCost[position].ToString();
    }

    private void Update()
    {
        PowerPerClick.text = PowerGenerationPerClick.ToString() + " mW/Click";
        switch (EnergyType)
        {
            case 0:
                for (int i = 0; i < UpgradeName.Length; i++)
                {
                    UpgradeButtonText[i].text = UpgradeName[i] + "\nNível: " + (UpgradeLevel[i]) + "\nCusto: " + Math.Round(UpgradeCost[i], 2);
                }
                break;
            case 1:
                for (int i = 0; i < DirtyUpgradeName.Length; i++)
                {
                    UpgradeButtonText[i].text = DirtyUpgradeName[i] + "\nNível: " + (DirtyUpgradeLevel[i]) + "\nCusto: " + Math.Round(DirtyUpgradeCost[i], 2);
                }
                break;
        }
    }



}
