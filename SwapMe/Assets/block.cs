using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    public float speed = 10f; // �G�̑��x
    private Transform player; // �v���C���[�̈ʒu���Q�Ƃ��邽�߂�Transform
    private GameManager gameManager;

    void Start()
    {
        // �v���C���[�̈ʒu���擾
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // GameManager�̃C���X�^���X���擾
        gameManager = GameManager.instance;

    }

    void Update()
    {
        //if (player != null && !GameManager.instance.stop)
        //{
        //    // �v���C���[�̈ʒu�֌������Ă܂������ːi
        //    Vector2 direction = player.position - transform.position;
        //    direction.Normalize();
        //    transform.Translate(direction * speed * Time.deltaTime);
        //}
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }

    }

}
