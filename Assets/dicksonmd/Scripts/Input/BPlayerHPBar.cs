using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BPlayerHPBar : MonoBehaviour {


    public void updateHPBar(float hp, float hp_max)
    {
        this.GetComponent<Slider>().maxValue = hp_max;
        this.GetComponent<Slider>().value = hp;
    }
}
