using UnityEngine;
using System.Collections;

public class BCameraDragAim : MonoBehaviour {

    Quaternion startRotation;

    void Start()
    {
        this.startRotation = this.transform.localRotation;
    }

    public void rotateHorizontal(float amount)
    {
        this.transform.Rotate(Vector3.up, amount);
    }

    public void rotateVertical(float amount)
    {
        this.transform.Rotate(Vector3.right, amount);
    }
}
