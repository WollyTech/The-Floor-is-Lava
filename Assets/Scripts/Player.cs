using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Player Movement
    #region Character controller
    //Character Controller Component
    private CharacterController _controller;
    #endregion
    #region Speed
    //Movement speed
    [SerializeReference]
    private float _speed = 5.0f;
    #endregion
    #region Gravity
    //Gravity
    [SerializeReference]
    private float _gravity = 1.0f;
    #endregion
    #region Jumping
    //Jump
    [SerializeField]
    private float _jumpHeight = 15.0f;
    private float _yVelocity;
    private bool _jump;
    #endregion
    #endregion

    private Vector3 _velocity;

    private int _height;
    private int _time;

    private UIManager _UImanager;

    // Start is called before the first frame update
    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _UImanager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jump = true;
        }

        HeightTracker();
        Timer();

        _controller.Move(_velocity * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        #region Movement Physics
        //Movement Physics
        float _horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(_horizontalInput, 0, 0);
        _velocity = direction * _speed;
        #endregion

        #region Jump Physics
        //Jump Physics
        if (_controller.isGrounded == true && _jump == true)
        {
            #region Jump
            //Jump           
            _yVelocity = _jumpHeight;
            #endregion
        }
        else
        {
            #region Applying Gravity
            //Applying gravity
            _yVelocity -= _gravity;
            #endregion
        }
        if (_controller.isGrounded == false)
        {
            _jump = false;
        }

        _velocity.y = _yVelocity;
        #endregion

        #region Player Movement
        //Player Movement
        //_controller.Move(_velocity * Time.deltaTime);
        #endregion

        //clamping movement
        Vector3 clampX = new Vector3(Mathf.Clamp(transform.position.x, -9.5f, 9.5f), transform.position.y, transform.position.z);
        transform.position = clampX;
    }

    //destruct coroutine
    //stop game object and destroy after 3 seconds

    #region Destory Logic
    public void Destroy()
    {
        StartCoroutine(DestroyTimer());
    }

    IEnumerator DestroyTimer()
    {
        _speed = 0.0f;
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
    #endregion

    private void HeightTracker()
    {
        _height = Mathf.RoundToInt(transform.position.y);

        _UImanager.UpdateHeight(_height);
    }
    private void Timer()
    {
        _time = Mathf.RoundToInt(Time.fixedTime);

        _UImanager.UpdateTime(_time);
    }

    //mehtod to activate lava coroutine
}