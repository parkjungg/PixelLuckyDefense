using UnityEngine;

public class Tile : MonoBehaviour
{
    // 중복 타워 건설 방지 스크립트
    public bool isBuildTower { set; get;}
    private void Awake() {
        isBuildTower = false;
    }
}
