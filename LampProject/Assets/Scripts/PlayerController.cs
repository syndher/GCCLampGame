using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;
    private Vector2 _input;

    void Update()
    {
        GatherInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void GatherInput()
    {
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical"); 

        _input = new Vector2(h, v).normalized;
    }

    void Move()
    {
        if (_input != Vector2.zero)
        {
            // 2:1 isometric
            float isoX = _input.x - _input.y;
            float isoY = (_input.x + _input.y) / 2f;

            Vector2 moveDir = new Vector2(isoX, isoY).normalized;

            _rb.MovePosition(_rb.position + moveDir * _speed * Time.fixedDeltaTime);
        }
    }
}
