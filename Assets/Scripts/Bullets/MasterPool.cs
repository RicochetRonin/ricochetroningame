using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MasterPool
{
    private static Dictionary<string, BulletPool> pools = new Dictionary<string, BulletPool>();

        public static void Spawn(GameObject bullet, Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        string key = bullet.name.Replace("(Clone)", "");
        if (GameObject.Find($"{key}_POOL") == null)
        {
            pools.Clear();
        }
        if (pools.ContainsKey(key))
        {
            if (pools[key].inactive.Count == 0)
            {
                Object.Instantiate(bullet, pos, rot, pools[key].parent.transform);
            }
            else
            {
                obj = pools[key].inactive.Pop();
                
                obj.GetComponent<BulletController>().SetHostile();
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.SetActive(true);
                
            }
        }
        else
        {
            GameObject newParent = new GameObject($"{key}_POOL");

            Object.Instantiate(bullet, pos, rot, newParent.transform);
            BulletPool newPool = new BulletPool(newParent);
            pools.Add(key, newPool);
        }
    }

    public static void Despawn(GameObject bullet)
    {
        string key = bullet.name.Replace("(Clone)", "");

        if (pools.ContainsKey(key))
        {
            pools[key].inactive.Push(bullet);
            bullet.transform.position = pools[key].parent.transform.position;
            bullet.SetActive(false);
        }
        else
        {
            GameObject newParent = new GameObject($"{key}_POOL");
            BulletPool newPool = new BulletPool(newParent);

            bullet.transform.SetParent(newParent.transform);

            pools.Add(key, newPool);
            pools[key].inactive.Push(bullet);
            bullet.SetActive(false);
        }
    }
}
