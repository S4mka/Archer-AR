using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject _targets;
    [SerializeField] AudioSource _audioSource;

    public void Start()
    {
        _targets.SetActive(true);
        _audioSource.Play();

    }
    public void Exit()
    {
        Application.Quit();
    }

}
