using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Entity player;

    private void Awake()
    {
        player = GetComponent<Entity>();
    }

    public void AE_DamageEnemies() => player.DamageTargets();
    public void AE_DisableMovementAndJump() => player.EnableMovementAndJump(false);
    

    public void AE_EnableMovementAndJump() => player.EnableMovementAndJump(true);

}
