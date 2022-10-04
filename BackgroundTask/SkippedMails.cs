using System;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using UniqueTodoApplication.Context;
using UniqueTodoApplication.Interface.IRepositries;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.MailFolder;
using UniqueTodoApplication.Controllers;
using System.Linq;

namespace UniqueTodoApplication.BackgroundTask
{
    public class SkippedMails : BackgroundService
    {
        private readonly SkippedMailsConfig _configuration;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly ILogger<SkippedMails> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public SkippedMails(IServiceScopeFactory serviceScopeFactory, 
        IOptions<SkippedMailsConfig> configuration, ILogger<SkippedMails> logger)
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
                    var task = await todoitemContext.GetAllExpiredTime();
                    if(task.Data.Count() > 0)
                    {
                        await todoitemContext.UpdateTodoitemToSkipped(task.Data.ToList());
                    }

                    var todos = await todoitemContext.GetAllSkippedTodoitem();
                    foreach (var item in todos.Data)
                    {
                        var todoitem = await todoitemContext.GetTodoitem(item.Id);
                        var customer = await customerContext.GetCustomer(todoitem.Data.CustomerId);
                        var reminder = new SkippedRequest
                        {
                            FirstName = customer.Data.FirstName,
                            Name = todoitem.Data.Name,
                            ToEmail = customer.Data.Email,
                            OriginalTime = todoitem.Data.OriginalTime
                        };
                    }
                    
                    
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occured reading Reminder Table in database. {ex.Message}");
                    _logger.LogError(ex, ex.Message);
                }
                _logger.LogInformation($"Background Hosted Service for {nameof(SkippedMails)} is stopping ");
                var timeSpan = _nextRun - now;
                await Task.Delay(timeSpan, stoppingToken);
                _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
            }
        }
    }
}