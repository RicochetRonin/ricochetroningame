using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class MasterPool
{
    private static Dictionary<string, BulletPool> bulletPool = new Dictionary<string, BulletPool>();
    private static Dictionary<string, BulletVFXPool> bulletVFXPool = new Dictionary<string, BulletVFXPool>();


    public static void SpawnBullet(GameObject bullet, Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        string key = bullet.name.Replace("(Clone)", "");
        if (GameObject.Find($"{key}_POOL") == null)
        {
            bulletPool.Clear();
        }
        if (bulletPool.ContainsKey(key))
        {
            if (bulletPool[key].inactive.Count == 0)
            {
                Object.Instantiate(bullet, pos, rot, bulletPool[key].parent.transform);

            }
            else
            {
                obj = bulletPool[key].inactive.Pop();


                obj.GetComponent<BulletController>().PoolSpawn(pos, rot);


            }
        }
        else
        {
            GameObject newParent = new GameObject($"{key}_POOL");

            Object.Instantiate(bullet, pos, rot, newParent.transform);
            BulletPool newPool = new BulletPool(newParent);
            bulletPool.Add(key, newPool);
        }
    }

    public static void DespawnBullet(GameObject bullet)
    {
        string key = bullet.name.Replace("(Clone)", "");

        if (bulletPool.ContainsKey(key))
        {
            bulletPool[key].inactive.Push(bullet);
            bullet.transform.position = bulletPool[key].parent.transform.position;
            bullet.SetActive(false);
        }
        else
        {
            GameObject newParent = new GameObject($"{key}_POOL");
            BulletPool newPool = new BulletPool(newParent);

            bullet.transform.SetParent(newParent.transform);

            bulletPool.Add(key, newPool);
            bulletPool[key].inactive.Push(bullet);
            bullet.SetActive(false);
        }
    }


    public static void SpawnBulletVFX(GameObject vfx, Vector3 pos, Quaternion rot, string animation)
    {
        GameObject bulletVFXref;
        string key = vfx.name.Replace("(Clone)", "");
        if (GameObject.Find($"{key}_POOL") == null)
        {
            bulletVFXPool.Clear();
        }
        if (bulletVFXPool.ContainsKey(key))
        {
            if (bulletVFXPool[key].inactive.Count == 0)
            {
                Object.Instantiate(vfx, pos, rot, bulletVFXPool[key].parent.transform).GetComponentInChildren<BulletVFXController>().PlayAnimation(animation);

            }
            else
            {
                bulletVFXref = bulletVFXPool[key].inactive.Pop();

                bulletVFXref.transform.position = pos;
                bulletVFXref.transform.rotation = rot;
                bulletVFXref.SetActive(true);
                bulletVFXref.GetComponentInChildren<BulletVFXController>().PlayAnimation(animation);


            }
        }
        else
        {
            GameObject newParent = new GameObject($"{key}_POOL");

            Object.Instantiate(vfx, pos, rot, newParent.transform).GetComponentInChildren<BulletVFXController>().PlayAnimation(animation);
            BulletVFXPool newPool = new BulletVFXPool(newParent);
            bulletVFXPool.Add(key, newPool);
        }
    }

    public static void DespawnBulletVFX(GameObject vfx)
    {
        string key = vfx.name.Replace("(Clone)", "");

        if (bulletVFXPool.ContainsKey(key))
        {
            bulletVFXPool[key].inactive.Push(vfx);
            vfx.transform.position = bulletVFXPool[key].parent.transform.position;
            vfx.SetActive(false);
        }
        else
        {
            GameObject newParent = new GameObject($"{key}_POOL");
            BulletVFXPool newPool = new BulletVFXPool(newParent);

            vfx.transform.SetParent(newParent.transform);

            bulletVFXPool.Add(key, newPool);
            bulletVFXPool[key].inactive.Push(vfx);
            vfx.SetActive(false);
        }
    }
}
