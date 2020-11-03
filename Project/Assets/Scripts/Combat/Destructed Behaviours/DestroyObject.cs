using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class DestroyObject : MonoBehaviour, IDestructable
{
    public EnemyController animator;

    private void Start()
    {
        animator = GetComponent<EnemyController>();
    }

    public void OnDestruct(GameObject destroyer)
    {
        if(animator != null)
        {
            animator.Dying();
            Destroy(gameObject, 10f);
        }
    }
}
