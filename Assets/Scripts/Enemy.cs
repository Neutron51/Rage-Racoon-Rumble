using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 100;

    public void Damage(int damage)
    {
        Health -= damage;

        if(Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
