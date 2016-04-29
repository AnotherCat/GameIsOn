using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CmdFire(GetComponent<Player>().damage);
        }
    }

    [Command]
    void CmdFire(int damage)
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6.0f;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 100.0f);
        bullet.GetComponent<Bullet>().damage = damage;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2);
    }
}
