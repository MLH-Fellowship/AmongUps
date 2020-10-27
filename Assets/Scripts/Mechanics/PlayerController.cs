using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using UnityEngine.UI;
using TMPro;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        public TextMeshProUGUI scoreKeeper;

        public GameObject powerUpsPanel;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 5;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        public Transform attackPoint, spawnPoint;
        public float attackRange = 0.5f;

        public int attackDamage = 2;

        public float attackRate = 2f;
        float nextAttackTime = 0f;

        public int playerScore = 0;
        public bool isDead = false;

        List<PowerUp> powerUps;

        public LayerMask enemyLayer;
        bool isSpriteFlipped;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            isSpriteFlipped = spriteRenderer.flipX;
            powerUps = new List<PowerUp>();
        }

        protected override void Update()
        {
            if (controlEnabled && !isDead)
            {
                //
                if(spriteRenderer.flipX != isSpriteFlipped) {
                    Vector3 newPos = new Vector3(attackPoint.transform.localPosition.x * -1f, 0f, 0f);
                    attackPoint.transform.localPosition = newPos;
                    isSpriteFlipped = spriteRenderer.flipX;
                }

                move.x = gameObject.CompareTag("Crewmate") ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal-2");
                bool jumpKeyDown = gameObject.CompareTag("Crewmate") ? Input.GetButtonDown("Jump") : Input.GetButtonDown("Jump-2");
                bool jumpKeyUp = gameObject.CompareTag("Crewmate") ? Input.GetButtonUp("Jump") : Input.GetButtonUp("Jump-2");

                if (jumpState == JumpState.Grounded && jumpKeyDown)
                    jumpState = JumpState.PrepareToJump;
                else if (jumpKeyUp)
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }

                bool attackKey = gameObject.CompareTag("Crewmate") ? Input.GetButtonDown("Attack") : Input.GetButtonDown("Attack-2");
                if (Time.time >= nextAttackTime) {
                    if (attackKey) {
                        Attack();
                        nextAttackTime = Time.time + 1f / attackRate;
                    }
                }


            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void Attack() {
            // Play attack animation
            animator.SetTrigger("attack");
            // Detect enemy in range of attack
            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            // Decrease enemy health
            foreach(Collider2D enemy in hitEnemy) {
                enemy.GetComponent<Health>().Decrement(attackDamage);
                if(enemy.GetComponent<Health>().IsAlive)
                    enemy.GetComponent<Animator>().Play("Player-Hurt");
            }
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public void incrementScore() {
            playerScore = playerScore + 1;
            scoreKeeper.text = $"{playerScore}";
        }

        public void Dead()
        {
            animator.Play("Player-Death");
            isDead = true;
        }

        public void Respawn() {
            isDead = false;
            health.Respawn();
            transform.position = spawnPoint.position;
            animator.Play("Player-Spawn");
        }

        public void AddPowerUp(PowerUp newPowerUp) {
            // add powerUp to player's list of powerups
            powerUps.Add(newPowerUp);

            // pass player controller to execute function of the powerup to modify player variables
            newPowerUp.Execute(this);

            // add image of powerUp Icon to the powerUp panel under player's health bar
            GameObject powerUpObject = new GameObject(newPowerUp.powerUpName);
            powerUpObject.transform.SetParent(powerUpsPanel.transform);
            RectTransform trans = powerUpObject.AddComponent<RectTransform>();
            Image powerUpImage = powerUpObject.AddComponent<Image>();
            powerUpImage.sprite = newPowerUp.powerUpIcon;
            // keep original scale and aspect ratio of the powerup icon
            trans.localScale = new Vector3 (1f, 1f, 1f);
            powerUpImage.preserveAspect = true;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}