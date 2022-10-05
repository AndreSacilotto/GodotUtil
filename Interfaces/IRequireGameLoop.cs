namespace Util
{
    public interface IRequireGameLoop
    {
        /// <summary>Funtion to be called on Game Loop Update</summary>
        void Step(float delta);
    }
}
