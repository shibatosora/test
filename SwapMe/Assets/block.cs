using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    public float speed = 10f; // 敵の速度
    private Transform player; // プレイヤーの位置を参照するためのTransform
    private GameManager gameManager;

    void Start()
    {
        // プレイヤーの位置を取得
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // GameManagerのインスタンスを取得
        gameManager = GameManager.instance;

    }

    void Update()
    {
        //if (player != null && !GameManager.instance.stop)
        //{
        //    // プレイヤーの位置へ向かってまっすぐ突進
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
