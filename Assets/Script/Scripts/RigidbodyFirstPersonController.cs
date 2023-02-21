using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

    [RequireComponent(typeof (Rigidbody))]
    [RequireComponent(typeof (CapsuleCollider))]
    public class RigidbodyFirstPersonController : MonoBehaviour
    {
        [Serializable]
        public class MovementSettings
        {
            public float ForwardSpeed = 5.5f;   // Speed when walking forward
            public float BackwardSpeed = 4.5f;  // Speed when walking backwards
            public float StrafeSpeed = 4.5f;    // Speed when walking sideways
            public float RunMultiplier = 3.5f;   // Speed when sprinting
	        public KeyCode RunKey = KeyCode.LeftShift;
            public float JumpForce = 100f;
            public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
            [HideInInspector] public float CurrentTargetSpeed = 8f;

            //private bool m_Running;

            public void UpdateDesiredTargetSpeed(Vector2 input)
            {
	            if (input == Vector2.zero) return;
				if (input.x > 0 || input.x < 0)
				{
					//strafe
					CurrentTargetSpeed = StrafeSpeed;
				}
				if (input.y < 0)
				{
					//backwards
					CurrentTargetSpeed = BackwardSpeed;
				}
				if (input.y > 0)
				{
					//forwards
					//handled last as if strafing and moving forward at the same time forwards speed should take precedence
					CurrentTargetSpeed = ForwardSpeed;
				}
            }
        }


        [Serializable]
        public class AdvancedSettings
        {
            public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
            public float stickToGroundHelperDistance = 0.5f; // stops the character
            public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
            public bool airControl; // can the user control the direction that is being moved in the air
            [Tooltip("set it to 0.1 or more if you get stuck in wall")]
            public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
        }


        public Camera cam;
        public MovementSettings movementSettings = new MovementSettings();
        public MouseLook mouseLook = new MouseLook();
        public AdvancedSettings advancedSettings = new AdvancedSettings();
        public Rigidbody m_RigidBody;
        private CapsuleCollider m_Capsule;
        private float m_YRotation;
        private Vector3 m_GroundContactNormal;
        private bool m_Jump, m_PreviouslyGrounded;
        public bool m_IsGrounded;

        public Vector2 RunAxis = Vector2.zero;
        public float fallMultiplier = 2.5f;
        public float flowJumpMultiplier = 2f;
        public bool JumpAxis;
        public bool CheckDestroy = false;
        public Vector3 camforward;
        private bool m_IsWalking;
        [SerializeField] private AudioClip[] m_FootstepSounds;
        [SerializeField] private AudioClip m_JumpSound;
        [SerializeField] private AudioClip m_LandSound;
        [SerializeField] private AudioClip playerCream;
        public AudioClip Painting;
        [SerializeField] private AudioSource m_AudioSource;
        private float m_StepCycle;
        private float m_NextStep;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        public UIPlayerManager uIPlayerManager;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float m_StepInterval = 0;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        public GameObject smoke;
        public GameObject body;
        [SerializeField] private Animator _animatorPlayer;

        public Vector3 Velocity
        {
            get { return m_RigidBody.velocity; }
        }

        public bool Grounded
        {
            get { return m_IsGrounded; }
        }
        public Timer timer;
        private float canJump = 0f;
        public Animator AnimatorPlayer;


        private void Start()
        {
            m_RigidBody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            mouseLook.Init (transform, cam.transform);
            m_HeadBob.Setup(cam, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
        }


        private void Update()
        {
            if(GameControll.Instance.CheckStartPlayer){
                RotateView();

                if ( JumpAxis && !m_Jump)
                {
                    m_Jump = true;
                }
            GroundCheck();
            if((RunAxis.y != 0f) || JumpAxis){
                cam.GetComponent<PlayerCam>().DoFov(90);
            }else if(!JumpAxis && m_IsGrounded && m_IsGrounded){
                cam.GetComponent<PlayerCam>().DoFov(60);
            }
            Vector2 input = GetInput();
            if(( Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon)){
                Vector3 desiredMove = cam.transform.forward*input.y + cam.transform.right*input.x;
                desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized * movementSettings.CurrentTargetSpeed;
                if (m_RigidBody.velocity.sqrMagnitude <
                    (movementSettings.CurrentTargetSpeed*movementSettings.CurrentTargetSpeed))
                {
                    m_RigidBody.AddForce(desiredMove*SlopeMultiplier(), ForceMode.Impulse);              
                }
                ProgressStepCycle(movementSettings.CurrentTargetSpeed, input);
            }
            if (m_IsGrounded && m_Jump)
            {
                if(Time.time > canJump){ 
                    PlayerJumpSound();
                    Vector3 flagVel = m_RigidBody.velocity;
                    m_RigidBody.velocity = new Vector3(flagVel.x, 0, flagVel.z);
                    m_RigidBody.AddForce(Vector3.up * movementSettings.JumpForce, ForceMode.Impulse);
                    canJump = Time.time + 0.1f; 
                }
            }
            //check luc cham xuong dat
            if(!m_PreviouslyGrounded && m_IsGrounded){
                PlayLandingSound();
            }
            if(CheckDestroy){
                Death(GameControll.Instance.ThrowPlayer);
                CheckDestroy = false;
            }
            m_Jump = false;
        }
            UpGravity();
        }

        //audio
        private void ProgressStepCycle(float speed, Vector2 input)
        {
            if (m_RigidBody.velocity.sqrMagnitude > 0 && (input.x != 0 || input.y != 0))
            {
                m_StepCycle += (m_RigidBody.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;
            PlayFootStepAudio();
        }
        private void PlayFootStepAudio()
        {
            if (!m_IsGrounded)
            {
                return;
            }
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }
        private void PlayerJumpSound(){
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }
        private void PlayLandingSound(){
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }

        public void UpGravity(){
            if(m_RigidBody.velocity.y < 0){
                m_RigidBody.velocity += 0.5f * Vector3.up * Physics.gravity.y * Time.deltaTime;
            }
        }

        private float SlopeMultiplier()
        {
            float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
            return movementSettings.SlopeCurveModifier.Evaluate(angle);
        }
        private Vector2 GetInput()
        {
            
            Vector2 input = new Vector2
                {
                    x = RunAxis.x,
                    y = RunAxis.y
                };
			movementSettings.UpdateDesiredTargetSpeed(input);
            return input;
        }


        private void RotateView()
        {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;
            // get the rotation before it's changed
            float oldYRotation = transform.eulerAngles.y;
            mouseLook.LookRotation (transform, cam.transform);
            if (m_IsGrounded || advancedSettings.airControl)
            {
                // Rotate the rigidbody velocity to match the new direction that the character is looking
                Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
                m_RigidBody.velocity = velRotation * m_RigidBody.velocity;
            }
        }

        /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
        private void GroundCheck()
        {
            m_PreviouslyGrounded = m_IsGrounded;
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_IsGrounded = true;
                m_GroundContactNormal = hitInfo.normal;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundContactNormal = Vector3.up;
            }
        }
        public void RaiseSpeed(){
            movementSettings.ForwardSpeed = 8.0f;   // Speed when walking forward
            movementSettings.BackwardSpeed = 6.0f;  // Speed when walking backwards
            movementSettings.StrafeSpeed = 6.0f;    // Speed when walking sideways
            movementSettings.RunMultiplier = 5.0f;   // Speed when sprinting
        }
        public void ResetSpeed(){
            movementSettings.ForwardSpeed = 5.5f;   // Speed when walking forward
            movementSettings.BackwardSpeed = 4.5f;  // Speed when walking backwards
            movementSettings.StrafeSpeed = 4.5f;    // Speed when walking sideways
            movementSettings.RunMultiplier = 3.5f;   // Speed when sprinting
        }
        public void Death(Vector3 dir){
            Vector3 g = Vector3.ProjectOnPlane(dir, m_GroundContactNormal).normalized;
            m_RigidBody.constraints = RigidbodyConstraints.None;
            transform.rotation = Random.rotation;
            UIManager.Instance.UICanvasInGame.SetActive(false);
            GameControll.Instance.CheckStart = false;
            GameControll.Instance.CheckStartPlayer = false;
            if(GameControll.Instance.Mod != 4){
                m_RigidBody.AddForce( g * 50 , ForceMode.Impulse);
            }
            if(GameControll.Instance.Mod == 1){
                timer.DestroyTimer();
            }
            m_AudioSource.PlayOneShot(playerCream);
            Invoke("DieEffect", 0.5f);  //0.5f 2.5f
            Invoke("ResetGamePlayer",2.5f);
        }
        public void DieEffect(){
            smoke.SetActive(true);
            body.SetActive(false);
        }
        public void ResetGamePlayer(){
            m_RigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            if(GameControll.Instance.Mod != 4){
                playerController.JumpButton.Pressed = false;
                UIManager.Instance.UICanvasReStartGame.SetActive(true);
                UIManager.Instance.TextEffectDied.TextMesh.text = "Died";
                UIManager.Instance.TextEffectDied.StartZoom();
                GameControll.Instance.DisableChildEnemyPlayerKeys();
            }else{
                //player
                ModeSCPUI.Instance.waitUI.SetupWaitTime();
            }
        }
        public void ResetPlayer(){
            mouseLook.m_CameraTargetRot = Quaternion.Euler(0,0,0);
            mouseLook.m_CharacterTargetRot = Quaternion.Euler(0,0,0);
            transform.localRotation = Quaternion.Euler(0,0,0);
            cam.transform.localRotation = Quaternion.Euler(0,0,0);
            smoke.SetActive(false);
            body.SetActive(true);
        }
    }
