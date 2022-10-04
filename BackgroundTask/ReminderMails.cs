using System;
using System.Threading;
using System.Threading.Tasks;
using UniqueTodoApplication.MailFolder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using UniqueTodoApplication.BackgroundConfiguration;
using UniqueTodoApplication.Context;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Interface.IRepositries;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.BackgroundTask
{
    public class ReminderMails : BackgroundService
    {
        private readonly ReminderMailsConfig _configuration;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly ILogger<ReminderMails> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ReminderMails(IServiceScopeFactory serviceScopeFactory, 
        IOptions<ReminderMailsConfig> configuration, ILogger<ReminderMails> logger)
        {

            _configuration = configuration.Value;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _schedule = CrontabSchedule.Parse(_configuration.CronExpression);
            _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);

        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
             while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    var todoitemContext = scope.ServiceProvider.GetRequiredService<ITodoitemService>();
                    var reminderContext = scope.ServiceProvider.GetRequiredService<IReminderRepository>();
                    var customerContext = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                    var mailContext = scope.ServiceProvider.GetRequiredService<IMailService>();
                    var intervals = await reminderContext.GetAll();

                    foreach(var interval in intervals)
                    {
                        var todoitem = await todoitemContext.GetTodoitem(interval.TodoitemId);
                        var customer = await customerContext.GetCustomer(todoitem.Data.CustomerId);
                        var reminder = new ReminderRequest
                        {
                            FirstName = customer.Data.FirstName,
                            Name = todoitem.Data.Name,
                            ToEmail = customer.Data.Email,
                            OriginalTime = todoitem.Data.OriginalTime
                        };
                        var diff = Math.Abs(int.Parse((interval.Time - DateTime.Now).Minutes.ToString()));
                        if( diff <= 4)
                        {
                          await  mailContext.Reminder(reminder);                         
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occured reading Reminder Table in database. {ex.Message}");
                    _logger.LogError(ex, ex.Message);
                }
                _logger.LogInformation($"Background Hosted Service for {nameof(ReminderMails)} is stopping ");
                var timeSpan = _nextRun - now;
                await Task.Delay(timeSpan, stoppingToken);
                _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
            }
        }
    }
}