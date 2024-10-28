using UnityEngine;

// 이동이 가능한 모든 오브젝트에게 부착
public class Movement2D : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    public float MoveSpeed => moveSpeed;
    private bool isMove = true;

    private void Awake() {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if(!isMove) return;
        float h = moveDirection.x; // 좌우 이동 방향
        transform.position += moveDirection * moveSpeed * Time.deltaTime; // 적 이동
        if(h < 0) {
            transform.localScale = new Vector3(1.7f,1.7f,1);
        }
        else if(h > 0) {
            transform.localScale = new Vector3(-1.7f,1.7f,1);
        } // 좌우 이동에 따른 적 프리팹 이미지 전환
    }

    // 외부에서 호출하여 이동 방향을 설정
    public void MoveTo(Vector3 direction) { 
        moveDirection = direction;
    }
    public void StopMoving() {
        isMove = false;
    }
}
