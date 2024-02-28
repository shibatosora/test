using System.Collections;
using UnityEngine;

using UnityEngine.UI;
public class enemy : MonoBehaviour
{
    public GameObject blockPrefab; // ブロックのプレハブ
    public float blockSpeed = 5f; // ブロックの速度
    public float fireRate = 2f; // ブロックを発射する頻度（秒）
    public Slider slider;

    private Transform player; // プレイヤーのTransform
    private Vector2 targetDirection; // プレイヤーの方向
    private SpriteRenderer spriteRenderer; // ボスのSprite Renderer
    int maxHp = 155;
    int currentHp;
    private GameManager gameManager;
    private bool isPaused = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FireBlocks());
        //Sliderを満タンにする。
        slider.value = 1;
        //現在のHPを最大HPと同じに。
        currentHp = maxHp;
        Debug.Log("Start currentHp : " + currentHp);
        gameManager = GameManager.instance;
    }


    private void Update()
    {
        if (GameManager.instance.stop && !isPaused)
        {
            isPaused = true;
            StopAllCoroutines(); // 動きを停止
        }
        else if (!GameManager.instance.stop && isPaused)
        {
            isPaused = false;
            StartCoroutine(FireBlocks()); // 動きを再開
        }
    }

    private IEnumerator FireBlocks()
    {
        while (true)
        {
            if (!isPaused) // 停止していない場合のみ処理を継続
            {
                // プレイヤーの方向を計算
                targetDirection = (player.position - transform.position).normalized;

                // ボスの方向を設定
                if (targetDirection.x < 0)
                {
                    spriteRenderer.flipX = true; // プレイヤーの左側にいる場合、ボスを反転する
                }
                else
                {
                    spriteRenderer.flipX = false; // プレイヤーの右側にいる場合、ボスを反転しない
                }

                // ブロックを生成して発射
                GameObject block = Instantiate(blockPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
                Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
                rb.velocity = targetDirection * blockSpeed;

                // 次の発射まで待機
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
        //Enemyタグのオブジェクトに触れると発動
        if (collider.gameObject.tag == "block")
        {
            //ダメージは1～100の中でランダムに決める。
            int damage = Random.Range(1, 100);
            Debug.Log("damage : " + damage);

            //現在のHPからダメージを引く
            currentHp = currentHp - damage;
            Debug.Log("After currentHp : " + currentHp);

            slider.value = (float)currentHp / (float)maxHp; ;
            Debug.Log("slider.value : " + slider.value);
        }
    }
}
