using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : GameSystem
{
    [Tooltip("Minimum distance for swiping")]
    [SerializeField] float MinimumDistance = 40;

    private bool  isDraging;
    private Vector2 startPosition;
    private Vector3 swipeDelta;
    private Action action;
    private Action previousAction;
    private bool validTouchInput = true;

    [SerializeField] GameEvent OnTouch;

    public enum Action
    {
        NULL, touch, swipeLeft, swipeRight, swipeUp, swipeDown, zoomIn
    }

    private void Update()
    {
        ValidateTouchInput();
        action = Action.NULL;

        #region Mouse Inputs
        if (Input.GetMouseButtonDown(0))
        {
            if(validTouchInput) OnTouch.Raise();
            isDraging = true;
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            action = Action.NULL;
            Reset();
        }
        #endregion


        #region Mobile Inputs

        if(Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if(validTouchInput) action = Action.touch;
                isDraging = true;
                startPosition = Input.GetTouch(0).position;
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            {
                Reset();
                action = Action.NULL;
            }
        }
        #endregion

        swipeDelta = Vector2.zero;

        if (isDraging)
        {
            if(Input.touchCount > 0)
            {
                swipeDelta = Input.GetTouch(0).position - startPosition;

            }
            else if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startPosition;
            }
        }

        if(swipeDelta.magnitude > MinimumDistance)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x > 0) // Swipe right
                {
                    action = Action.swipeRight;
                    previousAction = action;
                }
                else // Swipe left
                {
                    action = Action.swipeLeft;
                    previousAction = action;
                }
            }
            else
            {
                if (y > 0) // Swipe up
                {
                    action = Action.swipeUp;  
                    previousAction = action;
                }
                else // Swipe down
                {
                    action = Action.swipeDown;
                    previousAction = action;
                }
            }
            Reset();
        }

        if (ZoomingIn()) action = Action.zoomIn;
    }


    private void Reset()
    {
        startPosition = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    public Action GetPreviousAction() => previousAction;



    public bool ZoomingIn()
    {
        if (Input.touchCount > 1)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touchOnePreviosPos = touch1.position - touch1.deltaPosition;
            Vector2 touchTwoPreviosPos = touch2.position - touch2.deltaPosition;



            float preMagnitude = (touchOnePreviosPos - touchTwoPreviosPos).magnitude;
            float curMagnitude = (touch1.position - touch2.position).magnitude;

            float different = curMagnitude - preMagnitude;

            float angle1 = Vector2.Angle(touchOnePreviosPos - touch1.position, Vector2.right);
            if (angle1 > 90) angle1 = 180 - angle1;
            float angle2 = Vector2.Angle(touchTwoPreviosPos - touch2.position, Vector2.right);
            if (angle2 > 90) angle2 = 180 - angle2;

            if (different > 5 && angle1 < 30 && angle2 < 30)
            {
                action = Action.zoomIn;
                return true;
            }
        }


        return false;
    }

    public bool GetKeyDown(KeyCode k) => Input.GetKeyDown(k);

    private void ValidateTouchInput()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        // Mouse input
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject != null &&
                 EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
            {
                validTouchInput = false;
            }
            else
                validTouchInput = true;

        }
#else
        // Moblie inputs
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && EventSystem.current.currentSelectedGameObject != null &&
                     EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)

                validTouchInput = false;
            else
                validTouchInput = true;
        }
#endif
    }


}
