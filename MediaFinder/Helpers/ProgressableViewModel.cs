﻿using CommunityToolkit.Mvvm.Messaging;

using MediaFinder_v2.DataAccessLayer;
using MediaFinder_v2.Messages;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Windows.Input;

namespace MediaFinder_v2.Helpers;

public abstract class ProgressableViewModel
{
    private readonly ILogger _logger;

    protected ProgressableViewModel(
        IMessenger messenger,
        ILogger logger,
        AppDbContext dbContext)
    {
        _progressToken = Guid.NewGuid();
        _messenger = messenger;
        _logger = logger;
        _dbContext = dbContext;
    }

    protected readonly IMessenger _messenger;
    protected readonly AppDbContext _dbContext;
    protected readonly object _progressToken;

    #region Progress Actions

    protected void ShowProgressIndicator(string message, ICommand? cancelCommand = null)
    {
        _messenger.Send(ShowProgressMessage.Create(_progressToken, message, cancelCommand));
        _logger.LogInformation(message);
    }

    protected void UpdateProgressIndicator(string message)
    {
        _messenger.Send(UpdateProgressMessage.Create(_progressToken, message));
        _logger.LogInformation(message);
    }

    protected void CancelProgressIndicator(string message)
    {
        _messenger.Send(CancelProgressMessage.Create(_progressToken));
        _logger.LogInformation(message);
    }

    protected void HideProgressIndicator()
    {
        _messenger.Send(CompleteProgressMessage.Create(_progressToken));
        _logger.LogInformation("Process Complete.");
    }

    #endregion

    #region Completion Actions

    protected static async Task TruncateFileDetailState(AppDbContext dbContext)
    {
        await dbContext.FileDetails.Include(fd => fd.FileProperties).ExecuteDeleteAsync();
        dbContext.ChangeTracker.Clear();
    }

    #endregion
}
