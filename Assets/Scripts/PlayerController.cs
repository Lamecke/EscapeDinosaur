using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 moveVector;
    public float verticalVelocity;
    public bool isGrounded;
    public int currentLane;

    public float distanceBetweenLanes = 3.0f;
    public float baseRunSpeed = 5.0f;
    public float gravity = 14.0f;
    public float baseSidewaySpeed = 10.0f;
    public float terminalVelocity = 20.0f;

    public CharacterController controller;
    public Animator animator;

    [SerializeField] LayerMask layerCarMask;
    [SerializeField] float rayDistance = 0.8f;
    public bool collided = false;
    private BaseState state;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        state = GetComponent<RunningState>();
        state.Construct();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetGameState() == GameState.start)
        {

            OnPlayerCollision();
            UpdateMotor();
        }

    }
    private void UpdateMotor()
    {
        isGrounded = controller.isGrounded;

        moveVector = state.ProcessMotion();
        state.Transition();

        controller.Move(moveVector * Time.deltaTime);

    }
    public float SnapToLane()
    {
        float r = 0.0f;
        if (transform.position.x != (currentLane * distanceBetweenLanes))
        {
            float deltaDeseridPosition = (currentLane * distanceBetweenLanes) - transform.position.x;
            r = (deltaDeseridPosition > 0) ? 1 : -1;
            r *= baseSidewaySpeed;

            float actualDistance = r * Time.deltaTime;
            if (Mathf.Abs(actualDistance) > Mathf.Abs(deltaDeseridPosition))
                r = deltaDeseridPosition * (1 / Time.deltaTime);

        }
        else
        {
            r = 0.0f;
        }
        return r;
    }
    public void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, -1, 1);
    }
    public void ChangeState(BaseState s)
    {
        state.Destruct();
        state = s;
        state.Construct();
    }
    private void OnPlayerCollision()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDistance, layerCarMask))
        {
            if (hit.collider != null)
            {
                GameManager.Instance.SetGameState(GameState.finish);
                animator.SetTrigger("die");

            }
            

        }
    }
 
}
