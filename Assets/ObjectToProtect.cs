using UnityEngine;

public class ObjectToProtect : Entity
{
    [SerializeField] private Transform player;
    protected override void Awake()
    {
        base.Awake();

    }
    protected override void Update()
    {
        HandleAnimations();
    }

    protected override void HandleAnimations()
    {
        if (player == null)
            return;
        // sprite flip
        if (player.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else if (player.transform.position.x < transform.position.y)
            transform.localScale = new Vector3(-1, 1, 1);
        //I just flip visual on the x axis if my speed is a negative
    }
    protected override void Die()
    {
        base.Die();
        UI.instance.EnableGameOverUI();
    }
}
