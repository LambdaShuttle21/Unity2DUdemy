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

    public override void HandleAnimations()
    {// game objects gets crushed (Destroy(player)) so there it is a ghost reference
     // that means that it will throw an exception, you avoid that with player.gameObject == null
        if (player == null && player.gameObject == null)
            return;
        // sprite flip
        if (player.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else if (player.transform.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        //I just flip visual on the x axis if my speed is a negative
    }
    protected override void Die()
    {
        base.Die();
        UI.instance.EnableGameOverUI();
    }
}
