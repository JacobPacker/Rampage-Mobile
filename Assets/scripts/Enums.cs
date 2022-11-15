
    public enum States // used by all logic
    {
        None,
        Stand,    
        Walk,
        Fall,
        PreJump,
        DyingJump,
        Jump_up,
        Jump_forward,
        Landed,
        OnBalloon,
        Dying,
        Dead,
        Alive,
        Shoot,
        Mode1,
        Mode2,
        Mode3,
        Mode4,
        Mode5,
        Mode6,
        Mode7,
        Mode8,
        Throw,
        Wait,
        ChildOfPlayer,
        ParentOfPlayer,
    }

    public enum Dir: int
    {
        Left,
        Right,
        Up,
        Down,
        Forward,
    }

    public enum HitStates
    {
        None,
        Ground
    }
