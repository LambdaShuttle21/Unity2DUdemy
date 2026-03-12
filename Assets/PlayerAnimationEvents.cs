using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void AE_DamageEnemies() => player.DamageEnemies();
    public void AE_DisableMovementAndJump() => player.EnableMovementAndJump(false);
    

    public void AE_EnableMovementAndJump() => player.EnableMovementAndJump(true);

}
