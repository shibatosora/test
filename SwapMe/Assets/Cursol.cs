using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursol : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform cursorPos;
    public float checkRedius;
    public LayerMask whatIsblock;

    private Rigidbody2D rb;
    private GameObject player; // �v���C���[�I�u�W�F�N�g�̎Q�Ƃ�ێ�����ϐ�
    private bool cs = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player"); // �v���C���[�I�u�W�F�N�g�̃^�O���g���Č������A�Q�Ƃ��擾
    }

    void Update()
    {
        cs = Physics2D.OverlapCircle(cursorPos.position, checkRedius, whatIsblock);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (cs && Input.GetMouseButtonDown(0)) // �}�E�X�̍��N���b�N�������ꂽ��
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // �N���b�N�����ʒu�����[���h���W�ɕϊ�
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero); // �N���b�N�����ʒu���烌�C���΂��ăI�u�W�F�N�g�����o

            if (hit.collider != null && hit.collider.gameObject != player) // �N���b�N�����ꏊ�ɃI�u�W�F�N�g������A���v���C���[�ł͂Ȃ��ꍇ
            {
                Vector2 tempPosition = player.transform.position; // �v���C���[�̌��݈ʒu���ꎞ�ϐ��ɕۑ�
                player.transform.position = hit.collider.gameObject.transform.position; // �v���C���[�̈ʒu���N���b�N�����I�u�W�F�N�g�̈ʒu�ɕύX
                hit.collider.gameObject.transform.position = tempPosition; // �N���b�N�����I�u�W�F�N�g�̈ʒu���ꎞ�ۑ����Ă������v���C���[�̈ʒu�ɕύX
            }
        }
    }
}
