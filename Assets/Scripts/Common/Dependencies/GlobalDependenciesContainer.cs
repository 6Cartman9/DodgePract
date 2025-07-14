using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDependenciesContainer : Dependecy
{
    [SerializeField] private Pauser pauser;

    private static GlobalDependenciesContainer instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoader;

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoader;
    }

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        Bind<Pauser>(pauser, monoBehaviourInScene);
    }

    private void OnSceneLoader(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjectToBind();
    }

}
