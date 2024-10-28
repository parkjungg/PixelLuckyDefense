using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public int upgradeGold = 2;
    [SerializeField]
    private Gold playerGold;
    public void Upgrade(int upgradeAmount) {
        if(upgradeGold > playerGold.CurrentGold) {
            return;
        }
        TowerWeapon.upgradeBonus += upgradeAmount;
        playerGold.CurrentGold -= upgradeGold;
        upgradeGold += 4;
    }
}
