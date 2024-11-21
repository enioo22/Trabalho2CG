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

    public TextMeshProUGUI TotalClicksText;
    public TextMeshProUGUI PowerGenerationPerSecondText;
    public TextMeshProUGUI Upgrade1ButtonText;
    public TextMeshProUGUI PowerPerClick;
    public TextMeshProUGUI[] UpgradeButtonText;

    float PowerValue;
    float PowerGenerationPerClick;
    float PowerGenerationPerSecond;
    double TotalClicks;
    const int NumberOfUpgrades = 6;

    String[] UpgradeName = { "Hidrelétrica", "Usina Solar", "Usina Nuclear" };
    double[] UpgradeCost = { 10, 10000, 100000000 };
    double[] UpgradeLevel = { 0, 0, 0 };

    public double getUpgradeCost(double InitialCost, int NumberOfUpgrades)
    {
        return InitialCost * Math.Pow(1.2, NumberOfUpgrades);
    }

    

    public void mainClick()
    {
        this.TotalClicks += this.PowerGenerationPerClick;
        this.PowerValue += this.PowerGenerationPerClick;
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
        this.TotalClicksText.text = Math.Round(this.TotalClicks, 2).ToString();
        this.PowerPerClick.text = this.PowerGenerationPerClick.ToString() + "mW/Click";
        for(int i = 0; i<3; i++)
        {
            this.UpgradeButtonText[i].text = this.UpgradeName[i] + "\nNível: " + (this.UpgradeLevel[i]) + "\nCusto: " + Math.Round(this.UpgradeCost[i], 2);
        }
    }



}
