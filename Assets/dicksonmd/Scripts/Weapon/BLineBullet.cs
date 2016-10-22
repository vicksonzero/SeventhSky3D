using UnityEngine;
using System.Collections;

public class BLineBullet : MonoBehaviour {

    private LineRenderer line;
    public Transform bulletSpawnVector;

    public float duration=0.01f;
    private float elapsed=1;

	// Use this for initialization
	void Start () {
        this.line = this.GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (this.elapsed < this.duration)
        {
            this.elapsed += Time.deltaTime;
        }
        else
        {
            this.Hide();
            this.elapsed = this.duration;
        }
	}

    public void Shoot(Vector3 dest)
    {
        this.Hide();
        this.line.SetPosition(0, this.bulletSpawnVector.position);
        this.line.SetPosition(1,dest);
        this.line.enabled = true;

        this.elapsed = 0;
    }

    public void Hide()
    {
        this.line.enabled = false;
    }
}
