using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    public LayerMask movementMask;

    public Interactable focus;

    Camera cam;
    PlayerMotor motor;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update() {

        /**
         * TODO Test some more EventSystem.current
         */
        if(EventSystem.current.IsPointerOverGameObject()) { // If pointer is over UI then disable player movement
            return;
        }

        // if left mouse is pressed
        if(Input.GetMouseButtonDown(0)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, movementMask)){
                motor.MoveToPoint(hit.point);

                RemoveFocus();
            }
        }

        // if right mouse is pressed
        if(Input.GetMouseButtonDown(1)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if the ray hits
            if(Physics.Raycast(ray, out hit, 100)){
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                
                if(interactable != null){
                    SetFocus(interactable);
                }
                
            }
        }
    }

    void SetFocus(Interactable newFocus){

        if(newFocus != focus){
            if(focus != null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(focus);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus(){
        if(focus != null)
            focus.OnDefocused();
            
        focus = null;
        motor.StopFollowingTarget();
    }
}
