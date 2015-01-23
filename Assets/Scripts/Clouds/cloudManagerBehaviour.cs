using UnityEngine;
using System.Collections;

public class cloudManagerBehaviour : MonoBehaviour {

    public bool debug = false;
    public bool debugModules = false;
    public GameObject chunkPrefab;
    public int cloudsPerChunk = 15;
    public int chunkSize = 4000;
    private System.Random rand = new System.Random();
    private GameObject[] chunks = new GameObject[30];

	// Use this for initialization
	void Start () {
        int count = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    ++count;
                    Vector3 pos = new Vector3(chunkSize * i, chunkSize * j, chunkSize*k);
                    GameObject chunk = Instantiate(chunkPrefab,this.transform.position+pos,Quaternion.identity) as GameObject;

                    chunkBehaviour behaviour = chunk.GetComponent<chunkBehaviour>();
                    behaviour.chunkID = count;
                    behaviour.seed = this.rand.Next();
                    behaviour.chunkSize = this.chunkSize;
                    behaviour.cloudCount = this.cloudsPerChunk;
                    behaviour.chunkArea = new Vector3(i,j,k);

                    if (this.debugModules) behaviour.defaultColor = new HSBColor(
                        1.0f/27*count,
                        0.3f,
                        0.15f*(k+1)+0.5f,
                        0.7f
                    ).ToColor();

                    if (this.debug) Debug.Log(count + ":" + pos);
                    chunk.transform.parent = this.transform;
                    chunks[count] = chunk;
                }
            }
        }
        //Debug.Log(chunks);
	}

    // Update is called once per frame
    void Update(){}

    internal void ChunkSwap(Vector3 dir)
    {
        this.moveChunks(dir);
        this.moveTreadmillObjects(dir);
    }

    private void moveTreadmillObjects(Vector3 dir)
    {
        Vector3 amount = dir * this.chunkSize;
        GameObject[] treadmillObjects = GameObject.FindGameObjectsWithTag("TreadmillObjects");
        //GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] swappedObjects = new GameObject[treadmillObjects.Length];

        treadmillObjects.CopyTo(swappedObjects, 0);
        //enemyObjects.CopyTo(swappedObjects, treadmillObjects.Length);

        foreach (GameObject go in swappedObjects)
        {
            //Debug.Log(go);
            go.transform.position -= amount;
        }
    }

    private void moveChunks(Vector3 dir)
    {
        GameObject chunk; // reuse
        for (int i = 1; i <= 27; i++)
        {
            chunk = chunks[i];
            chunk.GetComponent<chunkBehaviour>().shiftChunk(dir);
        }
    }

}
