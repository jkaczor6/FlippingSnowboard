using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float baseSpeed = 20f;
    [SerializeField] float boostSpeed = 25f;
    InputAction moveAction;
    Rigidbody2D rb2d;
    SurfaceEffector2D se2d;
    Vector2 moveVector;
    bool canControlPlayer = true;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb2d = GetComponent<Rigidbody2D>();
        se2d = FindAnyObjectByType<SurfaceEffector2D>();
    }

    void Update()
    {
        if(canControlPlayer)
        {
            RotatePlayer();
            BoostPlayer();
        }
        }

    void RotatePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        if(moveVector.x < 0)
        {
            rb2d.AddTorque(torqueAmount);
        } else if (moveVector.x > 0)
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }

    void BoostPlayer()
    {
        if(moveVector.y > 0)
        {
            se2d.speed = boostSpeed;
        } else
        {
            se2d.speed = baseSpeed;
        }
    }

    public void DisableControls()
    {
        canControlPlayer = false;
    }
}
