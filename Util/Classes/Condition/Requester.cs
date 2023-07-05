namespace Util.Classes;

public class Requester
{
    private bool requested;
    private int requestCount;

    public int Count => requestCount;
    public bool InRequest => requested;

    public void AddRequest()
    {
        requestCount++;
        requested = true;
    }

    public void RemoveRequest()
    {
        if (--requestCount <= 0)
        {
            requestCount = 0;
            requested = false;
        }
    }

}
