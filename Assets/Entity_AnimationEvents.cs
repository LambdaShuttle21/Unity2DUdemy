using UnityEngine;

public class Entity_AnimationEvents : MonoBehaviour
{
    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    public void AE_DamageTargets() => entity.DamageTargets();
    public void AE_DisableMovementAndJump() => entity.EnableMovement(false);
    

    public void AE_EnableMovementAndJump() => entity.EnableMovement(true);

}
