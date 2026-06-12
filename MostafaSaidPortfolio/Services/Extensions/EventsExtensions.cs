namespace MostafaSaidPortfolio.Extensions
{
    public static class EventsExtensions
    {
        public static IServiceCollection AddCustomEvents(this IServiceCollection services)
        {
            // Configure event-specific infrastructure here — calendar integrations,
            // reminder background jobs, or iCal/webhook providers.
            // Example:
            //   services.AddHostedService<EventReminderService>();
            return services;
        }
    }
}
