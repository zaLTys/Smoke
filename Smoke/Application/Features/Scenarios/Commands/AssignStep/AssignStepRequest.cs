namespace Application.Features.Requests.HttpRequest.Commands.CreateApiRequest;

public sealed record AssignStepRequest(Guid RequestId, Guid ScenarioId, int Order);