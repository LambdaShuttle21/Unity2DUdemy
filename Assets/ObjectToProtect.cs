using UnityEngine;

public class ObjectToProtect : Entity
{
    [Header("Extra details")]
    [SerializeField] private Transform player;
    protected override void Update()
    {
        HandleAnimations();
    }

    protected override void HandleAnimations()
    {
        // sprite flip
        if (player.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else if (player.transform.position.x < transform.position.y)
            transform.localScale = new Vector3(-1, 1, 1);
        //I just flip visual on the x axis if my speed is a negative
    }
}
