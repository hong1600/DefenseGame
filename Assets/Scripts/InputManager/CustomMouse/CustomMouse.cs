using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomMouse : MonoBehaviour
{
    GameObject lastEventObj = null;
    PointerEventData eventData;

    [SerializeField] Image customCursor;
    [SerializeField] float mouseSpeed;
    [SerializeField] LayerMask objLayer;

    Vector2 mousePos;

    private void Awake()
    {
        InputManager.instance.customMouse = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InputManager.instance.onInputMouse += CheckUIOrObj;
        InputManager.instance.onLeftClickUp += ClickMouseEvent;

        mousePos = new Vector2(Screen.width / 2, Screen.height / 2);
        customCursor.rectTransform.position = mousePos;
    }

    private void OnEnable()
    {
        if (InputManager.instance != null)
        {
            InputManager.instance.onInputMouse += CheckUIOrObj;
            InputManager.instance.onLeftClickUp += ClickMouseEvent;
        }
    }

    private void OnDisable()
    {
        InputManager.instance.onInputMouse -= CheckUIOrObj;
        InputManager.instance.onLeftClickUp -= ClickMouseEvent;
    }

    private void Update()
    {
        UpdateCursorLock();
        MoveMouseCursor();
    }

    private void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void MoveMouseCursor()
    {
        if(Cursor.lockState == CursorLockMode.Locked) 
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed;

            mousePos += new Vector2(mouseX, mouseY);
            mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
            mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                customCursor.canvas.transform as RectTransform,
                mousePos,
                customCursor.canvas.worldCamera,
                out Vector2 localPoint
            );
            customCursor.rectTransform.localPosition = localPoint;
        }
    }

    private void CheckUIOrObj()
    {
        GameObject target = CheckUI();

        if (target == null) 
        {
            target = CheckObj();
        }

        if (target != lastEventObj)
        {
            if (lastEventObj != null)
            {
                ExecuteEvents.Execute(lastEventObj, eventData, ExecuteEvents.pointerExitHandler);
            }
            if (target != null)
            {
                ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerEnterHandler);
            }

            lastEventObj = target;
        }
    }

    private GameObject CheckUI()
    {
        if (EventSystem.current == null) return null;

        eventData = new PointerEventData(EventSystem.current)
        {
            position = mousePos
        };

        List<RaycastResult> resultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, resultList);

        GameObject closeUI = null;
        float minDistance = float.MaxValue;

        for (int i = 0; i < resultList.Count; i++) 
        {
            RaycastResult result = resultList[i];

            if (result.distance < minDistance)
            {
                minDistance = result.distance;
                closeUI = result.gameObject;
            }
        }

        return closeUI;
    }

    private GameObject CheckObj()
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private void ClickMouseEvent(Vector2 _pos)
    {
        CheckUIOrObj();

        if (lastEventObj != null)
        {
            ExecuteEvents.Execute(lastEventObj, eventData, ExecuteEvents.pointerClickHandler);
        }
    }

    public Vector2 GetMousePos() { return mousePos; }
}
