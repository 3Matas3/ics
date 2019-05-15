using Caliburn.Micro;

namespace SimpleTeamApp.App.Services
{
	/// <summary>
    /// Pomocná proxy třída pro zapozdření DependencyInjection kontejneru.
    /// </summary>
    public class IoCProxy
    {
		public virtual TService Get<TService>(string key = null)
        {
            return IoC.Get<TService>(key);
        }
    }
}
