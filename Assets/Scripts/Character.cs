using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    private Rigidbody characterRigidBody;
    [SerializeField]

    private float jumpForce = 5f;
    [SerializeField]

    private CharacterData characterData;
    [SerializeField]
    private Animator characterAnimator;

    private float distanceToMove = 2f;
    [SerializeField]
    private float  moveDuration = 0.2f;

    private bool isGrounded = true;

    private bool isMoving = false;
  
    
    private void Start()
    {
        characterAnimator.Play(characterData.runAnimationName, 0, 0f);
        characterRigidBody = GetComponent<Rigidbody>();
    }
    public void Jump()
    {
        if (isGrounded)
        {
            characterRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    public void MoveDown()
    {
        if (!isGrounded)
        {
            characterRigidBody.AddForce(Vector3.down * jumpForce * 2, ForceMode.Impulse);
        }
        characterAnimator.Play(characterData.rollAnimationName, 0, 0f);
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
        if (isMoving) return;
        characterAnimator.Play(characterData.moveAnimationName, 0, 0f);

        isMoving = true;
        Vector3 targetPosition = transform.position + direction * distanceToMove;

        transform.DOMove(targetPosition, moveDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            isMoving = false;
        });
            

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            characterAnimator.Play(characterData.runAnimationName, 0, 0f);
            isGrounded = true;
        }
    }
}
