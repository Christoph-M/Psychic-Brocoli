using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WeaponSpawner))]
public class WeaponSpawnerEditor : Editor
{

    void OnSceneGUI()
    {
        WeaponSpawner myObj = (WeaponSpawner)target;
        RaycastHit hitInfo;
        Handles.color = Color.green;

        Physics.Raycast(myObj.transform.position, Vector3.down, out hitInfo);
        float dist = Vector3.Distance(myObj.transform.position, hitInfo.point);

        if (dist > 0 && dist < 50)
        {
            Debug.DrawLine(myObj.transform.position, hitInfo.point);
            Handles.DrawWireDisc(myObj.transform.position, Vector3.up, myObj.dropZoneRadius);
        }

    }

}
