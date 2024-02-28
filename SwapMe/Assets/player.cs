using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class player : MonoBehaviour
{
    public float speed;   // ���x
    public float jumpForce;
    public Transform feetPos;
    public float checkRedius;
    public LayerMask whatIsGround;
    public float jumpTime;
    public GameObject cursorPrefab;//�J�[�\���v���n�u

    private Rigidbody2D rb = null;
    private float moveInput;
    private bool isGrounded;
    private float jumpTimeCounter;
    private bool isJumping;
    private bool isShiftPressed; // Shift �L�[�������ꂽ���ǂ����̃t���O
    private GameObject cursor; // �J�[�\���̃C���X�^���X���i�[����ϐ�
    private GameObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isShiftPressed) // Shift �L�[��������Ă��Ȃ��ꍇ�݈̂ړ�������
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRedius, whatIsGround);

        // Shift �L�[�̏�Ԃ��m�F
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isShiftPressed = !isShiftPressed; // Shift �L�[�̏�Ԃ�؂�ւ���
            GameManager.instance.stop = true;
            if (isShiftPressed) // Shift �L�[�������ꂽ�ꍇ�A�J�[�\���𐶐�����
            {
                cursor = Instantiate(cursorPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            }
            else // Shift �L�[�������ꂽ�ꍇ�A�J�[�\����j������
            {
                Destroy(cursor);
            }
        }

        //��������
        if (!isShiftPressed) // Shift �L�[��������Ă��Ȃ��ꍇ�̂݌�������������
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
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !isShiftPressed) // Shift �L�[��������Ă��Ȃ��ꍇ�̂݃W�����v������
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping && !isShiftPressed) // Shift �L�[��������Ă��Ȃ��ꍇ�̂݃W�����v���p��
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

        // Shift �L�[�������ꂽ��t���O�����Z�b�g
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isShiftPressed = false;
            GameManager.instance.stop=false;
            if (cursor != null) // Shift �L�[�������ꂽ�ꍇ�A�J�[�\����j������
            {
                Destroy(cursor);
            }
        }
    }
}