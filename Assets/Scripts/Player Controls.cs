//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Control Schemes/Player.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player"",
    ""maps"": [
        {
            ""name"": ""Aiming"",
            ""id"": ""f4db1a76-b62e-4627-ad8a-3800a6949d4a"",
            ""actions"": [
                {
                    ""name"": ""Gamepad"",
                    ""type"": ""Value"",
                    ""id"": ""7bdbda4d-6f07-4c5c-bc8e-de6d7d72f42e"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""d00978a1-45f0-4c8e-a4e7-5d70d5e49639"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3ba23ebc-4349-4254-8075-913fa6cf5b10"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3829fa51-6db3-4de1-90e2-58362ba8752a"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Moving"",
            ""id"": ""2a5b2d4b-8b72-4329-887b-0175f9592f7f"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""4d69b009-f0f0-41a7-8f2a-657d4e434799"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Value"",
                    ""id"": ""10619d89-ef92-4921-b560-a5bc59f448a6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""cb73ca5f-7955-49aa-becb-486fca34c443"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""d0573850-6a6e-4c63-aa13-1468054ecbac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""282dacca-b804-4570-997b-27dbe6a92ca8"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0615cae6-d9e5-459d-8c8f-68527fcb95d6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02ba437e-3509-441a-9ac7-b0761e6ff870"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d56ad267-41ee-447f-97c3-3740b69ede86"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""285f6adc-3924-4aca-bacd-c28a8dd2a5f2"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""715cf7c9-23e6-40ca-b104-c2da9f454084"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""17d667b4-f75b-4708-acbe-3d772c94b84e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""075529d3-520d-49c0-8186-fd16059a2428"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""09998936-36bb-4a53-a79e-6df4f2eb7551"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""627ca85c-59be-47b2-868d-619ab9a73cc9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""63c0ff1f-0150-4975-9dd5-525a271de099"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1c7ec684-e760-4528-8a13-049b91b5b100"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ca615ccd-d3d8-4daa-93cb-3f87ff6e4bac"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""725b0ec6-bd6e-4d85-b0ae-f4b985831e34"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee8a5a2b-a84c-438e-bcd6-0d91e2d79d05"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""de0abd40-9c77-4292-ae6a-1d3dede53a31"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""down"",
                    ""id"": ""81a8df05-847a-42b4-adaf-5fe70e4abd4c"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3b2f7586-c6b4-47bc-9e45-a348dee2cc2d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""down"",
                    ""id"": ""62eaf1f2-8909-4d48-adf7-641e6fda1d8c"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d77a59ad-7df8-4120-8b07-2378b52e1b87"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5ebdb51c-b947-4202-a4bb-9644f983ecfd"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Attacking"",
            ""id"": ""d2e792a3-566c-4d82-8c8b-10805fcfba52"",
            ""actions"": [
                {
                    ""name"": ""Reflect"",
                    ""type"": ""Button"",
                    ""id"": ""cb895ed9-c51d-4b3e-a666-0ac4f51231a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bd858a80-2d19-42ad-92e5-15c43aec0484"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reflect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ff505b2-c3c3-4188-97f6-4c29096ac974"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reflect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Pausing"",
            ""id"": ""ffc927c4-34d0-43f1-910e-219f74f544b7"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""85959dd4-efb8-462b-9e3a-22f04434ddd4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bdbd5eee-1ff0-4e62-aa14-e5137ad5b5db"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9840c0a5-8489-42fe-93da-9e15160f42f9"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Abilities"",
            ""id"": ""2e43fd17-c45c-4354-b512-b230eaec2486"",
            ""actions"": [
                {
                    ""name"": ""OmniReflect"",
                    ""type"": ""Button"",
                    ""id"": ""f6b76017-0485-4b16-af4c-a339da7570d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d1ad77b9-12f6-4a33-89f6-23424a95bd80"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OmniReflect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a84852cd-4101-4064-a0e0-6af37e010a0a"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OmniReflect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interaction"",
            ""id"": ""68b95048-4ce3-41d1-b4ed-cdad866d09cb"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""938f71bb-396e-4df1-b71a-6779fdcee1ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ProgressDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""b18f01ea-28fc-40d2-a5ce-acefeae5d511"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""253c6261-428e-4ecb-a8d3-753153d18ff0"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e335022d-b2df-4ae4-a939-fb103116db7c"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a6cbe60-1955-476a-92d9-2c8575d64415"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ProgressDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0bd31421-f0ff-4927-b178-fabcdec9c8e4"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ProgressDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Aiming
        m_Aiming = asset.FindActionMap("Aiming", throwIfNotFound: true);
        m_Aiming_Gamepad = m_Aiming.FindAction("Gamepad", throwIfNotFound: true);
        m_Aiming_Mouse = m_Aiming.FindAction("Mouse", throwIfNotFound: true);
        // Moving
        m_Moving = asset.FindActionMap("Moving", throwIfNotFound: true);
        m_Moving_Move = m_Moving.FindAction("Move", throwIfNotFound: true);
        m_Moving_Drop = m_Moving.FindAction("Drop", throwIfNotFound: true);
        m_Moving_Jump = m_Moving.FindAction("Jump", throwIfNotFound: true);
        m_Moving_Dash = m_Moving.FindAction("Dash", throwIfNotFound: true);
        // Attacking
        m_Attacking = asset.FindActionMap("Attacking", throwIfNotFound: true);
        m_Attacking_Reflect = m_Attacking.FindAction("Reflect", throwIfNotFound: true);
        // Pausing
        m_Pausing = asset.FindActionMap("Pausing", throwIfNotFound: true);
        m_Pausing_Pause = m_Pausing.FindAction("Pause", throwIfNotFound: true);
        // Abilities
        m_Abilities = asset.FindActionMap("Abilities", throwIfNotFound: true);
        m_Abilities_OmniReflect = m_Abilities.FindAction("OmniReflect", throwIfNotFound: true);
        // Interaction
        m_Interaction = asset.FindActionMap("Interaction", throwIfNotFound: true);
        m_Interaction_Interact = m_Interaction.FindAction("Interact", throwIfNotFound: true);
        m_Interaction_ProgressDialogue = m_Interaction.FindAction("ProgressDialogue", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Aiming
    private readonly InputActionMap m_Aiming;
    private IAimingActions m_AimingActionsCallbackInterface;
    private readonly InputAction m_Aiming_Gamepad;
    private readonly InputAction m_Aiming_Mouse;
    public struct AimingActions
    {
        private @PlayerControls m_Wrapper;
        public AimingActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Gamepad => m_Wrapper.m_Aiming_Gamepad;
        public InputAction @Mouse => m_Wrapper.m_Aiming_Mouse;
        public InputActionMap Get() { return m_Wrapper.m_Aiming; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AimingActions set) { return set.Get(); }
        public void SetCallbacks(IAimingActions instance)
        {
            if (m_Wrapper.m_AimingActionsCallbackInterface != null)
            {
                @Gamepad.started -= m_Wrapper.m_AimingActionsCallbackInterface.OnGamepad;
                @Gamepad.performed -= m_Wrapper.m_AimingActionsCallbackInterface.OnGamepad;
                @Gamepad.canceled -= m_Wrapper.m_AimingActionsCallbackInterface.OnGamepad;
                @Mouse.started -= m_Wrapper.m_AimingActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_AimingActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_AimingActionsCallbackInterface.OnMouse;
            }
            m_Wrapper.m_AimingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Gamepad.started += instance.OnGamepad;
                @Gamepad.performed += instance.OnGamepad;
                @Gamepad.canceled += instance.OnGamepad;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
            }
        }
    }
    public AimingActions @Aiming => new AimingActions(this);

    // Moving
    private readonly InputActionMap m_Moving;
    private IMovingActions m_MovingActionsCallbackInterface;
    private readonly InputAction m_Moving_Move;
    private readonly InputAction m_Moving_Drop;
    private readonly InputAction m_Moving_Jump;
    private readonly InputAction m_Moving_Dash;
    public struct MovingActions
    {
        private @PlayerControls m_Wrapper;
        public MovingActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Moving_Move;
        public InputAction @Drop => m_Wrapper.m_Moving_Drop;
        public InputAction @Jump => m_Wrapper.m_Moving_Jump;
        public InputAction @Dash => m_Wrapper.m_Moving_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Moving; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovingActions set) { return set.Get(); }
        public void SetCallbacks(IMovingActions instance)
        {
            if (m_Wrapper.m_MovingActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MovingActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MovingActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MovingActionsCallbackInterface.OnMove;
                @Drop.started -= m_Wrapper.m_MovingActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_MovingActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_MovingActionsCallbackInterface.OnDrop;
                @Jump.started -= m_Wrapper.m_MovingActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MovingActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MovingActionsCallbackInterface.OnJump;
                @Dash.started -= m_Wrapper.m_MovingActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_MovingActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_MovingActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_MovingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public MovingActions @Moving => new MovingActions(this);

    // Attacking
    private readonly InputActionMap m_Attacking;
    private IAttackingActions m_AttackingActionsCallbackInterface;
    private readonly InputAction m_Attacking_Reflect;
    public struct AttackingActions
    {
        private @PlayerControls m_Wrapper;
        public AttackingActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Reflect => m_Wrapper.m_Attacking_Reflect;
        public InputActionMap Get() { return m_Wrapper.m_Attacking; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AttackingActions set) { return set.Get(); }
        public void SetCallbacks(IAttackingActions instance)
        {
            if (m_Wrapper.m_AttackingActionsCallbackInterface != null)
            {
                @Reflect.started -= m_Wrapper.m_AttackingActionsCallbackInterface.OnReflect;
                @Reflect.performed -= m_Wrapper.m_AttackingActionsCallbackInterface.OnReflect;
                @Reflect.canceled -= m_Wrapper.m_AttackingActionsCallbackInterface.OnReflect;
            }
            m_Wrapper.m_AttackingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Reflect.started += instance.OnReflect;
                @Reflect.performed += instance.OnReflect;
                @Reflect.canceled += instance.OnReflect;
            }
        }
    }
    public AttackingActions @Attacking => new AttackingActions(this);

    // Pausing
    private readonly InputActionMap m_Pausing;
    private IPausingActions m_PausingActionsCallbackInterface;
    private readonly InputAction m_Pausing_Pause;
    public struct PausingActions
    {
        private @PlayerControls m_Wrapper;
        public PausingActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Pausing_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Pausing; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PausingActions set) { return set.Get(); }
        public void SetCallbacks(IPausingActions instance)
        {
            if (m_Wrapper.m_PausingActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_PausingActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PausingActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PausingActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PausingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PausingActions @Pausing => new PausingActions(this);

    // Abilities
    private readonly InputActionMap m_Abilities;
    private IAbilitiesActions m_AbilitiesActionsCallbackInterface;
    private readonly InputAction m_Abilities_OmniReflect;
    public struct AbilitiesActions
    {
        private @PlayerControls m_Wrapper;
        public AbilitiesActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @OmniReflect => m_Wrapper.m_Abilities_OmniReflect;
        public InputActionMap Get() { return m_Wrapper.m_Abilities; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AbilitiesActions set) { return set.Get(); }
        public void SetCallbacks(IAbilitiesActions instance)
        {
            if (m_Wrapper.m_AbilitiesActionsCallbackInterface != null)
            {
                @OmniReflect.started -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnOmniReflect;
                @OmniReflect.performed -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnOmniReflect;
                @OmniReflect.canceled -= m_Wrapper.m_AbilitiesActionsCallbackInterface.OnOmniReflect;
            }
            m_Wrapper.m_AbilitiesActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OmniReflect.started += instance.OnOmniReflect;
                @OmniReflect.performed += instance.OnOmniReflect;
                @OmniReflect.canceled += instance.OnOmniReflect;
            }
        }
    }
    public AbilitiesActions @Abilities => new AbilitiesActions(this);

    // Interaction
    private readonly InputActionMap m_Interaction;
    private IInteractionActions m_InteractionActionsCallbackInterface;
    private readonly InputAction m_Interaction_Interact;
    private readonly InputAction m_Interaction_ProgressDialogue;
    public struct InteractionActions
    {
        private @PlayerControls m_Wrapper;
        public InteractionActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_Interaction_Interact;
        public InputAction @ProgressDialogue => m_Wrapper.m_Interaction_ProgressDialogue;
        public InputActionMap Get() { return m_Wrapper.m_Interaction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionActions set) { return set.Get(); }
        public void SetCallbacks(IInteractionActions instance)
        {
            if (m_Wrapper.m_InteractionActionsCallbackInterface != null)
            {
                @Interact.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnInteract;
                @ProgressDialogue.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnProgressDialogue;
                @ProgressDialogue.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnProgressDialogue;
                @ProgressDialogue.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnProgressDialogue;
            }
            m_Wrapper.m_InteractionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @ProgressDialogue.started += instance.OnProgressDialogue;
                @ProgressDialogue.performed += instance.OnProgressDialogue;
                @ProgressDialogue.canceled += instance.OnProgressDialogue;
            }
        }
    }
    public InteractionActions @Interaction => new InteractionActions(this);
    public interface IAimingActions
    {
        void OnGamepad(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
    }
    public interface IMovingActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
    public interface IAttackingActions
    {
        void OnReflect(InputAction.CallbackContext context);
    }
    public interface IPausingActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IAbilitiesActions
    {
        void OnOmniReflect(InputAction.CallbackContext context);
    }
    public interface IInteractionActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnProgressDialogue(InputAction.CallbackContext context);
    }
}
