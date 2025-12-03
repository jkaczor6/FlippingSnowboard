using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    InputAction moveAction;
    Rigidbody2D rb2d;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 moveVector;
        moveVector = moveAction.ReadValue<Vector2>();
        if(moveVector.x < 0)
        {
            rb2d.AddTorque(torqueAmount);
        } else if (moveVector.x > 0)
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }
}
