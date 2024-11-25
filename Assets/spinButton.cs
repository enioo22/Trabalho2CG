using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class spinButton : MonoBehaviour
{

    public float spinAngle = 1;
    float MaxSpeed = 720;
    // Start is called before the first frame update

    public void SpinClick()
    {
        this.spinAngle *= 720;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.spinAngle >= MaxSpeed) { this.spinAngle = MaxSpeed; }
        if (this.spinAngle > 1) { spinAngle--; }
        transform.Rotate(0, 0, spinAngle * Time.deltaTime, Space.Self);
    }
}
