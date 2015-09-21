namespace Contr
{
    using Events;
    public interface Controller
	{
        GameDispatcher getDispatcher();
        void registerSocket();
        void addEvent();
        void Init();
        void Update();
	}
}