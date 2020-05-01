using System.Collections.Generic;
using NaughtyAttributes;
using GM;
using UnityEngine;
using System;


[Serializable]
public class PoolInfo
{
    public string Name = "Name";
    public GameObject Prefab;
    public int PoolSize = 10;
    public string TypeOf = "Transform";
}

public class PoolManager : MonoBehaviour
{
    #region Inspector
    [BoxGroup("Pool Objects"), ReorderableList] public PoolInfo[] PoolInfo;
    #endregion

    #region Private Properties
    //Private
    private static Dictionary<string, Queue<Component>> pool;
    private static List<GameObject> usedObj;
    #endregion

    #region Init state
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        GlobalManager.PoolManager = this;

        pool = new Dictionary<string, Queue<Component>>();
        usedObj = new List<GameObject>();

        PopulatePool();
    }
    #endregion

    #region Functions
    /// <summary>
    /// Function that generate our pool.
    /// it uses dictionary with string as key (name given in pool info) and queue of components (given in pool info).
    /// </summary>
    private void PopulatePool()
    {

        for (int i = 0; i < PoolInfo.Length; i++)
        {
            PoolInfo info = PoolInfo[i];

            if (info.Prefab == null || info.PoolSize <= 0)
                continue;

            GameObject Obj_Holder = new GameObject(info.Name);
            Obj_Holder.transform.parent = transform;

            Queue<Component> queue = new Queue<Component>();

            string type = info.TypeOf;

            for (int j = 0; j < info.PoolSize; j++)
            {
                GameObject obj = Instantiate(info.Prefab, Obj_Holder.transform);

                obj.name = info.Name + " - " + j;
                obj.transform.position = Vector3.zero;
                obj.SetActive(false);

                Component component = obj.GetComponent(type) as Component;

                if (component == null)
                    component = obj.GetComponent("Transform") as Component;

                queue.Enqueue(component);
            }

            pool.Add(info.Name, queue);
        }
    }

    /// <summary>
    /// Function for restarting all used objects.
    /// Usefull at end of level.
    /// </summary>
    public void RestartUsedObjects()
    {
        for (int i = 0; i < usedObj.Count; i++)
        {
            //usedObj[i].transform.localScale = Vector3.one;
            usedObj[i].SetActive(false);


        }



        for (int i = 0; i < usedObj.Count; i++)
        {


        }
    }



    #region Get Functions

    //GetFromPool


    #endregion
    /// <summary>
    /// Functon that returns object from dictionary
    /// </summary>
    /// <typeparam name="T">Generic type that we want to get back.
    /// (Must be in dictionary or else it returns null if it doenst match)</typeparam>
    /// <param name="name">Name of object we gave, it will look up in dictionary by name.
    /// if it doesnt find object in dictionary, it will return null</param>
    /// <returns>Returns component that is specified in pool info class.
    /// if the name or type doesnt match, it will return null.</returns>
    /// 
    public ParticleSystem GetWinning()
    {
        Transform tr = GetObjectFromDictionary<Transform>("Win");
        if (!tr)
            return null;

        return tr.GetComponent<ParticleSystem>();
    }

    public T GetObjectFromDictionary<T>(string name) where T : Component
    {
        if (!pool.ContainsKey(name))
        {
            Debug.LogError("No item found in Dictionary!\nKey passed: " + name + "\nAt Function: GetObjectFromDictionary() with T (type) : " + typeof(T).Name);
            return null;
        }

        Component poolobj = pool[name].Dequeue();
        pool[name].Enqueue(poolobj);

        if (poolobj.GetType().Name != typeof(T).Name)
        {
            Debug.LogError("Type passed doesnt match with one in Dictionary! \nKey passed: " + name + "\nAt Function: GetObjectFromDictionary() with T (type) : " + typeof(T).Name + "\nType from Dictionary: " + poolobj.GetType().Name);
            return null;
        }

        GameObject obj = poolobj.gameObject;

        obj.SetActive(false);
        obj.SetActive(true);

        usedObj.Add(obj);

        return (T)poolobj;
    }

    #endregion
}
