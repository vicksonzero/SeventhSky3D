using UnityEngine;
using System.Collections;

public class treadmillBehaviour : MonoBehaviour {

    public int chunkID;
    public int chunkSize;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("enter#"+this.chunkID);
    }
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("stay#"+this.chunkID+":"+other.gameObject);
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("triggerExit#" + this.chunkID + ", object:"+other.gameObject.name+", coord:" + other.gameObject.transform.position);

        if (other.gameObject.name == "Player")
        {
            this.tellParent(other.gameObject.transform.position);
        }
    }


    private void tellParent(Vector3 playerPos)
    {
        Vector3 temp = playerPos / chunkSize * 2;
        Vector3 area = new Vector3((int)temp.x,(int)temp.y,(int)temp.z );
        //Debug.Log("Player in:" + area + ", parent:" + this.gameObject.transform.parent.GetComponent<chunkBehaviour>().chunkID + " " + this.gameObject.transform.parent.GetComponent<chunkBehaviour>().chunkArea);
        this.gameObject.transform.parent.GetComponent<chunkBehaviour>().TreadmillTriggered(area);
    }
}
