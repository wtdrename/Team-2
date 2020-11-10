using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    #region References

    private Item_SO HeadItemEquiped;
    private Item_SO ChestItemEquiped;
    private Item_SO TorsoItemEquiped;
    private Item_SO LegsItemEquiped;
    private Item_SO HandsItemEquiped;
    private Item_SO PistolEquiped;
    private Item_SO RifleEquiped;

    public GameObject headSlotImage;
    public GameObject ChestSlotImage;
    public GameObject TorsoSlotImage;
    public GameObject LegsSlotImage;
    public GameObject HandSlotImage;
    public GameObject PistolSlotImage;
    public GameObject RifleSlotImage;

    public GameObject InventoryUiPanel;

    public Item_SO UiTestSimulationItemInsert;

    #endregion


    private void Start()
    {
        InventoryUiPanel.SetActive(false);
        //  DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (UiTestSimulationItemInsert)
        {
            if (UiTestSimulationItemInsert.itemType == ItemType.ARMOR)
                EquipNewArmour(UiTestSimulationItemInsert);
            if (UiTestSimulationItemInsert.itemType == ItemType.WEAPON)
                EquipNewWeapon(UiTestSimulationItemInsert);
            UiTestSimulationItemInsert = null;
        }
    }

    public void EquipNewArmour(Item_SO item_so)
    {       
        IncreaseArmourStat(item_so.armourAmount);


        switch (item_so.armourType)
        {
            case ArmourType.HEAD_ITEM:

                HeadItemEquiped = item_so;
                headSlotImage.GetComponent<Image>().sprite = HeadItemEquiped.itemSprite;

                break;

            case ArmourType.CHEST_ITEM:
                ChestItemEquiped = item_so;
                ChestSlotImage.GetComponent<Image>().sprite = ChestItemEquiped.itemSprite;
                break;

            case ArmourType.TORSO_ITEM://I believe that this item is irrelevant because torso and chest is the same body part

                TorsoItemEquiped = item_so;
                TorsoSlotImage.GetComponent<Image>().sprite = TorsoItemEquiped.itemSprite;
                break;

            case ArmourType.LEG_ITEM:

                LegsItemEquiped = item_so;
                LegsSlotImage.GetComponent<Image>().sprite = LegsItemEquiped.itemSprite;
                break;

            case ArmourType.HAND_ITEM:

                HandsItemEquiped = item_so;
                HandSlotImage.GetComponent<Image>().sprite = HandsItemEquiped.itemSprite;
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
            case WeaponType.PISTOL:

                PistolEquiped = item_so;
                PistolSlotImage.GetComponent<Image>().sprite = PistolEquiped.itemSprite;
                break;

            case WeaponType.RIFLE:

                RifleEquiped = item_so;
                RifleSlotImage.GetComponent<Image>().sprite = RifleEquiped.itemSprite;
                break;
        }
    }

    #region GearRemoval

    public void RemoveHeadGear()
    {
        if (HeadItemEquiped)
        {
            DecreaseArmourStat(HeadItemEquiped.armourAmount);
            headSlotImage.GetComponent<Image>().sprite = null;
            HeadItemEquiped = null;
        }
    }

    public void RemoveChestGear()
    {
        if (ChestItemEquiped)
        {
            DecreaseArmourStat(ChestItemEquiped.armourAmount);
            ChestSlotImage.GetComponent<Image>().sprite = null;
            ChestItemEquiped = null;
        }
    }

    public void RemoveTorsoGear()
    {
        if (TorsoItemEquiped)
        {
            DecreaseArmourStat(TorsoItemEquiped.armourAmount);
            TorsoSlotImage.GetComponent<Image>().sprite = null;
            TorsoItemEquiped = null;
        }
    }

    public void RemoveLegGear()
    {
        if (LegsItemEquiped)
        {
            DecreaseArmourStat(LegsItemEquiped.armourAmount);
            LegsSlotImage.GetComponent<Image>().sprite = null;
            LegsItemEquiped = null;
        }
    }

    public void RemoveHandGear()
    {
        if (HandsItemEquiped)
        {
            DecreaseArmourStat(HandsItemEquiped.armourAmount);
            HandSlotImage.GetComponent<Image>().sprite = null;
            HandsItemEquiped = null;
        }
    }

    public void RemovePistolGear()
    {
        if (PistolEquiped)
        {
            DecreaseDamageStat(PistolEquiped.weaponDamage);
            PistolSlotImage.GetComponent<Image>().sprite = null;
            PistolEquiped = null;
        }
    }

    public void RemoveRifleGear()
    {
        if (RifleEquiped)
        {
            DecreaseDamageStat(RifleEquiped.weaponDamage);
            RifleSlotImage.GetComponent<Image>().sprite = null;
            RifleEquiped = null;
        }
    }

    #endregion GearRemoval

    #region StatsChangers

    public void IncreaseDamageStat(int amount)
    {
        GameManager.Instance.stats.IncreaseDamage(amount);
    }

    public void DecreaseDamageStat(int amount)
    {
        GameManager.Instance.stats.DecreaseDamage(amount);
    }

    public void IncreaseArmourStat(int amount)
    {
        GameManager.Instance.stats.IncreaseArmour(amount);
    }

    public void DecreaseArmourStat(int amount)
    {
        GameManager.Instance.stats.DecreaseArmour(amount);
    }

    #endregion StatsChangers

    public void OpenCloseUiPanel()
    {
        if (InventoryUiPanel.activeInHierarchy)
            InventoryUiPanel.SetActive(false);
        else
            InventoryUiPanel.SetActive(true);
    }
}