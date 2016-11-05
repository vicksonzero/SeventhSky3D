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

    public float blinkInterval = 0.2f;

    public Transform tutorialBounds;

    private BUISound soundManager;

    private int currentProgress = -1;

    private Coroutine tutorialRoutine;

    // Use this for initialization
    void Start()
    {
        soundManager = GameObject.FindObjectOfType<BUISound>();

        if (spireRendezvous) spireRendezvous.onEnter += this.onArriveAtPillar;
        this.movementUIGroup.GetComponent<CanvasGroup>().alpha = 0.01f;
        this.attackUIGroup.GetComponent<CanvasGroup>().alpha = 0.01f;
        radar3d.gameObject.SetActive(false);

        this.tutorialRoutine = StartCoroutine(tutorial(0, 3));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator tutorial(int progress, float delay)
    {
        print("CurrentProgress: " + currentProgress + " vs " + progress);
        if (currentProgress < progress - 1)
        {
            BAnalyticsGA.logSkipTutorial();
        }
        this.currentProgress = progress;
        yield return new WaitForSeconds(delay);
        switch (progress)
        {
            case 0:
                soundManager.playNewWaveAlert();
                BUIMessage.log("Welcome to the tutorial.");
                this.tutorialRoutine = StartCoroutine(tutorial(1, 3));
                break;
            case 1:
                soundManager.playNewWaveAlert();
                BUIMessage.log("Tilt your phone to look around and aim.");
                this.tutorialRoutine = StartCoroutine(tutorial(2, 5));
                break;
            case 2:
                showRadar3D();
                soundManager.playNewWaveAlert();
                BUIMessage.log("Swipe this to reach more angles.");
                this.tutorialRoutine = StartCoroutine(tutorial(3, 5));
                break;
            case 3:
                showRadar3D();
                showMovement();
                soundManager.playNewWaveAlert();
                BUIMessage.log("Use these controls to move around.");
                this.tutorialRoutine = StartCoroutine(tutorial(4, 5));
                break;
            case 4:
                showRendezvous();
                soundManager.playNewWaveAlert();
                BUIMessage.log("Try to move near the red spire.");
                break;
            case 5:
                showRadar3D();
                showMovement();
                showAttack();
                Destroy(tutorialBounds.gameObject);
                soundManager.playNewWaveAlert();
                BUIMessage.log("Use these controls to attack.");
                this.tutorialRoutine = StartCoroutine(tutorial(6, 5));
                break;
            case 6:
                spawner.startSpawning();
                soundManager.playNewWaveAlert();
                BUIMessage.log("Try to eliminate 20 enemies as quickly as possible.");
                break;
            default:
                break;
        }
    }

    public void onArriveAtPillar(BUnit unit)
    {
        if (unit.unitName == "Player")
        {
            if (this.tutorialRoutine != null) StopCoroutine(tutorialRoutine);
            this.tutorialRoutine = StartCoroutine(tutorial(5, 0.5f));
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
        var movementGroups = this.movementUIGroup.GetComponent<CanvasGroup>();
        if (movementGroups.alpha != 1)
        {
            BlinkElement(movementGroups);
        }
    }

    void showAttack()
    {
        var attackGroups = this.attackUIGroup.GetComponent<CanvasGroup>();
        if (attackGroups.alpha != 1)
        {
            BlinkElement(attackGroups);
        }
    }

    void showRadar3D()
    {
        if (! radar3d.gameObject.activeSelf)
        {
            radar3d.gameObject.SetActive(true);
            BlinkElement(radar3d);
        }
    }

    void BlinkElement(Transform radar3d)
    {
        StartCoroutine(blink2d(radar3d));
    }

    IEnumerator blink2d(Transform radar3d)
    {
        var renderers = radar3d.GetComponentsInChildren<LineRenderer>();
        for (int i = 0; i < 5; i++)
        {
            foreach (var renderer in renderers)
            {
                renderer.enabled = true;
            }
            yield return new WaitForSeconds(this.blinkInterval);
            foreach (var renderer in renderers)
            {
                renderer.enabled = false;
            }
            yield return new WaitForSeconds(this.blinkInterval);
        }
        foreach (var renderer in renderers)
        {
            renderer.enabled = true;
        }
    }

    void BlinkElement(CanvasGroup buttons2D)
    {
        StartCoroutine(blink2d(buttons2D));
    }

    IEnumerator blink2d(CanvasGroup buttons2D)
    {
        for (int i = 0; i < 5; i++)
        {
            buttons2D.alpha = 1;
            yield return new WaitForSeconds(this.blinkInterval);
            buttons2D.alpha = 0.05f;
            yield return new WaitForSeconds(this.blinkInterval);
        }
        buttons2D.alpha = 1;
    }
}
