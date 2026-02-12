using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;
 
public class Character : MonoBehaviour 
{
    private Rigidbody characterRigidBody;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private CharacterData characterData;
    [SerializeField] private Animator characterAnimator;
    private float distanceToMove = 2f;
    [SerializeField] private float moveDuration = 0.2f;
    [SerializeField] private Transform CharacterStartPivot;

     [SerializeField]  
       private UnityEvent onJump;
    [SerializeField]
    private UnityEvent onMoveToSide;
    [SerializeField]
    private UnityEvent onRoll;
    
    private bool isGrounded = true;
    private bool isMoving = false;
    private bool isRolling = false;
    private bool isActive = false;
 
    private void Awake() 
    {
        characterRigidBody = GetComponent<Rigidbody>();
    }
 
    public void StartGame() 
    {
        isRolling = false;
        isMoving = false;
        isActive = true;
        characterAnimator.Play(characterData.runAnimationName, 0, 0f);
        transform.position = CharacterStartPivot.position;
    }
 
    public void Lose() 
    {
        isActive = false;
        StopAllCoroutines();
        characterAnimator.Play(characterData.runAnimationName, 0, 0F);
    }
 
    public void Jump() 
    {
        if(!isActive) return;
        
        if (isGrounded) 
        {
            onJump?.Invoke();
            characterAnimator.Play(characterData.jumpAnimationName, 0, 0f);
            characterRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
 
    public void MoveDown() 
    {
        if(!isActive || isRolling) return;
        
        if (!isGrounded) 
        {
            characterRigidBody.AddForce(Vector3.down * jumpForce * 2, ForceMode.Impulse);
        }
        
        characterAnimator.Play(characterData.rollAnimationName, 0, 0f);
       onRoll?.Invoke();
        isRolling = true;
        StartCoroutine(ResetRoll());
    }
 
    public void MoveLeft() 
    {
        Move(Vector3.left);
    }
 
    public void MoveRight() 
    {
        Move(Vector3.right);
    }
 
    private void Move(Vector3 direction) 
    {
        if (!isActive || isMoving) return;

        onMoveToSide?.Invoke();
        
        characterAnimator.Play(characterData.moveAnimationName, 0, 0f);
        isMoving = true;
        
        Vector3 targetPosition = transform.position + direction * distanceToMove;
        transform.DOMove(targetPosition, moveDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => 
            {
                isMoving = false;
            });
    }
 
    private IEnumerator ResetRoll() 
    {
        yield return new WaitForSeconds(characterAnimator.GetCurrentAnimatorStateInfo(0).length);
        isRolling = false;
    }
 
    public void OnCollisionEnter(Collision collision) 
    {
        if (isActive && collision.gameObject.CompareTag("Ground")) 
        {
            if (!isRolling) 
            {
                characterAnimator.Play(characterData.runAnimationName, 0, 0f);
            }
            isGrounded = true;
        }
    }
}
 