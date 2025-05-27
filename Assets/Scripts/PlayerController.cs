using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool facingLeft = false;
    public bool FacingLeft => facingLeft;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 movement = new Vector3(moveX, moveY, 0);
        transform.position += movement;

        // Hareket ediyor mu?
        bool isWalking = moveX != 0f || moveY != 0f;
        anim.SetBool("IsWalking", isWalking);

        // Yönü ayarla
        if (moveX < 0f)
        {
            facingLeft = true;
            spriteRenderer.flipX = true;
        }
        else if (moveX > 0f)
        {
            facingLeft = false;
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = facingLeft; // idle yönünü koru
        }
    }
}
