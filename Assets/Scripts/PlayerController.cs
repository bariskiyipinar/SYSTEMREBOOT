using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Animator anim;
    private bool facingLeft = false;
    public bool FacingLeft => facingLeft;   /* bu ayný zamanda bu iþe yarar 
                                             public bool FacingLeft
                                              {
                                                   get { return facingLeft} */
                                                
    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 movement = new Vector3(moveX, moveY, 0f);
        transform.position += movement;

        bool isWalking = moveX != 0f || moveY != 0f;
        anim.SetBool("IsWalking", isWalking);


        if (moveX < 0f)
        {
            facingLeft = true;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (moveX > 0f)
        {
            facingLeft = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }


    }
}
