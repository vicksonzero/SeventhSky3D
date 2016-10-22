using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(EventTrigger))]
public class BUIDragScreen : MonoBehaviour {

    float startX = 0;
    float startY = 0;
    public float horiSensitivity = -0.01f;
    public float vertSensitivity = 0; //0.002f;

    public BMainInput mainInput;

	// Use this for initialization
	void Start ()
    {
        EventTrigger trigger = GetComponentInParent<EventTrigger>();

        // event handlers for PointerEnter
        EventTrigger.Entry entryPointerEnter = new EventTrigger.Entry();
        entryPointerEnter.eventID = EventTriggerType.PointerDown;
        entryPointerEnter.callback.AddListener((eventData) => { onPointerDown(eventData); });
        trigger.triggers.Add(entryPointerEnter);

        // event handlers for PointerExit
        EventTrigger.Entry entryPointerExit = new EventTrigger.Entry();
        entryPointerExit.eventID = EventTriggerType.Drag;
        entryPointerExit.callback.AddListener((eventData) => { onPointerDrag(eventData); });
        trigger.triggers.Add(entryPointerExit);

    }
	
	// Update is called once per frame
	void Update () {

    }

    void onPointerDown(BaseEventData e)
    {
        Vector2 localCursor;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                ((PointerEventData)e).position,
                ((PointerEventData)e).pressEventCamera,
                out localCursor))
        {
            return;

        }

        startX = localCursor.x;
        startY = localCursor.y;
        //Debug.Log("LocalCursor:" + localCursor);
    }

    void onPointerDrag(BaseEventData e)
    {
        Vector2 localCursor;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GetComponent<RectTransform>(),
                ((PointerEventData)e).position,
                ((PointerEventData)e).pressEventCamera,
                out localCursor))
        {
            return;
        }

        var xx = startX - localCursor.x;
        var yy = startY - localCursor.y;
        rotateWorld(xx * horiSensitivity, yy * vertSensitivity);
        startX = localCursor.x;
        startY = localCursor.y;
    }

    void rotateWorld(float horizontal, float vertical)
    {
        mainInput.tuneCamera(horizontal, vertical);
    }
}
