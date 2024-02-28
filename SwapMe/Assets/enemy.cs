using System.Collections;
using UnityEngine;

using UnityEngine.UI;
public class enemy : MonoBehaviour
{
    public GameObject blockPrefab; // �u���b�N�̃v���n�u
    public float blockSpeed = 5f; // �u���b�N�̑��x
    public float fireRate = 2f; // �u���b�N�𔭎˂���p�x�i�b�j
    public Slider slider;

    private Transform player; // �v���C���[��Transform
    private Vector2 targetDirection; // �v���C���[�̕���
    private SpriteRenderer spriteRenderer; // �{�X��Sprite Renderer
    int maxHp = 155;
    int currentHp;
    private GameManager gameManager;
    private bool isPaused = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FireBlocks());
        //Slider�𖞃^���ɂ���B
        slider.value = 1;
        //���݂�HP���ő�HP�Ɠ����ɁB
        currentHp = maxHp;
        Debug.Log("Start currentHp : " + currentHp);
        gameManager = GameManager.instance;
    }


    private void Update()
    {
        if (GameManager.instance.stop && !isPaused)
        {
            isPaused = true;
            StopAllCoroutines(); // �������~
        }
        else if (!GameManager.instance.stop && isPaused)
        {
            isPaused = false;
            StartCoroutine(FireBlocks()); // �������ĊJ
        }
    }

    private IEnumerator FireBlocks()
    {
        while (true)
        {
            if (!isPaused) // ��~���Ă��Ȃ��ꍇ�̂ݏ������p��
            {
                // �v���C���[�̕������v�Z
                targetDirection = (player.position - transform.position).normalized;

                // �{�X�̕�����ݒ�
                if (targetDirection.x < 0)
                {
                    spriteRenderer.flipX = true; // �v���C���[�̍����ɂ���ꍇ�A�{�X�𔽓]����
                }
                else
                {
                    spriteRenderer.flipX = false; // �v���C���[�̉E���ɂ���ꍇ�A�{�X�𔽓]���Ȃ�
                }

                // �u���b�N�𐶐����Ĕ���
                GameObject block = Instantiate(blockPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
                Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
                rb.velocity = targetDirection * blockSpeed;

                // ���̔��˂܂őҋ@
                yield return new WaitForSeconds(1f / fireRate);
            }
            else
            {
                yield return null;
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        //Enemy�^�O�̃I�u�W�F�N�g�ɐG���Ɣ���
        if (collider.gameObject.tag == "block")
        {
            //�_���[�W��1�`100�̒��Ń����_���Ɍ��߂�B
            int damage = Random.Range(1, 100);
            Debug.Log("damage : " + damage);

            //���݂�HP����_���[�W������
            currentHp = currentHp - damage;
            Debug.Log("After currentHp : " + currentHp);

            slider.value = (float)currentHp / (float)maxHp; ;
            Debug.Log("slider.value : " + slider.value);
        }
    }
}
