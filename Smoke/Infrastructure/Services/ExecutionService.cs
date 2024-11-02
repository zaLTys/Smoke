using Application.Abstractions.Services;
using Domain.Abstractions.Repositories;
using Domain.Entities.Requests;
using Domain.Entities.Scenarios;
using System.Collections.Concurrent;
using System.Text;

public class ExecutionService : IExecutionService
{
    private readonly IRequestRepository _requestRepository;
    private readonly IHttpRequestService _httpRequestService;

    public ExecutionService(IRequestRepository requestRepository, IHttpRequestService httpRequestService)
    {
        _requestRepository = requestRepository;
        _httpRequestService = httpRequestService;
    }

    public async Task<ExecutionResult> ExecuteScenarioAsync(Scenario scenario)
    {
        var startTime = DateTime.UtcNow;
        var stepResults = new ConcurrentDictionary<Guid, ScenarioStepResult>();
        var stepTasks = new Dictionary<Guid, Task<ScenarioStepResult>>();
        var logs = new StringBuilder();

        var steps = scenario.Steps.OrderBy(s => s.Order).ToList();

        var sharedData = new ConcurrentDictionary<string, object>();

        foreach (var step in steps)
        {
            stepTasks[step.Id] = ExecuteStepAsync(step, stepTasks, sharedData, logs)
                .ContinueWith(task =>
                {
                    stepResults[step.Id] = task.Result;
                    return task.Result;
                });
        }

        await Task.WhenAll(stepTasks.Values);

        var isSuccess = stepResults.Values.All(sr => sr.IsSuccess);
        var endTime = DateTime.UtcNow;

        return new ExecutionResult(
            ScenarioId: scenario.Id,
            StepResults: stepResults.Values.ToList(),
            StartTime: startTime,
            EndTime: endTime,
            IsSuccess: isSuccess,
            Logs: logs.ToString()
        );
    }

    private async Task<ScenarioStepResult> ExecuteStepAsync(
    ScenarioStep step,
    Dictionary<Guid, Task<ScenarioStepResult>> stepTasks,
    ConcurrentDictionary<string, object> sharedData,
    StringBuilder logs)
    {
        var dependencyTasks = step.DependsOn?
            .Where(depId => stepTasks.ContainsKey(depId))
            .Select(depId => stepTasks[depId]) ?? Enumerable.Empty<Task<ScenarioStepResult>>();

        var dependencyResults = await Task.WhenAll(dependencyTasks);

        var cts = new CancellationTokenSource();
        if (step.TimeOut.HasValue)
        {
            cts.CancelAfter(step.TimeOut.Value);
        }

        try
        {
            var apiRequest = _requestRepository.GetById(step.RequestId);

            // Map input data
            //if (step.Mappings != null)
            //{
            //    foreach (var mapping in step.Mappings)
            //    {
            //        if (sharedData.TryGetValue(mapping.Key, out var value))
            //        {
            //            // Replace placeholders in apiRequest with mapped values
            //            apiRequest = ApplyMapping(apiRequest, mapping.Value, value);
            //        }
            //    }
            //}

            var result = await _httpRequestService.SendRequestAsync(apiRequest);

            var outputData = FormatOutput(result.Response);

            foreach (var output in outputData)
            {
                sharedData[output.Key] = output.Value;
            }

            var stepResult = new ScenarioStepResult(
                StepId: step.Id,
                Response: result.Response,
                IsSuccess: result.IsSuccess,
                ErrorMessage: result.ErrorMessage,
                OutputData: outputData
            );

            logs.AppendLine($"Step {step.Order} {apiRequest.Name} executed successfully: {result.IsSuccess}");

            if (step.DelayAfter.HasValue)
            {
                await Task.Delay(step.DelayAfter.Value);
            }

            return stepResult;
        }
        catch (OperationCanceledException)
        {
            var errorMessage = $"Step {step.Id} timed out.";

            logs.AppendLine(errorMessage);

            return new ScenarioStepResult(
                StepId: step.Id,
                Response: null,
                IsSuccess: false,
                ErrorMessage: errorMessage,
                OutputData: new Dictionary<string, string>()
            );
        }
        catch (Exception ex)
        {
            var errorMessage = $"Error executing step {step.Id}: {ex.Message}";

            logs.AppendLine(errorMessage);

            return new ScenarioStepResult(
                StepId: step.Id,
                Response: null,
                IsSuccess: false,
                ErrorMessage: errorMessage,
                OutputData: new Dictionary<string, string>()
            );
        }
    }

    //private ApiRequest ApplyMapping(ApiRequest apiRequest, string placeholder, object value)
    //{
    //    var requestData = apiRequest.ApiRequestData;

    //    if (!string.IsNullOrEmpty(requestData.Url))
    //    {
    //        requestData.Url = requestData.Url.Replace(placeholder, value.ToString());
    //    }

    //    // Replace in Headers
    //    if (requestData.Headers != null)
    //    {
    //        var newHeaders = new Dictionary<string, string>();
    //        foreach (var header in requestData.Headers)
    //        {
    //            newHeaders[header.Key] = header.Value.Replace(placeholder, value.ToString());
    //        }
    //        requestData.Headers = newHeaders;
    //    }

    //    // Replace in Body
    //    if (!string.IsNullOrEmpty(requestData.Body))
    //    {
    //        requestData.Body = requestData.Body.Replace(placeholder, value.ToString());
    //    }

    //    return apiRequest;
    //}

    private Dictionary<string, string> FormatOutput(string responseContent)
    {
        try
        {
            return new Dictionary<string, string>
            {
                { "responseContent", responseContent }
            };
        }
        catch
        {
            return new Dictionary<string, string>();
        }
    }



}
