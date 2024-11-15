using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public int upgradeGold = 2;
    [SerializeField]
    private Gold playerGold;
    private int upgradeCount = 0;
    public void Upgrade(int upgradeAmount) {
        if(upgradeGold > playerGold.CurrentGold) {
            return;
        }
        if(upgradeCount < 5) {
            TowerWeapon.upgradeBonus += upgradeAmount;
            upgradeCount++;
        }
        else if(upgradeCount < 10) {
            TowerWeapon.upgradeBonus += upgradeAmount + 2;
            upgradeCount++;
        }
        else if(upgradeCount < 20) {
            TowerWeapon.upgradeBonus += upgradeAmount + 6;
            upgradeCount++;
        }
        else {
            TowerWeapon.upgradeBonus += upgradeAmount + 10;
        }        
        
        playerGold.CurrentGold -= upgradeGold;
        upgradeGold += 4;
    }
}
