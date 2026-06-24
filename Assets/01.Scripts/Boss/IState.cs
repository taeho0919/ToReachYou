using UnityEngine;

public interface IState 
{
    public void Started();
    public void Looped();
    public void Stopped();
}
