using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage;

	void OnCollisionEnter(Collision c)
    {
        GameObject hit = c.gameObject;
        Player player = hit.GetComponent<Player>();
        if(player != null)
        {
            player.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
