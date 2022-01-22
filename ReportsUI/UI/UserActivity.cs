namespace UIReports.UI
{
    public abstract class UserActivity
    {
        public abstract void ShowActivityContent();

        protected void StartActivity(UserActivity activity)
        {
            activity.ShowActivityContent();
        }

        protected void Refresh()
        {
            this.ShowActivityContent();
        }

    }
}