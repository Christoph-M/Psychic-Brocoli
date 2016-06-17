using UnityEngine;
using System.Collections;
using System;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject[] weaponsPrefabs;
    public int maxNumberOfWeapons = 10;
    public Transform[] throwingLocations = new Transform[4];

    public int dropZoneRadius = 10;
    public float airTime = 5;
    [Space(10)]
    public float waitBetweenSpawns = 0.5f;
    public float waitWhenFull = 1f;

    GameObject[] spawnedWeapons;

    void Start()
    {
        spawnedWeapons = new GameObject[maxNumberOfWeapons];

        RandomisedPosition();

        StartCoroutine(SpawnWeaponLoop());

        //Debug.Log(spawnedWeapons.Length);
    }

    /// <summary>
    /// The loop which constantly spawns boxes.
    /// However only if spawnedWeapons has fewer Objects than maxNumberOfWeapons
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnWeaponLoop()
    {
        while (true)
        {
            float waitTime;

            if (spawnedWeapons.Length != maxNumberOfWeapons)
                Array.Resize(ref spawnedWeapons, maxNumberOfWeapons);

            int objectsInArray = CountArray(spawnedWeapons);

            for (int i = objectsInArray; i < maxNumberOfWeapons; i++)
            {
                Vector3 targetPosition;
                bool isPositionValid = false;

                do
                {
                    targetPosition = RandomisedPosition();
                    isPositionValid = CheckForWall(targetPosition);
                } while (isPositionValid);

                Vector3 throwingLocation = throwingLocations[UnityEngine.Random.Range(0, throwingLocations.Length)].position;

                GameObject newWeapon = (GameObject)Instantiate( weaponsPrefabs[UnityEngine.Random.Range(0, weaponsPrefabs.Length)],
                                                                targetPosition,
                                                                Quaternion.identity );
                //SetupWeapon(newWeapon, targetPosition, throwingLocation);
                
                AddToArray(spawnedWeapons, newWeapon);
                yield return new WaitForSeconds(waitBetweenSpawns);
            }
            if (objectsInArray == maxNumberOfWeapons)
                waitTime = waitWhenFull;
            else
                waitTime = waitBetweenSpawns;


            yield return new WaitForSeconds(waitTime);
        }

    }

    void SetupWeapon(GameObject weapon, Vector3 target, Vector3 origin)
    {
        weapon.transform.rotation = Quaternion.LookRotation(target - origin, Vector3.up);

        weapon.AddComponent<Rigidbody>();
        weapon.GetComponent<Rigidbody>().AddForce(CalculateTrajectory(origin, target, airTime), ForceMode.Impulse);
    }

    int CountArray(GameObject[] array)
    {
        int count = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null)
            {
                if (array[i].tag == "Pickup")
                {
                    count++;
                }
            }
        }
        return count;
    }

    void AddToArray(GameObject[] array, GameObject item)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
            {
                array[i] = item;
                break;
            }
        }
    }

    Vector3 RandomisedPosition()
    {
        Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * dropZoneRadius;
        Vector3 randomPos = new Vector3(transform.position.x + randomCircle.x, transform.position.y, transform.position.z + randomCircle.y);

        return randomPos;
    }

    bool CheckForWall(Vector3 point)
    {
        RaycastHit hitInfo;
        Physics.Raycast(point, Vector3.down, out hitInfo);

        if (hitInfo.transform.tag == "Wall")
            return true;
        else
            return false;
    }

    Vector3 CalculateTrajectory(Vector3 from, Vector3 to, float time)
    {
        Vector3 gravity = Physics.gravity;
        Vector3 velocityNeeded = Vector3.zero;
        //Vector3 dir = to - from;

        //dir = Quaternion.LookRotation(to-from, Vector3.up) * dir;

        velocityNeeded.z = (to.z - from.z) / time;
        velocityNeeded.y = (to.y + 0.5f * gravity.y * time * time - from.y) / time;


        return velocityNeeded;
    }

}
