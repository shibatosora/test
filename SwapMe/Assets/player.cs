using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class player : MonoBehaviour
{
    public float speed;   // 速度
    public float jumpForce;
    public Transform feetPos;
    public float checkRedius;
    public LayerMask whatIsGround;
    public float jumpTime;
    public GameObject cursorPrefab;//カーソルプレハブ

    private Rigidbody2D rb = null;
    private float moveInput;
    private bool isGrounded;
    private float jumpTimeCounter;
    private bool isJumping;
    private bool isShiftPressed; // Shift キーが押されたかどうかのフラグ
    private GameObject cursor; // カーソルのインスタンスを格納する変数
    private GameObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isShiftPressed) // Shift キーが押されていない場合のみ移動を許可
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRedius, whatIsGround);

        // Shift キーの状態を確認
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isShiftPressed = !isShiftPressed; // Shift キーの状態を切り替える
            GameManager.instance.stop = true;
            if (isShiftPressed) // Shift キーが押された場合、カーソルを生成する
            {
                cursor = Instantiate(cursorPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            }
            else // Shift キーが離された場合、カーソルを破棄する
            {
                Destroy(cursor);
            }
        }

        //向き調整
        if (!isShiftPressed) // Shift キーが押されていない場合のみ向き調整を許可
        {
            if (moveInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (moveInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

        //jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !isShiftPressed) // Shift キーが押されていない場合のみジャンプを許可
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping && !isShiftPressed) // Shift キーが押されていない場合のみジャンプを継続
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        // Shift キーが離されたらフラグをリセット
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isShiftPressed = false;
            GameManager.instance.stop=false;
            if (cursor != null) // Shift キーが離された場合、カーソルを破棄する
            {
                Destroy(cursor);
            }
        }
    }
}