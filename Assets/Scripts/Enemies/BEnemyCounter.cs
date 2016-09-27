using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BEnemyCounter : MonoBehaviour {

    public Text counterLabel;
    public int count=0;

    public delegate void EnemyCountUpdated();
    public event EnemyCountUpdated enemyCountUpdated;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void login()
    {
        this.count++;
        this.onEnemyCountUpdated();
    }

    public void logout()
    {
        this.count--;
        this.onEnemyCountUpdated();

    }

    public void onEnemyCountUpdated(){
        this.counterLabel.text = "Targets left: " + this.count.ToString();
        this.enemyCountUpdated();
    }
}
