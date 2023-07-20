using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TapTurn
{
    public class Ball : MonoBehaviour
    {
        [Header("References")]
        private Rigidbody2D rb2D;
        private SpriteRenderer sr;
        private WayPointManager wayPointManager;
        private GamePlayManager gameplayManager;
        [SerializeField] private GameObject destroyBallParticlePrefab;
        [SerializeField] private GhostEffect ghostEffect;

        [Header("Properties")]
        [SerializeField] private float speed = 5.0f;
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private LayerMask finishLineLayer;

        // Cached
        private Vector3 nextTurnDirection;
        private GameObject destroyBallParticle;

        private void Start()
        {
            wayPointManager = WayPointManager.Instance;
            gameplayManager = GamePlayManager.Instance;
            rb2D = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();

            nextTurnDirection = wayPointManager.GetNextTurnDirection();
        }


        private void Update()
        {
            if(gameplayManager.currentState == GamePlayManager.GameState.PLAYING)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Turn();
                    SFXManager.Instance.PlaySound(SoundType.Tap, false);
                    GameManager.Instance.ScoreUp();
                }
            }    
        }

        private void FixedUpdate()
        {
            switch (gameplayManager.currentState)
            {
                default: break;
                case GamePlayManager.GameState.PLAYING:
                    rb2D.velocity = nextTurnDirection * speed;
                    break;
                case GamePlayManager.GameState.GAMEOVER:
                    rb2D.velocity = Vector2.zero;
                    ghostEffect.isGhosting = false;
                    break;
                case GamePlayManager.GameState.WIN:
                    rb2D.velocity = -wayPointManager.CurrentMoveDirection * speed;
                    ghostEffect.isGhosting = false;
                    break;
            }
       

        }

        private void Turn()
        {
            wayPointManager.currentWayPointIndex++;
            nextTurnDirection = wayPointManager.GetNextTurnDirection();

        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((obstacleLayer.value & 1 << collision.gameObject.layer) != 0)
            {
                if (gameplayManager.currentState != GamePlayManager.GameState.WIN)
                {
                    gameplayManager.ChangeGameState(GamePlayManager.GameState.GAMEOVER);
                }
                SFXManager.Instance.PlaySound(SoundType.Destroyed, false);
                CameraShake.Instance.Shake();
                PlayDestroyBallParticle();
                SetVisual(false);
            }

            if ((finishLineLayer.value & 1 << collision.gameObject.layer) != 0)
            {
                gameplayManager.ChangeGameState(GamePlayManager.GameState.WIN);
            }
        }

        private void PlayDestroyBallParticle()
        {
            destroyBallParticle = Instantiate(destroyBallParticlePrefab, transform.position, Quaternion.identity);
        }

        private void SetVisual(bool isVisual)
        {
            sr.enabled = isVisual;
        }

        public void DestroyVFX()
        {
            Destroy(destroyBallParticle.gameObject);
        }
    }
}
