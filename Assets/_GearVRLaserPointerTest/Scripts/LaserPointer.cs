using UnityEngine;

/// <summary>
/// LineRenderer によるレーザーポインタ
/// 2017/8/21 Fantom (Unity 5.6.2p3)
/// http://fantom1x.blog130.fc2.com/blog-entry-259.html
///（使い方）
///・空の GameObject にアタッチし、LineRenderer を追加する（線の幅[Width:0.005くらい]やマテリアル・色も設定する）。
///※LineRenderer のマテリアルを「Sprites-Default」などアルファが利くものにし、色のアルファを設定すれば半透明レーザーにできる。
///※Use World Space は true である必要がある（デフォルト値）。
///・インスペクタで Anchor にコントローラなどの GameObject をセットする。
///・認識させるオブジェクトにコライダを追加する。
///（仕様説明）
///・anchor → target へ LineRenderer で線（レーザー）を引く。
///・LineRenderer は厚さのないテープのようなものなので（ただし常にカメラの方を向いている）、カメラ位置とanchorが同じで、targetが真正面方向だと見えない（故にanchorのY軸をずらせば見える）。
///・外部の Raycaster で判定をするときは shotRay をオフにして、target に指す位置を入れる。
///・shotRay がオンのときは anchor 位置から anchor.forward 方向へ Ray を飛ばし、ヒットしたオブジェクトの transform が target に入る（ヒットが無いときは null）。ヒットした位置ではないことに注意（ヒットした位置は RaycastHit.point で取得できる）。
/// </summary>
public class LaserPointer : MonoBehaviour
{
    public bool IsActive = true;            //稼働フラグ
    public float defaultLength = 0.5f;      //ヒットなしのときのレーザーの長さ

    public bool shotRay = true;             //Ray を撃つ(false のときは target に指す位置を入れる)
    public float rayLength = 500f;          //Ray の長さ
    public LayerMask rayExclusionLayers;    //Ray 判定を除外するレイヤー

    public Transform target;                //指す位置（shotRay=true のときはヒットしたオブジェクトの transform が入る）
    public Transform anchor;                //発射位置（コントローラの位置）
    public LineRenderer lineRenderer;       //レーザーを描画するラインレンダラ

    // Use this for initialization
    void Awake()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsActive)
        {
            lineRenderer.enabled = false;
            return;
        }

        if (shotRay)
        {
            Ray ray = new Ray(anchor.position, anchor.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayLength, ~rayExclusionLayers))
            {
                target = hit.transform;
                DrawTo(hit.point);     //ヒットした位置にしたいため
                return;
            }

            target = null;
        }

        if (target != null)
            DrawTo(target.position);
        else
            DrawTo(anchor.position + anchor.forward * defaultLength);   //コントローラの正面方向へ一定の長さ
    }

    //レーザーを描く
    void DrawTo(Vector3 pos)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, anchor.position);
        lineRenderer.SetPosition(1, pos);
    }
}
