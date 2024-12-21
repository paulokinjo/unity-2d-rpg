using UnityEngine;

public static class PlayerExtensions
{
    public static void SetVelocity(this Rigidbody2D rigidbody, float x, float y)
    {
        rigidbody.velocity = new Vector2(x, y);
    }
}
