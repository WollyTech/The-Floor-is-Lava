using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour{
    private int _direction;
    private float _changeDirection;

    [SerializeField]
    private float _speed = 5.0f;

    private BoxCollider _collider;

    // Start is called before the first frame update
    void Start(){
        DecidingDirection();

        _collider = GameObject.FindGameObjectWithTag("Cloud").GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update(){

    }

    void FixedUpdate(){
        MovementLogic();
    }

    private void DecidingDirection(){
        _direction = Random.Range(0, 2);
        if (_direction == 0){
            _direction = -1;
        }

        _changeDirection = Random.Range(2.0f, 8.0f);
    }

    private void MovementLogic(){
        Vector3 direction = new Vector3(_direction, 0, 0);

        //Bounce
        if (transform.position.x > _changeDirection){
            _direction = -1;
        }
        else if (transform.position.x < -(_changeDirection)){
            _direction = 1;
        }

        //Movement
        Vector3 velocity = direction * _speed * Time.deltaTime;

        transform.Translate(velocity);
    }

    //Destroy gsme object coroutine
    public void Destory(){
        StartCoroutine(DestroyTimer());
    }

    IEnumerator DestroyTimer(){
        _speed = 0.0f;
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }


    //Passing through the clouds
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            StartCoroutine(InvinisibleCloud());
            other.transform.parent = gameObject.transform;
        }
    }
    private void OnTriggerExit(Collider other){
        if (other.gameObject.tag == "Player"){
            other.transform.parent = null;
        }
    }

    IEnumerator InvinisibleCloud(){
        _collider.enabled = false;
        yield return new WaitForSeconds(0.25f);
        _collider.enabled = true;
    }
}