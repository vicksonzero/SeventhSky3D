using UnityEngine;
using System.Collections;

public class BTapToContinue : MonoBehaviour {

	public void OnMouseUp ()
    {
        Destroy(this.gameObject);
    }
	
}
