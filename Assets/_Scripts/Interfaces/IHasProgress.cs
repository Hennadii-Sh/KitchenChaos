using System;

public interface IHasProgress
{
    
    // Really, it should be enough of property named "progress" for purpose of changing progress bar fill amount.
    // A little bit overkill, in my opinion:
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float _progressNormalized;
    }
}
