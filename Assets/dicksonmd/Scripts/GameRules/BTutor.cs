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

    private BUISound soundManager;

    // Use this for initialization
    void Start()
    {
        soundManager = GameObject.FindObjectOfType<BUISound>();

        if (spireRendezvous) spireRendezvous.onEnter += this.onArriveAtPillar;
        this.movementUIGroup.GetComponent<CanvasGroup>().alpha = 0.05f;
        this.attackUIGroup.GetComponent<CanvasGroup>().alpha = 0.05f;
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
                soundManager.playNewWaveAlert();
                BUIMessage.log("Welcome to the tutorial.");
                StartCoroutine(tutorial(1, 3));
                break;
            case 1:
                soundManager.playNewWaveAlert();
                BUIMessage.log("Tilt your phone to look around and aim.");
                StartCoroutine(tutorial(6, 5));
                break;
            case 6:
                radar3d.gameObject.SetActive(true);
                soundManager.playNewWaveAlert();
                BUIMessage.log("Swipe this to reach more angles.");
                StartCoroutine(tutorial(2, 5));
                break;
            case 2:
                radar3d.gameObject.SetActive(true);
                showMovement();
                soundManager.playNewWaveAlert();
                BUIMessage.log("Use these controls to move around.");
                StartCoroutine(tutorial(3, 5));
                break;
            case 3:
                showRendezvous();
                soundManager.playNewWaveAlert();
                BUIMessage.log("Try to move near the red spire.");
                break;
            case 4:
                radar3d.gameObject.SetActive(true);
                showMovement();
                showAttack();
                soundManager.playNewWaveAlert();
                BUIMessage.log("Use these controls to attack.");
                StartCoroutine(tutorial(5, 5));
                break;
            case 5:
                spawner.startSpawning();
                soundManager.playNewWaveAlert();
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

    void showAttack()
    {
        this.attackUIGroup.GetComponent<CanvasGroup>().alpha = 1;
    }
}
