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
    private GameObject player; // プレイヤーオブジェクトの参照を保持する変数
    private bool cs = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player"); // プレイヤーオブジェクトのタグを使って検索し、参照を取得
    }

    void Update()
    {
        cs = Physics2D.OverlapCircle(cursorPos.position, checkRedius, whatIsblock);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (cs && Input.GetMouseButtonDown(0)) // マウスの左クリックが押されたら
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // クリックした位置をワールド座標に変換
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero); // クリックした位置からレイを飛ばしてオブジェクトを検出

            if (hit.collider != null && hit.collider.gameObject != player) // クリックした場所にオブジェクトがあり、かつプレイヤーではない場合
            {
                Vector2 tempPosition = player.transform.position; // プレイヤーの現在位置を一時変数に保存
                player.transform.position = hit.collider.gameObject.transform.position; // プレイヤーの位置をクリックしたオブジェクトの位置に変更
                hit.collider.gameObject.transform.position = tempPosition; // クリックしたオブジェクトの位置を一時保存しておいたプレイヤーの位置に変更
            }
        }
    }
}
