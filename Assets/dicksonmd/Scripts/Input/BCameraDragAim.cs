using UnityEngine;
using System.Collections;

public class BCameraDragAim : MonoBehaviour
{


    void Start()
    {
        var spire = GameObject.Find("StructureCenterSpire");
        if (true && spire != null)
        {
            StartCoroutine(zeroStartPoint());
        }
        else
        {
            print("no spire detected. will not initialize player rotation");
        }
    }

    public void rotateHorizontal(float amount)
    {
        this.transform.Rotate(Vector3.up, amount);
    }

    public void rotateVertical(float amount)
    {
        this.transform.Rotate(Vector3.right, amount);
    }

    IEnumerator zeroStartPoint()
    {
        yield return new WaitForSeconds(0.5f);

        var spire = GameObject.Find("StructureCenterSpire");

        GyroController gyro = this.GetComponentInChildren<GyroController>();
        var myTransform = this.transform;
        var playerTransform = gyro.transform;

        // swap parent
        playerTransform.parent = myTransform.parent;
        myTransform.parent = gyro.transform;

        // rotate player directly,
        // so that it retains its pitch and row, but yaw exactly 180 degree away from spire
        var directionToSpire = spire.transform.position - playerTransform.position;
        directionToSpire.y = 0;
        var lookingAtYComponent = playerTransform.forward.y;
        var shouldLookAtVector = -1 * (directionToSpire.normalized);
        shouldLookAtVector.y = lookingAtYComponent;
        playerTransform.rotation = Quaternion.LookRotation(shouldLookAtVector, Vector3.up);

        // swap parent
        myTransform.parent = gyro.transform.parent;
        var myLookingDirection = myTransform.forward;
        myLookingDirection.y = 0;
        myTransform.rotation = Quaternion.LookRotation(myLookingDirection, Vector3.up);
        gyro.transform.parent = myTransform;
    }
}
