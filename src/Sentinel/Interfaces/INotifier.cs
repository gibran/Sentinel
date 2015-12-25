namespace Sentinel.Interfaces
{
    internal interface INotifier
    {
        void Notify(string testKey, string message, bool status);

        void NotifyItem(string testKey, string item, string message, bool status);
    }
}