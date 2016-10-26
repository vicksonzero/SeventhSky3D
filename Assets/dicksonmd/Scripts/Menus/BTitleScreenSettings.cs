using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BTitleScreenSettings : MonoBehaviour
{

    public Toggle useAutoHeal;

    // Use this for initialization
    void Start()
    {
        useAutoHeal.isOn = PlayerPrefs.GetInt("useAutoHeal", 1) == 1;
        useAutoHeal.onValueChanged.AddListener(this.onUseAutoHealValueChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onUseAutoHealValueChanged(bool isOn)
    {
        PlayerPrefs.SetInt("useAutoHeal", isOn ? 1 : 0);
    }
}
