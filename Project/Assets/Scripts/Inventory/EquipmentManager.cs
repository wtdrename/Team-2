using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Item_SO_Referencers

    public Item_SO HeadItemEquiped;
    public Item_SO ChestItemEquiped;
    public Item_SO TorsoItemEquiped;
    public Item_SO LegsItemEquiped;
    public Item_SO HandsItemEquiped;
    public Item_SO PistolEquiped;
    public Item_SO RifleEquiped;

    #endregion 

    public void EquipNewArmour(Item_SO item_so)
    {
        IncreaseArmourStat(item_so.armourAmount);
        switch (item_so.armourType)
        {
            case Item_SO.ArmourType.HEAD_ITEM:
                if (HeadItemEquiped)
                
                    RemoveHeadGear();
                
                HeadItemEquiped = item_so;
                break;

            case Item_SO.ArmourType.CHEST_ITEM:
                if (ChestItemEquiped)
                
                    RemoveChestGear();
                
                ChestItemEquiped = item_so;
                break;

            case Item_SO.ArmourType.TORSO_ITEM:
                if (TorsoItemEquiped)                
                    RemoveTorsoGear();
                
                TorsoItemEquiped = item_so;
                break;

            case Item_SO.ArmourType.LEG_ITEM:
                if (LegsItemEquiped)                
                    RemoveLegGear();
                
                LegsItemEquiped = item_so;
                break;

            case Item_SO.ArmourType.HAND_ITEM:
                if (HandsItemEquiped)                
                    RemoveHandGear();
                
                HandsItemEquiped = item_so;

                break;

            default:
                print("no other item type found");
                break;
        }
    }
    public void EquipNewWeapon(Item_SO item_so)
    {
        IncreaseDamageStat(item_so.weaponDamage);
        switch (item_so.weaponType)
        {
            case Item_SO.WeaponType.PISTOL:
                if (PistolEquiped)
                    RemovePistolGear();
                break;

            case Item_SO.WeaponType.RIFLE:
                if (RifleEquiped)
                    RemoveRifleGear();
                break;

        }
    }

    #region GearRemoval
    private void RemoveHeadGear()
    {
        DecreaseArmourStat(HeadItemEquiped.armourAmount);
        HeadItemEquiped = null;
    }

    private void RemoveChestGear()
    {
        DecreaseArmourStat(ChestItemEquiped.armourAmount);
        ChestItemEquiped = null;
    }

    private void RemoveTorsoGear()
    {
        DecreaseArmourStat(TorsoItemEquiped.armourAmount);
        TorsoItemEquiped = null;
    }

    private void RemoveLegGear()
    {
        DecreaseArmourStat(LegsItemEquiped.armourAmount);
        LegsItemEquiped = null;
    }

    private void RemoveHandGear()
    {
        DecreaseArmourStat(HandsItemEquiped.armourAmount);
        HandsItemEquiped = null;
    }

    private void RemovePistolGear()
    {
        DecreaseDamageStat(PistolEquiped.weaponDamage);
        PistolEquiped = null;
    }

    private void RemoveRifleGear()
    {
        DecreaseDamageStat(RifleEquiped.weaponDamage);
        RifleEquiped = null;
    }
    #endregion

    #region StatsChangers

    public void IncreaseDamageStat(int amount)
    {
        GameManager.instance.stats.IncreaseDamage(amount);
    }

    public void DecreaseDamageStat(int amount)
    {
        GameManager.instance.stats.DecreaseDamage(amount);
    }


    public void IncreaseArmourStat(int amount)
    {
        GameManager.instance.stats.IncreaseArmour(amount);
    }

    public void DecreaseArmourStat(int amount)
    {
        GameManager.instance.stats.DecreaseArmour(amount);
    }

    #endregion
}