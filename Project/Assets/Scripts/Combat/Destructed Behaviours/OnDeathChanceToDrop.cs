using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathChanceToDrop : MonoBehaviour, IDestructable
{
    // Fill them in Unity editor with prefabs of corresponding items
    public List<GameObject> AmmoDrop;
    public List<GameObject> MedkitDrop;
    public List<GameObject> WeaponDrop;

    public void OnDestruct(GameObject destroyer)
    {
        int dropChance = UnityEngine.Random.Range(0, 100);

        if (dropChance > 90)
        {
            Instantiate(WeaponDrop[0], gameObject.transform.position + new Vector3(0, WeaponDrop[0].transform.position.y/2), Quaternion.identity);
        }
        else if (dropChance > 75)
        {
            Instantiate(MedkitDrop[0], gameObject.transform.position + new Vector3(0, WeaponDrop[0].transform.position.y / 2), Quaternion.identity);
        }
        else if (dropChance > 50)
        {
            Instantiate(AmmoDrop[0], gameObject.transform.position + new Vector3(0, WeaponDrop[0].transform.position.y / 2), Quaternion.identity);
        }
    }
}
