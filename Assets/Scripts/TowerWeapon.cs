using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.XR;

public enum WeaponState {SearchTarget = 0, AttackToTarget}

public class TowerWeapon : MonoBehaviour
{
    [Header("# Tower Info")]
    [SerializeField]
     private int attackDamage = 1; // 공격력 → 초기에 타워마다 다르게 설정    
    [SerializeField]
    private float attackRange = 2.0f; // 공격 범위
    [SerializeField]
    private float attackRate = 0.5f; // 공격 속도
    [SerializeField]
    private int sellGold = 10;
   
    [Header("# Game Component")]
    [SerializeField]
    private GameObject weaponPrefab; // 무기 프리팹
    [SerializeField]
    private RectTransform spawnPoint; // 무기 발사 생성 위치
    
    public static int upgradeBonus; // 업그레이드 → 모든 타워에 동일하게 적용
    private WeaponState weaponState = WeaponState.SearchTarget; // 타워 무기의 상태
    private Transform attackTarget = null; // 공격 대상
    private EnemySpwaner enemySpawner; // 게임에 존재하는 적 정보 획득하기 위한 선언
    private AudioSource audioSource;
    public float volume = 0.15f;
    private Tile ownerTile;
    [SerializeField]
    private Gold playerGold;
    [SerializeField]
    Animator anim;
    [SerializeField]
    private Sprite towerImage;
    public int TotalAttackDamage => attackDamage + upgradeBonus;
    public float Damage => TotalAttackDamage;
    public float Rate => attackRate;
    public float Range => attackRange;
    public int SellGold => sellGold;
    public Sprite TowerImage => towerImage;  
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }  
    public void Setup(EnemySpwaner enemySpwaner,Gold playerGold, Tile ownerTile) {
        this.enemySpawner = enemySpwaner;
        this.playerGold = playerGold;
        this.ownerTile = ownerTile;
        ChangeState(WeaponState.SearchTarget); // 최초 상태를 설정
    }
    public void ChangeState(WeaponState newState) {
        StopCoroutine(weaponState.ToString());
        weaponState = newState;
        StartCoroutine(weaponState.ToString());
    }
    private IEnumerator SearchTarget() {
        while(true) {
            float closesDistSqr = Mathf.Infinity; // 제일 가까이 있는 적을 찾기 위해 최초 거리를 최대한 크게 설정
            for(int i = 0; i < enemySpawner.EnemyList.Count; i++) { // EnemySpawner의 EnemyList에 있는 현재 맵에 존재하는 모든 적 검사
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                // 현재 검사중인 적과의 거리가 공격 범위 내에 있고, 현재까지 검사한 적보다 거리가 가까우면 실행
                if(distance <= attackRange && distance <= closesDistSqr) {
                    closesDistSqr = distance;
                    attackTarget = enemySpawner.EnemyList[i].transform;
                }
            }
            if(attackTarget != null) {
                ChangeState(WeaponState.AttackToTarget);
            }
            else {
                anim.SetTrigger("NotAttack");
            }
            yield return null;
        }
    }
    private IEnumerator AttackToTarget() {
        while(true) {
            // 적이 있는지 검사
            if(attackTarget == null) {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            // 적이 공격 범위 안에 있는지 검사(공격 범위를 벗어나면 새로운 적 탐색)
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if(distance > attackRange) {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            anim.SetTrigger("2_Attack");
            audioSource.volume = volume;
            audioSource.Play();
            yield return new WaitForSeconds(0.05f); // 애니메이션 싱크를 위한 지연
            yield return new WaitForSeconds(attackRate);   

            Attack();        
        }
    }
        private void Attack() {            
            GameObject clone = Instantiate(weaponPrefab, spawnPoint.position, Quaternion.identity);
            clone.GetComponent<Weapon>().Setup(attackTarget, TotalAttackDamage);            
        }
        public void Sell() {
            playerGold.CurrentGold += sellGold;
            ownerTile.isBuildTower = false;
            Destroy(gameObject);
        }
}
