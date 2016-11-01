using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BTutor : MonoBehaviour
{

    public RectTransform movementUIGroup;
    public RectTransform attackUIGroup;
    public Transform radar3d;

    public BRendezvous spireRendezvous;

    public BReSpawner spawner;

    // Use this for initialization
    void Start()
    {
        if (spireRendezvous) spireRendezvous.onEnter += this.onArriveAtPillar;
        this.movementUIGroup.GetComponent<CanvasGroup>().alpha = 0.01f;
        attackUIGroup.gameObject.SetActive(false);
        radar3d.gameObject.SetActive(false);

        StartCoroutine(tutorial(0, 3));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator tutorial(int progress, float delay)
    {
        yield return new WaitForSeconds(delay);
        switch (progress)
        {
            case 0:
                BUIMessage.log("Welcome to the tutorial.");
                StartCoroutine(tutorial(1, 3));
                break;
            case 1:
                BUIMessage.log("Tilt your phone to look around and aim.");
                StartCoroutine(tutorial(6, 5));
                break;
            case 6:
                radar3d.gameObject.SetActive(true);
                BUIMessage.log("Swipe this to reach more angles.");
                StartCoroutine(tutorial(2, 5));
                break;
            case 2:
                radar3d.gameObject.SetActive(true);
                showMovement();
                BUIMessage.log("Use these controls to move around.");
                StartCoroutine(tutorial(3, 5));
                break;
            case 3:
                showRendezvous();
                BUIMessage.log("Try to move near the red spire.");
                break;
            case 4:
                radar3d.gameObject.SetActive(true);
                showMovement();
                attackUIGroup.gameObject.SetActive(true);
                BUIMessage.log("Use these controls to attack.");
                StartCoroutine(tutorial(5, 7));
                break;
            case 5:
                spawner.startSpawning();
                BUIMessage.log("Try to eliminate 30 enemies as quickly as possible.");
                break;
            default:
                break;
        }
    }

    public void onArriveAtPillar(BUnit unit)
    {
        if (unit.unitName == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(tutorial(4, 0.5f));
            Destroy(spireRendezvous.gameObject);
        }
    }

    void showRendezvous()
    {
        this.spireRendezvous.GetComponent<BOnRadarBehind>().enabled = true;
        this.spireRendezvous.GetComponent<BOnRadarShootingRange>().enabled = true;
    }

    void showMovement()
    {
        this.movementUIGroup.GetComponent<CanvasGroup>().alpha = 1;
    }
}
