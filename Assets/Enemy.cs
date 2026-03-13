using UnityEngine;

public class Enemy : Entity
{
    protected override void Update()
    {
        HandleCollision();
        HandleAnimations();
        HandleMovement();
    }
    protected override void HandleMovement()
    {
        if (canMove == true)//this cames from PlayerAnimationEvents -> AE_DisableMovementAndJump
        {
            rb.linearVelocity = new Vector2(facingDir * moveSpeed, rb.linearVelocityY);//(x,y)
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);//(x,y)
        }
    }

    //[SerializeField] protected float moveSpeed;// Protected: Only child components will be able to use
    //[SerializeField] protected string enemyName;// Encapsulation: Getting properties locked only to his
    //// childs by making them usable with a public method

    //private void Update()
    //{
    //   // MoveAround();

    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        Attack();
    //    }
    //}
    //private void MoveAround()
    //{
    //    Debug.Log(enemyName + " moves at speed " + moveSpeed);
    //}
    //// virtual: it allows overriding methods in childs
    //protected virtual void Attack()
    //{
    //    Debug.Log(enemyName + "attacks!");
    //}
    //public void TakeDamage()
    //{

    //}
    //public string getEnemyName()
    //{
    //    return enemyName;
    //}
}
