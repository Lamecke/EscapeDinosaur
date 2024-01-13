using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }


    private RunningEnzo actionScheme;
    [SerializeField] private float sqrSwipeDeadZone = 50.0f;

    public bool Tap { get { return tap; } }
    public Vector2 TouchPosition { get { return touchPosition; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    private bool tap;
    private Vector2 touchPosition;
    private Vector2 startDrag;
    private bool swipeLeft;
    private bool swipeRight;
    private bool swipeUp;
    private bool swipeDown;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        SetupControls();
    }
    private void LateUpdate()
    {
        ResetInputs();
    }
    private void ResetInputs()
    {
        tap = swipeDown = swipeUp = swipeRight = swipeLeft = false;
    }
    private void SetupControls()
    {
        actionScheme = new RunningEnzo();

        actionScheme.GamePlay.Tap.performed += ctx => OnTap(ctx);
        actionScheme.GamePlay.TouchPosition.performed += ctx => OnPosition(ctx);
        actionScheme.GamePlay.StartDrag.performed += ctx => OnStartDrag(ctx);
        actionScheme.GamePlay.EndDrag.performed += ctx => OnEndDrag(ctx);
    }

    private void OnEndDrag(InputAction.CallbackContext ctx)
    {
        Vector2 delta = touchPosition - startDrag;
        float sqrDistance = delta.sqrMagnitude;

        if (sqrDistance > sqrSwipeDeadZone)
        {
            float x = Mathf.Abs(delta.x);
            float y = Mathf.Abs(delta.y);
            if (x > y)
            {
                if (delta.x > 0)
                    swipeRight = true;
                else
                    swipeLeft = true;
                Debug.Log("EndDrag");
            }
            else
            {
                if (delta.y > 0)
                    swipeUp = true;
                else
                    swipeDown = true;

            }
        }
        startDrag = Vector2.zero;
    }

    private void OnStartDrag(InputAction.CallbackContext ctx)
    {
        startDrag = touchPosition;
        Debug.Log("start");

    }

    private void OnTap(InputAction.CallbackContext ctx)
    {
        tap = true;
        Debug.Log("Tap");
    }

    private void OnPosition(InputAction.CallbackContext ctx)
    {
        touchPosition = ctx.ReadValue<Vector2>();
    }
    private void OnEnable()
    {
        actionScheme.Enable();
    }
    private void OnDisable()
    {
        actionScheme.Disable();
    }
}
