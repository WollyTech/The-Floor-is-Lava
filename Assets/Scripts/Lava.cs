using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour{
    private Vector3 _changePosition;
    private Vector3 _changeScale;
    [SerializeField]
    private float _speed = 1.0f;

    private Transform _ground;

    //position and scale increases at 1:2 on y

    // Start is called before the first frame update
    void Start(){
        _ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<Transform>();

        if (_ground == null){
            Debug.LogError("_ground Transform not found!");
        }

        transform.position = _ground.position;
        transform.localScale = new Vector3(22, 1, 1);
    }

    // Update is called once per frame
    void Update(){

    }

    void FixedUpdate(){
        Rise();
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Cloud"){
            Debug.Log("contact made...");
            other.GetComponent<Cloud>().Destory();
        }

        if (other.tag == "Player"){
            other.GetComponent<Player>().Destroy();
        }

    }

    //coroutine to start rise

    private void Rise(){
        //calculating rate of change
        _changePosition.y = _speed * Time.deltaTime;
        _changeScale.y = _changePosition.y * 2.0f;

        //simulating rising movement
        transform.position += _changePosition;
        transform.localScale += _changeScale;
    }
}