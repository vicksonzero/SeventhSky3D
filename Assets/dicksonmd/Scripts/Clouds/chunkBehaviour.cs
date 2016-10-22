using UnityEngine;
using System.Collections.Generic;

public class chunkBehaviour : MonoBehaviour {

    public bool debugBoundary = false;
    public bool debugColor = false;
    public Color defaultColor;

    // set by cloudManager, side length of the chunk
    [HideInInspector]
    public int chunkSize = 8000;

    // half of chunkSize
    private int size = 4000;

    [HideInInspector]
    public int cloudCount = 10;

    // list of clouds to choose from
    public ParticleSystem[] cloudPrefabs;

    // hard scale all clouds
    public float cloudScale;

    // list of clouds
    private List<ParticleSystem> clouds = new List<ParticleSystem>(10);

    // random generator
    private System.Random rand = new System.Random();
    // customize seed
    public int seed;

    // current position in the chunk matrix
    [HideInInspector]
    public Vector3 chunkArea = Vector3.zero;

    // chunk id for debug, setter also updates its hitbox
    private int _chunkID = -1;
    public int chunkID
    {
        get
        {
            return this._chunkID;
        }
        set
        {
            this._chunkID = value;
            this.transform.GetChild(0).GetComponent<treadmillBehaviour>().chunkID = value;
        }
    }

	// Use this for initialization
	void Start () {
        // init random
        this.rand = new System.Random(this.seed);
        // divide partition size by 2 to facilitate randomization
        this.size = this.chunkSize / 2;

	    // create a bunch of clouds in its control
        int x, y, z, cloudID;// reuse variables
        Color color;// reuse variables
        Vector3 pos;// reuse variables
        //Debug.Log(this.cloudScale);
        for (int i = 0; i < this.cloudCount; i++)
        {
            x = this.rand.Next(-1 * size, 1 * size);
            y = this.rand.Next(-1 * size, 1 * size);
            z = this.rand.Next(-1 * size, 1 * size);
            cloudID = this.rand.Next(3);
            color = chooseColorByDebug(cloudID);

            pos = this.transform.position + new Vector3(x,y,z);

            ParticleSystem cloud = this.createCloud(cloudID, pos, color);
            this.clouds.Add(cloud);
            cloud.transform.parent = this.transform;
        }
        //Debug.Log("cloud count:"+ this.cloudCount);

        Transform chunkBoundary = this.transform.GetChild(1);
        Transform chunkTrigger = this.transform.GetChild(0);
        chunkTrigger.GetComponent<treadmillBehaviour>().chunkSize = this.chunkSize;

        chunkBoundary.localScale = new Vector3(
            this.chunkSize * 1f,
            this.chunkSize * 1f,
            this.chunkSize * 1f
        );
        chunkTrigger.localScale = new Vector3(
            this.chunkSize * 1.25f,
            this.chunkSize * 1.25f,
            this.chunkSize * 1.25f
        );
        if (this.chunkID == 14) { /* do nothing for now */}
        if (!this.debugBoundary)
        {
            chunkTrigger.GetComponent<Renderer>().enabled = false;
            chunkBoundary.GetComponent<Renderer>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {}

    // when its chunkTrigger is triggered
    public void TreadmillTriggered(Vector3 area)
    {
        this.transform.parent.GetComponent<cloudManagerBehaviour>().ChunkSwap(area);
    }

    // create a new cloud using prefab ParticleSystem, with random rotation
    ParticleSystem createCloud(int cloudID, Vector3 pos,Color c){

        ParticleSystem p = Instantiate(cloudPrefabs[cloudID],pos,Random.rotation) as ParticleSystem;
        p.startColor = c;

        return p;
    }

    // create a color according to hex color values
    static Color rgba(int r,int g,int b, float a){
        return new Color(1.0f*r/255,1.0f*g/255,1.0f*b/255,a);
    }

    // returns different colors if debug, else returns a designated color (with differernt alpha).
    Color chooseColorByDebug(int cloudID)
    {
        Color color;
        if (this.debugColor)
        {
            color = new HSBColor(0.3f * cloudID, 0.4f, 1, 0.7f).ToColor();
        }
        else
        {
            HSBColor temp = HSBColor.FromColor(this.defaultColor);
            color = new HSBColor(
                temp.h,
                temp.s,
                temp.b,
                (float)(this.rand.NextDouble() * 0.35 + 0.3)
            ).ToColor();
        }
        return color;
    }

    // shifts the chunk in the direction of dir, wrapping at edges
    // updates this.chunkArea and this.transform.position
    internal void shiftChunk(Vector3 dir)
    {

        Vector3 temp = this.chunkArea - dir;
        Vector3 newChunkArea = new Vector3(
            ((temp.x + 1 + 3) % 3) - 1,
            ((temp.y + 1 + 3) % 3) - 1,
            ((temp.z + 1 + 3) % 3) - 1
        );
        //Debug.Log(this.chunkID +":"+ this.chunkArea + "-" + dir + "=" + newChunkArea);

        //Debug.Log(this.transform.position);
        this.transform.position = newChunkArea * this.chunkSize;
        //Debug.Log(newChunkArea +"x"+ this.chunkSize +"="+this.transform.position);
        this.chunkArea = newChunkArea;
        

    }
    
}
